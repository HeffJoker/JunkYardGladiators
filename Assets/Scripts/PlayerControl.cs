using UnityEngine;
using System.Collections;
using InControl;

public class PlayerControl : MonoBehaviour {

	public float MoveSpeed = 25f;
	public Texture2D CursorTexture;
	public float MaxVelocity = 2f;
	public GameStateManager StateManager;


	private Rigidbody2D rigidBody;
	private WeaponHolder weaponHolder;
	private Player player;

	// Use this for initialization
	void Start () 
    {
		rigidBody = GetComponent<Rigidbody2D>();
		weaponHolder = GetComponent<WeaponHolder>();
		player = GetComponent<Player>();

		//Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);

        if (!StateManager)
            Debug.LogError("No statemanager set for '" + gameObject.name + "'");
	}

	void Update()
	{
		InputDevice inDevice = InputManager.ActiveDevice;

		rigidBody.velocity = inDevice.LeftStick.Vector * MoveSpeed;

		if(weaponHolder.IsTwoHanded)
		{
			if(inDevice.LeftTrigger.WasPressed || inDevice.RightTrigger.WasPressed)
				Attack (false);
			//else
			//	StopAttacking(false);
		}
		else
		{
			if(inDevice.LeftTrigger.IsPressed)
				Attack(true);
			else
				StopAttacking(true);
			
			if(inDevice.RightTrigger.IsPressed)
				Attack (false);
			else
				StopAttacking(false);
		}
		
		if(inDevice.LeftBumper.WasPressed)
			DropWeapon(true);
		
		if(inDevice.RightBumper.WasPressed)
			DropWeapon(false);
		
		Vector2 lookDir;
		
		if(inDevice.Name == "Mouse/Keyboard mapping")
			lookDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
		else
			lookDir = inDevice.RightStick.Vector.normalized;
		
		if(lookDir != Vector2.zero)
			transform.rotation = Quaternion.AngleAxis(MathUtil.VectorToAngle(lookDir), -Vector3.back);

        if (inDevice.MenuWasPressed && StateManager)
			StateManager.Pause();
	}

	void Attack(bool isLeft)
	{
		WeaponHand hand = (isLeft ? weaponHolder.LeftHand : weaponHolder.RightHand);

		if(hand.AttachedWeapon != null)
		{
			player.PlayAttackAnim(hand.AttachedWeapon.Type, isLeft);
			/*
			switch(hand.AttachedWeapon.Type)
			{
			case WeaponType.ONE_HAND_PROJ: player.PlaySimpleShootAnim(isLeft); break;
			case WeaponType.ONE_HAND_MELEE: player.PlaySimpleMeleeAnim(isLeft); break;
			case WeaponType.ONE_HAND_STAB: player.PlayAttackAnim(isLeft); break;
			}*/	
		}

		hand.AttackWithWeapon();
	}

	void StopAttacking(bool isLeft)
	{
		WeaponHand hand = (isLeft ? weaponHolder.LeftHand : weaponHolder.RightHand);

		hand.StopAttack();
		player.StopAttackAnims(isLeft);
		//player.StopSimpleShootAnim(isLeft);
		//player.StopSimpleMeleeAnim(isLeft);
	}

	void DropWeapon(bool isLeft)
	{
		weaponHolder.IsTwoHanded = false;

		if(isLeft)
			weaponHolder.LeftHand.DetachWeapon(false);
		else
			weaponHolder.RightHand.DetachWeapon(false);

		player.TwoHWeaponLost();

		StopAttacking(isLeft);
	}
}
