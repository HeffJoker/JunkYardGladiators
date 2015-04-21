using UnityEngine;
using System.Collections;

public enum WeaponType
{
	ONE_HAND_MELEE,
	ONE_HAND_PROJ,
	ONE_HAND_STAB,
	TWO_HAND_MELEE,
	TWO_HAND_PROJ,
}

public abstract class Weapon : MonoBehaviour {

	public float AttackCooldown = 1f;
	public int MaxUses = 5;

	public float PickupCoolDown = 2f;
	public WeaponType Type;
	public string WeaponName = string.Empty;

	private bool canAttack = true;
	private bool canPickup = true;
	private int currUses = 0;
	private Quaternion startRot;

	public int CurrUses
	{
		get { return currUses; }
		set { currUses = value; }
	}

	public bool CanPickup
	{
		get { return canPickup; }
	}

	public Quaternion StartRotation
	{
		get { return startRot; }
	}

	public void Attack()
	{
		if(!canAttack)
			return;

		DoAttack();
		canAttack = false;
		Invoke("AllowAttack", AttackCooldown);
		--currUses;
	}

	public void Detach()
	{
		canPickup = false;
		transform.rotation = startRot;
		Invoke("AllowPickup", PickupCoolDown);
	}

	protected abstract void DoAttack();

	void Start()
	{
		currUses = MaxUses;
		startRot = transform.localRotation;
	}

	void AllowAttack()
	{
		canAttack = true;
	}

	void AllowPickup()
	{
		canPickup = true;
	}
}
