using UnityEngine;
using System.Collections;

public class ProjectileWeapon : Weapon {

	public Projectile ProjPrefab;


	#region implemented abstract members of Weapon

	protected override void DoAttack ()
	{
		Projectile newProj = Instantiate(ProjPrefab, transform.position, Quaternion.identity) as Projectile;

		Vector2 dir = MathUtil.AngleToVector(transform.eulerAngles.z);
		newProj.Move(dir.normalized, this);
	}

	#endregion
}
