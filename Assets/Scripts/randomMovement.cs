using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class randomMovement : MonoBehaviour {

    public float timeBetweenMoves = 2f;
    public float moveForce = 10f;
    public enum affinity {NONE, LOW, MED, HIGH, STALKER};
    public affinity playerAffinity = affinity.LOW;
    
    GameObject player;

	// Use this for initialization
	void Start () 
    {
        InvokeRepeating("move", 0f, timeBetweenMoves);

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (player == null)
            Debug.LogError("Player reference not set for random movement");
	}
    
    void move()
    {
        bool gotoPlayer = false;
        
        //Randomly set if moving towards the player based on playerAffinity
        switch(playerAffinity)
        {
            case affinity.STALKER:
                gotoPlayer = true;                      //100% chance
                break;
            case affinity.HIGH:
                gotoPlayer = Random.Range(1, 3) == 1;   //50% chance
                break;
            case affinity.MED:
                gotoPlayer = Random.Range(1, 4) == 1;   //33% chance                
                break;
            case affinity.LOW:
                gotoPlayer = Random.Range(1, 5) == 1;   //25% chance
                break;
            case affinity.NONE:
            default:
                gotoPlayer = false;                     //0% chance
                break;
        }

        if (gotoPlayer)
            moveTowardsPlayer();
        else
            moveInRandomDirection();
    }

    void moveTowardsPlayer()
    {        
        Rigidbody2D body = transform.GetComponent<Rigidbody2D>();
        Vector3 vect2Player = Vector3.Normalize(player.transform.position - transform.position);
        body.AddForce(vect2Player * moveForce);
    }

    void moveInRandomDirection()
    {
        Rigidbody2D body = transform.GetComponent<Rigidbody2D>();
        body.AddForce(MathUtil.AngleToVector(Random.Range(0, 360)) * moveForce);
    }
}
