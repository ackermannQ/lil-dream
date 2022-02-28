using Pathfinding;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Transform target;
    public Transform enemyGFX;
    public float Speed = 300f;
    public float nextWayPointDistance = 3f;
    public Animator animator;
    public AudioSource chaseMusic;

    private Path path;

    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    private readonly float RegularSpeed = 300f;
    private readonly float ChasePlayerDistance = 8f;
    private readonly float AttackPlayerDistance = 5f;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private bool playingMusic = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        startingPosition = transform.position;
        roamPosition = GetRoamingDirection();
    }

    private void Update()
    {
        animator.SetFloat("Horizontal", rb.velocity.x);
        animator.SetFloat("Vertical", rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }
    private void UpdatePath()
    {
        var distanceToTarget = Vector2.Distance(rb.position, target.position);

        if (seeker.IsDone())
        {
            if (distanceToTarget <= ChasePlayerDistance)
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
                Speed = 1500f;

                if (!playingMusic)
                {
                    playingMusic = true;
                    chaseMusic.Play();
                }

                if (distanceToTarget <= AttackPlayerDistance)
                {

                    Speed = 2000f;

                    if (distanceToTarget <= 1.3f)
                    {
                        Reality.hasBeenCatch = true;
                    }
                }
                else
                {
                    animator.SetBool("IsAttacking", false);
                }
            }
            else
            {
                seeker.StartPath(rb.position, roamPosition, OnPathComplete);
                Speed = RegularSpeed;
                chaseMusic.Stop();
                playingMusic = false;
            }
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
            roamPosition = GetRoamingDirection();
        }
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        var direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;

        var force = direction * Speed * Time.deltaTime;
        rb.AddForce(force);

        var distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

    private Vector3 GetRoamingDirection()
    {
        return startingPosition + Tools.Utils.GetRandomDirection() * Random.Range(15f, 25f);
    }
}
