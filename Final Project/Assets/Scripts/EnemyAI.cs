using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private string nameOfDestination;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float spaceBetweenDest;
    [SerializeField]
    private float withinRadius;
    [SerializeField]
    private Rigidbody2D enemyRigidBody;
    [SerializeField]
    private float timeToChangeDirection;
    [SerializeField]
    private float enhanceForceBy;

    private Vector2 direction = Vector2.zero;
    private bool isMoving = false;
    private GameObject destination;
    // Update is called once per frame
    void Update()
    {
        destination = GameObject.FindGameObjectWithTag(nameOfDestination);
        if (destination != null)
        {
            Vector2 location = destination.transform.position;
            float distance = Vector2.Distance(transform.position, location);
            if (distance > withinRadius)
            {
                // Enemy wonders if outside given radius
                int chooseRandomDirection = Random.Range(0, 4);
                direction = Vector2.zero;
                Vector2 newDirection = Vector2.zero;
                if (chooseRandomDirection == 0)
                {
                    newDirection = new Vector2(0, speed);
                    StartCoroutine(WonderDirection(newDirection));
                }
                else if (chooseRandomDirection == 1)
                {
                    newDirection = new Vector2(0, -speed);
                    StartCoroutine(WonderDirection(newDirection));
                }
                else if (chooseRandomDirection == 2)
                {
                    newDirection = new Vector2(speed, 0);
                    StartCoroutine(WonderDirection(newDirection));
                }
                else
                {
                    newDirection = new Vector2(-speed, 0);
                    StartCoroutine(WonderDirection(newDirection));
                }
            }
            else if (distance > spaceBetweenDest)
            {
                // If destination (player) is within radius, enemy will begin to follow destination.
                Vector2 enemyLocation = transform.position;
                direction = (location - enemyLocation).normalized * speed;
            }
        }
    }

    // Moves enemy accordingly.
    void FixedUpdate()
    {
        if(direction != Vector2.zero)
        {
            enemyRigidBody.AddForce(direction - enemyRigidBody.velocity);
        }
        
    }

    // WonderDirection() applies force at a random direction on enemy object once and changes
    // into a new direction after "timeToChangeDirection" seconds.
    private IEnumerator WonderDirection(Vector2 newDirection)
    {
        if (!isMoving)
        {
            isMoving = true;
            enemyRigidBody.AddForce((newDirection * enhanceForceBy) - enemyRigidBody.velocity);
            yield return new WaitForSeconds(timeToChangeDirection);
            isMoving = false;
        }
    }
}
