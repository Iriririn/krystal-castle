using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class HeroAnim : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;
    public float speed;


    public Transform groundPos;
    private bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;


    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private bool doubleJump;

    
    public Animator anim;
    public SpriteRenderer CharacterSprite;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
            isJumping = true;
            //doubleJump = true;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.linearVelocity = Vector2.up * jumpForce;
        }
        
        if (isGrounded == true)
        {
            doubleJump = false;
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }


        if (isGrounded == false && doubleJump == false && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            //doubleJump = true;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.linearVelocity = Vector2.up * jumpForce;
            //rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        }

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new UnityEngine.Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
        
        if (moveInput < 0)
        {
            CharacterSprite.flipX = true;
        }
        else if (moveInput > 0)
        {
            CharacterSprite.flipX = false;
        }
    }


}
