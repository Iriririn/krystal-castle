using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    private int facingDirection = 1;
    private PlayerState playerState;


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
        ChangeState(PlayerState.Idle);
    }

    void Update()
    {
        GroundCheck();

        float horizontalMovement = Input.GetAxis("Horizontal");
        animator.SetFloat("xVelocity", Mathf.Abs(horizontalMovement));
        

        if(horizontalMovement > 0 && transform.localScale.x < 0 ||
        horizontalMovement < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
        
        //Vector2 direction = (rb.position - transform.position).normalized;
        //rb.linearVelocity = direction * moveSpeed;

        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }
    
    
    public void Move(InputAction.CallbackContext context)
    {
        ChangeState(PlayerState.Moving);

        if (context.performed)
        {
            horizontalMovement = context.ReadValue<Vector2>().x;
            animator.SetBool("isMoving", true);
        }
        else if (context.canceled && rb.linearVelocity.x <= 0)
        {
            animator.SetBool("isMoving", false);
        }
        
        
    } 

    public void Jump(InputAction.CallbackContext context)
    {
        AudioManager.Instance.PlaySFX("Jump");
        ChangeState(PlayerState.Jumping);

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
                //rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                jumpsRemaining--;
                animator.SetBool("isJumping", false);
                animator.SetBool("isIdle", true);
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

    void ChangeState(PlayerState newState)
    {
        //exit the current anim
        if(playerState == PlayerState.Idle)
        animator.SetBool("isIdle", false);
        else if (playerState == PlayerState.Moving)
        animator.SetBool("isMoving", false);
        

        //update current state
        playerState = newState;

        //update new anim
        if(playerState == PlayerState.Idle)
        animator.SetBool("isIdle", true);
        else if (playerState == PlayerState.Moving)
        animator.SetBool("isMoving", true);
    }
}

public enum PlayerState
{
    Idle,
    Moving,
    Jumping,
}
