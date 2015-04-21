using UnityEngine;
using System.Collections;

public class Boomerang : Projectile {

	public bool AddOnReturn = true;
	public bool FollowWeapon = false;
	public float Distance = 5f;
	public float RoundTripTime = 3f;
	public float SpinSpeed = 0.1f;

	private bool reachedDest = false;
	private bool doMove = false;
	private Vector3 startPt;
	private Vector3 endPt;
	private float currTime;
	private ProjectileWeapon weapon;

	public override void Move (Vector2 dir, ProjectileWeapon weapon)
	{
		doMove = true;
		startPt = transform.position;
		endPt = startPt + (new Vector3(dir.x, dir.y, 0) * Distance);
		currTime = 0;
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		rigidBody.AddTorque(SpinSpeed);

		this.weapon = weapon;
	}

	protected override void DoCollide (Collider2D collider)
	{
		base.DoCollide (collider);

		if(collider.CompareTag("Player") && reachedDest)
		{
			weapon.CurrUses += 1;
			Die();
		}
	}

	void Update()
	{
		if(doMove)
		{
			if(!reachedDest)
			{
				transform.position = Vector3.Lerp(startPt, endPt, currTime / (RoundTripTime / 2));

				if(currTime >= (RoundTripTime / 2))//Vector3.Distance(transform.position, endPt) <= 0.1f)
				{
					reachedDest = true;
					currTime = 0;
				}
			}
			else
			{
				if(FollowWeapon && weapon != null)
					startPt = weapon.transform.position;

				transform.position = Vector3.Lerp(endPt, startPt, currTime / (RoundTripTime / 2));

				if((FollowWeapon && weapon != null) && Vector3.Distance(transform.position, startPt) <= 0.1f)
					Die();
				else if((!FollowWeapon || weapon == null) && currTime >= (RoundTripTime / 2))
					Die();
			}

			currTime += Time.deltaTime;
		}
	}
}
