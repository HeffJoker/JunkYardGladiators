using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour 
{
    int prefabsOnscreen = 0;

    public GameObject selectedPrefab;
    public GameObject spawnLocation;
    public float timeBetweenSpawns = 1f;
    public float spawnerDirection = 0;
    public int spawnTotal = 5;
    public float spawnAngleVariance = 40;
    public float forwardForce = 30;
    public float torque = 0;
    public bool noCollisionWhileSpawning = false;
    public bool infiniteSpawning = false;
    public bool removeRigidBodyAfterSpawn = false;
    public float timeBeforeRemovingRigidBody = 1;
    public float timeWithoutCollision = 0;
    public float timeWithGravity = 0;
    public float timedGravityScale = 0.2f;
    public float initialDelay = 0;
    public AudioSource[] spawnSfx = new AudioSource[1];

	// Use this for initialization
	void Start () 
    {

	}

	public void StartSpawn()
	{
		InvokeRepeating("SpawnObj", initialDelay, timeBetweenSpawns);
		
		if (!spawnLocation)
			Debug.LogError("No spawn location set for item launcher '" + gameObject.name + "'");
	}
	
	public void Reset()
	{
		CancelInvoke("SpawnObj");
		prefabsOnscreen = 0;
	}

    void SpawnObj()
    {
        if (infiniteSpawning || prefabsOnscreen < spawnTotal)
        {
            //Instantiate new prefab
            GameObject newObj = Instantiate(selectedPrefab, spawnLocation.transform.position, selectedPrefab.transform.rotation) as GameObject;
            prefabsOnscreen++;

            //Move prefab a little when spawned
            Rigidbody2D body = newObj.GetComponent<Rigidbody2D>();
            if (body)
            {
                //Generate a random angle variance 
                float randMagnitude = Random.Range(0, 2) == 0 ? 1 : -1;
                float randAngleVariance = Random.Range(0f, spawnAngleVariance) * randMagnitude;

                //Apply forces
                body.AddForce(MathUtil.AngleToVector(spawnerDirection + randAngleVariance) * forwardForce);
                
                if (torque!=0)                
                    body.AddTorque(torque);

                //Debug.Log("New gameObject spawned: Angle=" + (mainAngle + randAngleVariance) + 
                //          " RandomAngleVariance=" + randAngleVariance + 
                //          " Force=" + pushForce);

                //Play Sfx                
                AudioSource mySfx = spawnSfx[Random.Range(0, spawnSfx.Length)];
                if (mySfx)
                    mySfx.Play();

                if (timeWithGravity != 0)
                {
                    body.gravityScale = timedGravityScale;
                    StartCoroutine(turnOffGravity(timeWithGravity, body));
                }

                if (noCollisionWhileSpawning)
                {
                    Collider2D collider = newObj.GetComponent<Collider2D>();
                    if (collider)
                    {
                        collider.enabled = false;
                        StartCoroutine(turnOnCollider(timeWithoutCollision, collider));
                    }
                }

                if(removeRigidBodyAfterSpawn)
                    StartCoroutine(removeRigidBody(timeBeforeRemovingRigidBody, body));
            }
            else
            {
                Debug.LogError("No Rigid Body to spawn prefab '" + selectedPrefab.name + "'");
            }
        }        
    }


    IEnumerator turnOffGravity(float waitTime, Rigidbody2D body)
    {
        yield return new WaitForSeconds(waitTime);

        if (body != null)
            body.gravityScale = 0;
    }
    IEnumerator turnOnCollider(float waitTime, Collider2D collider)
    {
        yield return new WaitForSeconds(waitTime);

        if (collider)
            collider.enabled = true;
    }
    IEnumerator removeRigidBody(float waitTime, Rigidbody2D body)
    {
        yield return new WaitForSeconds(waitTime);

        if (removeRigidBodyAfterSpawn && body != null)
            Component.Destroy(body);
    }
}
