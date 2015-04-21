using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon {

	public string ColliderTag = "enemy";
	public float Timeout = 0.5f;

	private Collider2D meleeCollider;
	private bool canDamage = false;

	#region implemented abstract members of Weapon
	protected override void DoAttack ()
	{
		//CancelInvoke("DoDisable");
		canDamage = true;

		if(Type != WeaponType.TWO_HAND_MELEE)
			Invoke("DoDisable", Timeout);
	}
	#endregion

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag(ColliderTag) && canDamage)
		{
			Health health = collider.GetComponent<Health>();

			if(health != null)
				health.TakeDamage();

			if(Type != WeaponType.TWO_HAND_MELEE)
			{
				CancelInvoke("DoDisable");
				canDamage = false;
			}
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.CompareTag(ColliderTag) && canDamage)
		{
			Health health = collider.GetComponent<Health>();
			
			if(health != null)
				health.TakeDamage();
			
			canDamage = false;
		}
	}

	void DoDisable()
	{
		canDamage = false;
	}
}
