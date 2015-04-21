using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour {

	public int MaxHealth = 3;
	public List<Image> HealthImages;
	public bool GameOverOnDeath = false;

	private int currHealth = 0;

	public bool IsAlive
	{
		get { return currHealth > 0; }
	}

	public void TakeDamage()
	{
		--currHealth;

		if(HealthImages.Count > 0)
			HealthImages[currHealth].enabled = false;

		if(currHealth <= 0)
		{
			if(GameOverOnDeath)
				SendMessage("GameOver");
			else
				Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		currHealth = MaxHealth;

		if(HealthImages.Count > 0)
		{
			HealthImages.Sort((a, b) => { return a.transform.position.x.CompareTo(b.transform.position.x); });
			HealthImages.ForEach(x => x.enabled = true);
		}
	}
}
