using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float TimeToLive = 3f;
	public float MoveSpeed = 100f;
	public int Damage = 1;
	public string DamageTag;
    public string WallTag;
    public bool destroyOnWallHit = true;
	public bool destroyOnEnemyHit = true;

    public void Start()
    {
        if (DamageTag.Trim() == string.Empty)
            Debug.LogError("DamageTag is not set in '" + gameObject.name + "'");

        if (WallTag.Trim() == string.Empty)
            Debug.LogError("WallTag is not set in '" + gameObject.name + "'");
    }

	public virtual void Move(Vector2 dir, ProjectileWeapon weapon)
	{
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		rigidBody.AddForce(dir * MoveSpeed);
		Invoke("Die", TimeToLive);
	}
	
	protected void Die()
	{
		Destroy(gameObject);
	}

	protected virtual void DoCollide(Collider2D collider)
	{
		if(collider.CompareTag(DamageTag))
		{
			Health health = collider.GetComponent<Health>();
			
			if(health != null)
			{
				health.TakeDamage();

				if(destroyOnEnemyHit)
					Die();	
			}
		}

        if (destroyOnWallHit && collider.CompareTag(WallTag))
            Die();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		DoCollide(collider);
	}
}
