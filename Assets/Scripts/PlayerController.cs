using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;

    private float Speed = 5f;

    private Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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
    }
}
