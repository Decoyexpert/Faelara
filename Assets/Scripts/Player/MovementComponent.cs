using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    private CollisionComponent collisionComponent;
    public bool isFlying;
    private SpriteRenderer sr;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collisionComponent = GetComponent<CollisionComponent>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            Move(Input.GetAxisRaw("Horizontal"));
        }

        if (collisionComponent.onGround && !isFlying)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
        }
        if (isFlying)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Fly();
            }
        }

            animator.SetBool("isJumping", !collisionComponent.onGround);
    }

    public void Move(float direction)
    {
        rb.velocity += new Vector2 (direction, 0) * moveSpeed / 100;
        sr.flipX = direction < 0;
        animator.SetFloat("speed", direction);

    }
    public void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
    }
    public void Fly()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
    }
}
