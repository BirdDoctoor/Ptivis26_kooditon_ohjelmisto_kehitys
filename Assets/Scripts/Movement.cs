using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    // Ground detection settings
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    private float moveInputH;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator is not assigned!");
        }
    }

    void Update()
    {
        Debug.Log(
     "Running: " + animator.GetBool("isRunning") +
     " | Speed: " + animator.speed +
     " | State: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime
 );
        // Get horizontal input
        moveInputH = Input.GetAxisRaw("Horizontal");

        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("takeOf");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
        }


        // Flip player direction
        if (moveInputH > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else if (moveInputH < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }

        // Running animation
        UpdateAnimations();

    }
    void UpdateAnimations()
    {
        // Walking animation
        animator.SetBool("isRunning",
            Mathf.Abs(moveInputH) > 0.01f && isGrounded);

        // Idle animation
        animator.SetBool("isIdle",
            Mathf.Abs(moveInputH) < 0.01f && isGrounded);

        // Jumping animation
        animator.SetBool("isJumping",
            !isGrounded && rb.velocity.y > 0.1f);

        // Falling animation
        animator.SetBool("isFalling",
            !isGrounded && rb.velocity.y < -0.1f);
    }
    void FixedUpdate()
    {
        // Move player horizontally
        if (rb != null)
        {
            rb.velocity = new Vector2(
                moveInputH * moveSpeed,
                rb.velocity.y
            );
        }
    }

    // Visualize ground detection
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRadius
            );
        }
    }
}