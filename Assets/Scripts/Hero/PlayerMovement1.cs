using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private int facingDirection = 1;


    [Header("Movement")]
    public InputActionReference move;
    [SerializeField] private float moveSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public InputActionReference jump;
    [SerializeField] private float jumpForce = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        GroundCheck();
        float horizontalMovement = Input.GetAxis("Horizontal");
        animator.SetFloat("xVelocity", Mathf.Abs(horizontalMovement));
        
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);

        if(horizontalMovement > 0 && transform.localScale.x < 0 ||
        horizontalMovement < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    private void Gravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; //makes player fall faster
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        //animator.SetFloat("magnitude", rb.linearVelocity.magnitude);

    }
    
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
        animator.SetBool("isMoving", true);
    } 

    public void Jump(InputAction.CallbackContext context)
    {
        AudioManager.Instance.PlaySFX("Jump");
        animator.SetBool("isJumping", true);

        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                //hold down jump button = full height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpsRemaining--;
                animator.SetBool("isJumping", true);
            }
            else if (context.canceled && rb.linearVelocity.y > 0)
            {
                //light tap of jump button = half the height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                jumpsRemaining--;
            }
        }
    
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    private void OnEnable()
    {
        jump.action.performed += Jump;
    }
    private void OnDisable()
    {
        jump.action.performed -= Jump;
    }
}

