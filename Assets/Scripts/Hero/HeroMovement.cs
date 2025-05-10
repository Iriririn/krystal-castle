using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class HeroMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public SpriteRenderer CharacterSprite;

    
    [Header("Movement")]
    public InputActionReference move;
    public float moveSpeed = 5f;
    float moveInput; //horizontal movement

    
    [Header("Jumping")]
    public InputActionReference jump;
    public float jumpForce = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    
    [Header("GroundCheck")]
    private bool isGrounded;
    public float checkRadius;
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

    void FixedUpdate()
    {
        GroundCheck();
        Gravity();
        moveInput = Input.GetAxis("Horizontal");
        //animator.SetFloat("xVelocity", Mathf.Abs(moveInput));
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        //idle to running animation
        if (moveInput == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
        
        //flipping the character
        if (moveInput < 0)
        {
            CharacterSprite.flipX = true;
        }
        else if (moveInput > 0)
        {
            CharacterSprite.flipX = false;
        }

        //character dying
        if (rb.position.y < -2f)
        {
            FindAnyObjectByType<GameController>().EndGame();
        }
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
        //animator.SetFloat("yVelocity", rb.linearVelocity.y);
        //animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
        animator.SetBool("isRunning", true);
    } 

    public void Jump(InputAction.CallbackContext context)
    {
        AudioManager.Instance.PlaySFX("Jump");
        animator.SetTrigger("takeOff");

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
                animator.SetBool("isJumping", true);
            }
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, checkRadius, groundLayer);
        
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
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
        //animator.SetBool("isJumping", true);
    }
    private void OnDisable()
    {
        jump.action.performed -= Jump;
        //animator.SetBool("isJumping", false);
    }
}
