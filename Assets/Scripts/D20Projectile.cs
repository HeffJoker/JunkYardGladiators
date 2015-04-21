using UnityEngine;
using System.Collections;

public class D20Projectile : Projectile {

	public float ExplodeTime = 1f;
	public GameObject ExplosionObj;
	public TextMesh Text;
    public float torque = 10;

	private bool hasExploded = false;

	public override void Move (Vector2 dir, ProjectileWeapon weapon)
	{
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
			
		rigidBody.AddForce(dir * MoveSpeed);
        rigidBody.AddTorque(torque);

		StartCoroutine(RollD20());
	}	

	void Awake()
	{
		ExplosionObj.SetActive(false);
	}

	protected override void DoCollide (Collider2D collider)
	{
		if(hasExploded)
		{
			if(collider.CompareTag(DamageTag))
			{
				Health health = collider.GetComponent<Health>();
				
				if(health != null)
					health.TakeDamage();
			}
		}
	}

	IEnumerator RollD20()
	{
		yield return new WaitForSeconds(TimeToLive);

		int num = Random.Range(1, 21);

		Animator animator = GetComponent<Animator>();
		Text.text = num.ToString(); 

		if(animator != null)
			animator.SetInteger("Number", num);

		yield return new WaitForSeconds(ExplodeTime);

		hasExploded = true;
		ExplosionObj.transform.localScale = new Vector3((float)num, (float)num);
		ExplosionObj.SetActive(true);

		yield return new WaitForSeconds(0.25f);
		Die();
	}
}
