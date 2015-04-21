using UnityEngine;
using System.Collections;

public class YoyoWeapon : ProjectileWeapon {

	public float Force = 100f;
	//public SpriteRenderer YoyoSprite;

	private bool throwingYoyo = false;
	private Boomerang projectile;
	private LineRenderer line;

	#region implemented abstract members of Weapon

	protected override void DoAttack ()
	{
		if(throwingYoyo)
			return;

		//YoyoSprite.enabled = false;
		projectile = Instantiate(ProjPrefab, transform.position, Quaternion.identity) as Boomerang;

		projectile.AddOnReturn = false;
		Vector2 dir = MathUtil.AngleToVector(transform.eulerAngles.z);
		projectile.Move(dir.normalized, this);
		throwingYoyo = true;
	}

	void Update()
	{
		if(throwingYoyo)
		{
			if(projectile == null)
			{
				throwingYoyo = false;
				return;
			}

			line.SetPosition(0, transform.position);
			line.SetPosition(1, projectile.transform.position);
		}
	}

	void Start()
	{
		line = GetComponent<LineRenderer>();
	}

	#endregion
}
