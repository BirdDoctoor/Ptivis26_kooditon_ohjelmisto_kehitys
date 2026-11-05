using UnityEngine;

public class Movement : MonoBehaviour
{
    // Movement settings
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    // Ground detection settings
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    // Rigidbody reference
    private Rigidbody2D rb;

    // Player input
    private float moveInputH;

    // Is the player touching the ground?
    private bool isGrounded;

    void Start()
    {
        // Get the Rigidbody2D attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Read horizontal input (A/D or left/right arrows)
        moveInputH = Input.GetAxisRaw("Horizontal");

        // Check if the GroundCheck position overlaps with the Ground layer
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Only allow jumping when the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Apply upward velocity without changing horizontal velocity
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Normalize movement so diagonal movement cannot be faster
        Vector2 movement = new Vector2(moveInputH, 0).normalized;

        // Apply horizontal movement while preserving vertical velocity
        rb.velocity = new Vector2(
            movement.x * moveSpeed,
            rb.velocity.y
        );
    }

    // Draw a circle in the Scene view to visualize ground detection
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}