using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingMonster : MonoBehaviour
{

    public float speed = 3.0f; // Monsters speed

    public float detectionRange = 10f; // Distance where monster will start chasing the player

    public Transform playerTransform; // Player's transform
    
    public float changeDirectionInterval = 4f; // Interval before changing direction

    public float maxRayDistance = 1; // Max distance used in wall avoidvance

    public List<Encounter> possibleEncounters; 

    private Vector3 movementDirection; 

    private float changeDirectionTimer;


    // Start is called before the first frame update
    void Start()
    {
        // Finds and sets the players transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        PickRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        changeDirectionTimer -= Time.deltaTime; 

        // Calculates the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Chase the player if within range
        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Wander();
        }

        AvoidWalls(); // Checks for and avoid walls
        Move(); // Moves in the current direction
    }

    void Move() // Moves the monster in the current movement direction
    {
        transform.position += movementDirection * speed * Time.deltaTime;
    }

    void Wander() // Randomly changes the monsters directions in intervals
    {
        if (changeDirectionTimer <= 0)
        {
            PickRandomDirection();
            changeDirectionTimer = changeDirectionInterval;
        }
    }

    void ChasePlayer() // Changes the movement direction towards the player
    {
        if (playerTransform != null)
        {
            movementDirection = (playerTransform.position - transform.position).normalized;
            movementDirection.y = 0;
        }
    }

    void AvoidWalls() // Detects walls and changes direction if detected
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, movementDirection, out hit, maxRayDistance))
        {
            if (hit.collider.gameObject != gameObject)
            {
                PickRandomDirection();
            }
        }
    }

    void PickRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        movementDirection = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad));
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Destroy(gameObject);
            GameManager.Instance.BeginEncounter(possibleEncounters[Random.Range(0, possibleEncounters.Count)]);
        }
    }
}
