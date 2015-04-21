using UnityEngine;
using System.Collections;

public class PebbleProjectile : Projectile {

	public float ExplodeTime = 1f;
	public GameObject RockSpawnObj;
	public Projectile RockPrefab;
	public float MaxNumRocks = 3f;
	public float RockOffset = 2f;

	public override void Move (Vector2 dir, ProjectileWeapon weapon)
	{
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
			
		rigidBody.AddForce(dir * MoveSpeed);
		StartCoroutine(DoPebble());
	}	

	protected override void DoCollide (Collider2D collider)
	{
	}

	IEnumerator DoPebble()
	{
		yield return new WaitForSeconds(TimeToLive);

		RockSpawnObj.transform.position = transform.position + new Vector3(0, 2f);

		float numRocks = MaxNumRocks; //.Random.Range(1, MaxNumRocks + 1);
		Vector3 position;

		Projectile newProj;
		for(int i = 0; i < numRocks; ++i)
		{
			position = RockSpawnObj.transform.position + 
				new Vector3(Random.Range(0, RockOffset + 1) * Mathf.Pow(-1, Random.Range(1, 3)),
				Random.Range(0, RockOffset + 1) * Mathf.Pow (-1, Random.Range(1, 3)));

			newProj = Instantiate(RockPrefab, position, Quaternion.identity) as Projectile;

			newProj.Invoke("Die", newProj.TimeToLive);
		}
		
		Die();
	}
}
