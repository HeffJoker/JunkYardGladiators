using UnityEngine;
using System.Collections;

public class WeaponHolder : MonoBehaviour {

	public WeaponHand LeftHand;
	public WeaponHand RightHand;
	public bool CanPickUpWeapons = false;
	public bool IsTwoHanded = false;

	public void AllowAttack_Left()
	{
		LeftHand.CanAttack = true;
	}

	public void AllowAttack_Right()
	{
		RightHand.CanAttack = true;
	}

	public void AllowDamage_Left()
	{
		LeftHand.AllowDamage();
	}

	public void AllowDamage_Right()
	{
		RightHand.AllowDamage();
	}

	public void StopDamage_Left()
	{
		LeftHand.StopDamage();
	}

	public void StopDamage_Right()
	{
		RightHand.StopDamage();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("weapon"))
		{
			Weapon weapon = collider.GetComponent<Weapon>();

			if(weapon == null)
				return;

			if(!weapon.CanPickup)
				return;

			if(weapon.Type == WeaponType.TWO_HAND_MELEE)
			{
				IsTwoHanded = true;

				LeftHand.DetachWeapon(false);
				RightHand.DetachWeapon(false);

				RightHand.AttachWeapon(weapon);

				SendMessage("TwoHWeaponFound");
			}
			else if(!IsTwoHanded)
			{
				if(LeftHand.AttachedWeapon == null)
					LeftHand.AttachWeapon(weapon);
				else if(RightHand.AttachedWeapon == null)
					RightHand.AttachWeapon(weapon);
			}
		}
	}

	void TwoHWeaponLost()
	{
		IsTwoHanded = false;
	}
}
