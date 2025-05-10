using TMPro.Examples;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class HeroAnim : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer CharacterSprite;
    
    [Header("Movement")]
    public float jumpForce;
    public float speed;
    private float moveInput;

    [Header("Ground Check")]
    public Transform groundPos; //ground check
    private bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    
    [Header("Jumping")]
    private float jumpTimeCounter;
    public float jumpTime;
    //private bool isJumping;

    private int extraJumps;
    public int extraJumpValue;

    
    
    private void Start()
    {
        extraJumps = extraJumpValue;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround); //check if player is grounded
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        //idle to running animation
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
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
    }

    private void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
            jumpTimeCounter = jumpTime;
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
        

        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            anim.SetTrigger("takeOff");
            rb.linearVelocity = Vector2.up * jumpForce;
            extraJumps--;
            jumpTimeCounter -= Time.deltaTime;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            anim.SetTrigger("takeOff");
            rb.linearVelocity = Vector2.up * jumpForce;
        }
        
        
        //if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        //{
            //anim.SetTrigger("takeOff");
            //isJumping = true;
            //jumpTimeCounter = jumpTime;
            //rb.linearVelocity = Vector2.up * jumpForce;
        //}
        
        //if(Input.GetKey(KeyCode.Space) && isJumping == true)
        //{
            //if(jumpTimeCounter > 0)
            //{
                //rb.linearVelocity = Vector2.up * jumpForce;
                //jumpTimeCounter -= Time.deltaTime;
            //}
            //else
            //{
                //isJumping = false;
            //}
        //}


        //if (isGrounded == false && doubleJump == false && Input.GetKeyDown(KeyCode.Space))
        //{
            //isJumping = true;
            //doubleJump = true;
            //isJumping = true;
            //jumpTimeCounter = jumpTime;
            //rb.linearVelocity = Vector2.up * jumpForce;
            //rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        //}

        
        
    }


}
