using Pathfinding;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;

    [Header("Enemy Settings")]
    public Transform enemyGFX;
    public float Speed = 300f;
    public float nextWayPointDistance = 3f;
    public Animator animator;

    [Header("Environment Settings")]
    public AudioSource chaseMusic;

    [Header("Probes Settings")]
    public GameObject ProbeUp;
    public GameObject ProbeDown;
    public GameObject ProbeLeft;
    public GameObject ProbeRight;

    //Enemy A* Settings
    private Path path;

    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    //Enemy physics Settings
    private readonly float RegularSpeed = 300f;
    private readonly float ChasePlayerDistance = 8f;
    private readonly float AttackPlayerDistance = 5f;
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    // Environment
    private bool playingMusic = false;

    // Sensors
    private float sensorLength = 0.2f;
    private bool hasObjectTop = false;
    private bool hasObjectBelow = false;
    private bool hasObjectOnLeft = false;
    private bool hasObjectOnRight = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        startingPosition = transform.position;
        //roamPosition = GetRoamingDirection();
        roamPosition = startingPosition;
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
                animator.SetBool("IsAttacking", true);

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

    private Vector3 GetRoamingDirection()
    {
        return startingPosition + Tools.Utils.GetRandomDirection() * Random.Range(5f, 100f);
    }

    private void Update()
    {
        animator.SetFloat("Horizontal", rb.velocity.x);
        animator.SetFloat("Vertical", rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }

    void FixedUpdate()
    {
        EnemyMovements();
        Sensors();
        MovementDecisions();
    }

    private void EnemyMovements()
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

    private void Sensors()
    {
        var hitUp = Physics2D.Raycast(ProbeUp.transform.position, ProbeUp.transform.TransformDirection(Vector2.up), sensorLength, 1 << 10);

        if (hitUp)
        {
            hasObjectTop = true;
        }

        var hitDown = Physics2D.Raycast(ProbeDown.transform.position, ProbeDown.transform.TransformDirection(-Vector2.up), sensorLength, 1 << 10);

        if (hitDown)
        {
            hasObjectBelow = true;
        }

        var hitLeft = Physics2D.Raycast(ProbeLeft.transform.position, ProbeLeft.transform.TransformDirection(-Vector2.right), sensorLength, 1 << 10);

        if (hitLeft)
        {
            hasObjectOnLeft = true;
        }

        var hitRight = Physics2D.Raycast(ProbeRight.transform.position, ProbeRight.transform.TransformDirection(Vector2.right), sensorLength, 1 << 10);

        if (hitRight)
        {
            hasObjectOnRight = true;
        }
    }

    private void MovementDecisions()
    {
        if (hasObjectTop)
        {
            var rndChoice = Random.Range(-150f, 150f);

            if (rndChoice <= -50)
            {
                GoDown();
            }

            if (rndChoice > -50 && rndChoice < 50)
            {
                GoLeft();
            }

            if (rndChoice >= 50)
            {
                GoRight();
            }
        }

        if (hasObjectOnLeft)
        {
            var rndChoice = Random.Range(-150f, 150f);

            if (rndChoice <= -50)
            {
                GoDown();
            }

            if (rndChoice > -50 && rndChoice < 50)
            {
                GoTop();
            }

            if (rndChoice >= 50)
            {
                GoRight();
            }
        }

        if (hasObjectOnRight)
        {
            var rndChoice = Random.Range(-150f, 150f);

            if (rndChoice <= -50)
            {
                GoDown();
            }

            if (rndChoice > -50 && rndChoice < 50)
            {
                GoTop();
            }

            if (rndChoice >= 50)
            {
                GoLeft();
            }
        }

        if (hasObjectBelow)
        {
            var rnd = Random.Range(-150f, 150f);

            if (rnd <= -50)
            {
                GoTop();
            }

            if (rnd > -50 && rnd < 50)
            {
                GoTop();
            }

            if (rnd >= 50)
            {
                GoRight();
            }
        }
    }

    private void GoLeft()
    {
        rb.AddForce(new Vector2(-1, 0) * Speed * Time.deltaTime);
    }

    private void GoRight()
    {
        rb.AddForce(new Vector2(1, 0) * Speed * Time.deltaTime);
    }

    private void GoTop()
    {
        rb.AddForce(new Vector2(0, 1) * Speed * Time.deltaTime);
    }

    private void GoDown()
    {
        rb.AddForce(new Vector2(0, -1) * Speed * Time.deltaTime);
    }


}
