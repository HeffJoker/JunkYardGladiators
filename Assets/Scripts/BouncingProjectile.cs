using UnityEngine;
using System.Collections;

public class BouncingProjectile : Projectile {

	public int MaxBounces = 2;
	public float BounceOffset = 30f;

	protected override void DoCollide (Collider2D collider)
	{
		base.DoCollide (collider);

		if(collider.CompareTag("wall"))
		{
			if(MaxBounces == 0)
			{
				Die ();
				return;
			}

			Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

			Vector2 offset = MathUtil.AngleToVector(BounceOffset * Random.Range(-1, 1));
			rigidBody.velocity *= -1f;
			rigidBody.velocity += offset.normalized;

			--MaxBounces;
		}
	}
}
