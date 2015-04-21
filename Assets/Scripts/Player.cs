using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameStateManager GameStateManager;
	public float DamageCooldown = 2f;

	private Animator animator;
	private bool canTakeDamage = true;

	public void PlayAttackAnim(WeaponType type, bool isLeft)
	{
		switch(type)
		{
		case WeaponType.ONE_HAND_MELEE: animator.SetBool((isLeft ? "L_Melee" : "R_Melee"), true); break;
		case WeaponType.ONE_HAND_PROJ: animator.SetBool((isLeft ? "L_Proj" : "R_Proj"), true); break;
		case WeaponType.ONE_HAND_STAB: animator.SetBool((isLeft ? "L_Stab" : "R_Stab"), true); break;
		case WeaponType.TWO_HAND_MELEE: animator.SetTrigger("2H_Swing"); break;
		}
	}

	public void StopAttackAnims(bool isLeft)
	{
		if(isLeft)
		{
			animator.SetBool("L_Proj", false);
			animator.SetBool("L_Melee", false);
			animator.SetBool("L_Stab", false);
		}
		else
		{
			animator.SetBool("R_Proj", false);
			animator.SetBool("R_Melee", false);
			animator.SetBool("R_Stab", false);
		}
	}

	public void PlaySimpleShootAnim(bool isLeft)
	{
		if(isLeft)
			animator.SetBool("L_Proj", true);
		else
			animator.SetBool("R_Proj", true);	
	}

	public void StopSimpleShootAnim(bool isLeft)
	{
		if(isLeft)
			animator.SetBool("L_Proj", false);
		else
			animator.SetBool("R_Proj", false);	
	}

	public void PlaySimpleMeleeAnim(bool isLeft)
	{
		if(isLeft)
			animator.SetBool("L_Melee", true);
		else
			animator.SetBool("R_Melee", true);	
	}

	public void StopSimpleMeleeAnim(bool isLeft)
	{
		if(isLeft)
			animator.SetBool("L_Melee", false);
		else
			animator.SetBool("R_Melee", false);	
	}

	public void TwoHWeaponFound()
	{
		animator.SetBool("TwoHanded", true);
	}

	public void TwoHWeaponLost()
	{
		animator.SetBool("TwoHanded", false);
	}

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void GameOver()
	{
		GameStateManager.GameOver();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("enemy") && canTakeDamage)
		{
			Health health = GetComponent<Health>();

			if(health.IsAlive)
			{
				health.TakeDamage();
				canTakeDamage = false;
				animator.SetBool("Damaged", true);
				Invoke("AllowDamage", DamageCooldown);
			}
		}
	}

	void AllowDamage()
	{
		animator.SetBool("Damaged", false);
		canTakeDamage = true;
	}
}
