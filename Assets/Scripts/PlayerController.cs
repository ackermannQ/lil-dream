using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public static bool isInteracting = false;

    private Animator animator;

    private float Speed = 5f;

    private Vector2 movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isInteracting)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }

        if (Input.GetButtonDown("Run"))
        {
            Speed += 4f;
        }
        if (Input.GetButtonUp("Run"))
        {
            Speed = 5f;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(Speed * Time.fixedDeltaTime * movement + rb.position);


        if (Input.GetButtonDown("Action"))
        {
            isInteracting = true;
        }

        if (Input.GetButtonUp("Action"))
        {
            isInteracting = false;
        }

    }
}
