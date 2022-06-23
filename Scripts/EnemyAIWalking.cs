using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAIWalking : MonoBehaviour
{

    [Header("Pathfinding")]
    public Transform target;
    public float targetDistanceToActivate = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics & Stats")]
    public int enemyAttackPower = 20;
    public float jumpHeightRequirement = 0.8f;
    public float jumpHeight = 0.3f;
    public float jumpOffset = 0.1f;
    public float enemySpeed = 200f;
    public float distanceToNextWaypoint = 3f;
    public float enemyKnockbackAmount;
    

    [Header("Conditionals")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionalLookEnabled = true;

    [Header("A*")]
    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    public Rigidbody2D rb;

    public Animator animator;
  
    // Start is called before the first frame update
    void Start()
    {
        //call components
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //invoke path updates for the player based on path update timing
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //when target is in range AI will activate
        if(TargetInDistance() && followEnabled)
        {
            PathFollow();
        }       
    }

    // starts the path to the targets position (player) when the seeker has fully completed the path
    private void UpdatePath()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    //Checks that the target is within the scope of activate distance
    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < targetDistanceToActivate;
    }

    //Detects for pathing errors, if no error is detected the path and waypoints are created
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    //Knocksback the enemy when it collides with the player, the player also takes damage based on enemyAttackPower
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(enemyAttackPower);
                Vector2 enemyKnockback = rb.transform.position - other.transform.position;
                enemyKnockback.y = 0;


            }
        }
    }

    //Main method
    private void PathFollow()
    {
        //no path
        if (path == null)
        {
            return;
        }
        // End of path
        if (currentWaypoint >= path.vectorPath.Count)
        {   
            return;
        }
        // changes the current waypoint based on distance to the next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        //Updates the current path allowing the enemy to move to the next waypoint
        if (distance < distanceToNextWaypoint)
        {
            currentWaypoint++;
        }

        // if the enemy is colliding with an object using a raycast isGrounded is true
        isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + jumpOffset);

        // Calculates the direction and applies force based on direction of waypoints
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = enemySpeed * Time.fixedDeltaTime * direction;

        // Applies a jump force if one of the nodes is higher than the height requirement
        if(jumpEnabled && isGrounded)
        {
            if(direction.y > jumpHeightRequirement)
            {
                rb.AddForce(Vector2.up * enemySpeed * jumpHeight);
            }
        }

        //The animations for the enemy sprite
        if (rb.velocity.x > 0.1 || rb.velocity.x < 0.1)
        {
            animator.SetFloat("Speed", Mathf.Abs(0.2f));
        }
        else
        {
            animator.SetFloat("Speed", Mathf.Abs(0f));
        }
        //Movement
        if (!isGrounded)
        {
            force.y = 0;
        }
        rb.AddForce(force);
        // Changes the graphics of the enemy based on direction
        if (directionalLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }else if(rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

}
