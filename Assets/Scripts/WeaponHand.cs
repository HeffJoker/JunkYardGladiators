using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponHand : MonoBehaviour {

	public Weapon AttachedWeapon;
	public Canvas WeaponGUI;
	public Text WeaponText;
	public Slider AmmoGUI;
	public float DropAngle = 0f;	

	private Collider2D collider;

	public bool CanAttack
	{
		get;
		set;
	}

	public void AttackWithWeapon()
	{
		if(AttachedWeapon == null || !CanAttack)
			return;

		AttachedWeapon.Attack();
		//AmmoGUI.value = (float)AttachedWeapon.CurrUses / (float)AttachedWeapon.MaxUses;

		if(AttachedWeapon.CurrUses <= 0)
			DetachWeapon(true);
	}

	public void StopAttack()
	{
		CanAttack = false;
	}

	public void AllowDamage()
	{
		if(collider != null)
			collider.enabled = true;
	}

	public void StopDamage()
	{
		if(collider != null)
			collider.enabled = false;
	}

	public void AttachWeapon(Weapon weaponObj)
	{
		if(WeaponText != null)
			WeaponText.text = weaponObj.WeaponName;

		Rigidbody2D rigidBody = weaponObj.GetComponent<Rigidbody2D>();
		if(rigidBody != null)
			Destroy(rigidBody);

		collider = weaponObj.GetComponent<Collider2D>();
		weaponObj.transform.localRotation = weaponObj.StartRotation;
		weaponObj.transform.position = transform.position;
		weaponObj.transform.localRotation = Quaternion.AngleAxis(transform.parent.eulerAngles.z + weaponObj.transform.eulerAngles.z, -Vector3.back);
		weaponObj.transform.parent = transform;

        if (WeaponGUI)
		    WeaponGUI.enabled = true;

		//AmmoGUI.value = (float)weaponObj.CurrUses / (float)weaponObj.MaxUses;
		AttachedWeapon = weaponObj;
	}

	public void DetachWeapon(bool destroy)
	{
		if(AttachedWeapon == null)
			return;

        if (WeaponGUI)
            WeaponGUI.enabled = false;

		collider = null;

		if(destroy)
		{
			Destroy(AttachedWeapon.gameObject);
			SendMessageUpwards("TwoHWeaponLost");
		}
		else
		{
			AttachedWeapon.transform.parent = null;
			AttachedWeapon.Detach();
			//AttachedWeapon.transform.localRotation = Quaternion.AngleAxis(90f, -Vector3.back);
			//AttachedWeapon.transform.localRotation = Quaternion.identity;
		}

		AttachedWeapon = null;
	}

	void Update()
	{
        if (AttachedWeapon && AmmoGUI)
		{
			AmmoGUI.value = (float)AttachedWeapon.CurrUses / (float)AttachedWeapon.MaxUses;
		}
	}

	void Start()
	{
        if (WeaponGUI)
            WeaponGUI.enabled = false;
        else
            Debug.LogError("WeaponGUI not set for '" + gameObject.name + "'");

        if (!AmmoGUI)
            Debug.LogError("AmmoGUI not set for '" + gameObject.name + "'");

		CanAttack = false;
	}
}
