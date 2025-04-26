using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    public Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //is grounded?
        isGrounded = Physics2D.Raycast(transform.position, UnityEngine.Vector2.down, 1f, groundLayer);
        
        //player direction
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        if(isGrounded)
        {
            //chase player
            rb.linearVelocity = new UnityEngine.Vector2(direction * chaseSpeed, rb.linearVelocity.y);
        }

    }
}
