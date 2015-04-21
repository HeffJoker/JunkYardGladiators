using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour {

	public int SpawnerAmountIncrement = 1;
	public PrefabSpawner[] EnemySpawners;
	public PrefabSpawner[] WeaponSpawners;

	public GameObject[] WeaponPrefabs;
	public float WaveTimeout = 5f;
	public Animator WaveAnimator;
	public Text WaveGUI;

	private bool allowWave = true;
	private int currWave = 0;


	// Use this for initialization
	void Start () {
		DoNextWave();
		allowWave = false;
		Invoke("AllowWave", WaveTimeout);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

		if(allowWave && enemies.Length <= 0)
		{
			WaveGUI.text = string.Format("WAVE {0}", currWave + 1);
			WaveAnimator.SetTrigger("Show");

			Invoke("DoNextWave", 2);
			allowWave = false;
		}
	}

	void DoNextWave()
	{
		int weaponIndex;
		foreach(PrefabSpawner weaponSpawner in WeaponSpawners)
		{
			weaponIndex = Random.Range(0, WeaponPrefabs.Length);
			weaponSpawner.Reset();
			weaponSpawner.selectedPrefab = WeaponPrefabs[weaponIndex];
			weaponSpawner.StartSpawn();
		}

		int numToAdd = (currWave == 0 ? 0 : SpawnerAmountIncrement);

		foreach(PrefabSpawner enemySpawner in EnemySpawners)
		{
			enemySpawner.Reset();
			enemySpawner.spawnTotal += numToAdd;
			enemySpawner.StartSpawn();
		}

		++currWave;

		Invoke("AllowWave", WaveTimeout);
	}

	void AllowWave()
	{
		allowWave = true;
	}
}
