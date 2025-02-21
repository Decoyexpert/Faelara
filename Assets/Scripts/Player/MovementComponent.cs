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
            animator.SetFloat("speed", 1);
            animator.SetBool("moving", true); // fix animation idle to walk condition - valery
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("moving", false); // fix animation walk to idle condition - valery
        }

        if (collisionComponent.onGround && !isFlying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        if (isFlying)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Fly();
            }
        }

            animator.SetBool("inAir", !collisionComponent.onGround);
    }

    public void Move(float direction)
    {
       // float clampedSpeed = Mathf.Clamp(direction * moveSpeed, direction, direction * 20);
       // rb.velocity = new Vector2(clampedSpeed, rb.velocity.y) ;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        sr.flipX = direction < 0;
    }
    public void Jump()
    {
        //rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
         //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //more readable - valery
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    public void Fly()
    {
        //rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // changed to velocity - valery
    }
}
