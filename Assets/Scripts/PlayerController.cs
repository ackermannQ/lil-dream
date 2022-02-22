using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(Speed * Time.fixedDeltaTime * movement + rb.position);
    }
}
