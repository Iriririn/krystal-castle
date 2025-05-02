using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    //private bool isChasing;
    private int facingDirection = 1;
    private EnemyState enemyState;
    
    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        if (enemyState == EnemyState.Chasing)
        {
            if(player.position.x > transform.position.x && facingDirection == 1||
            player.position.x < transform.position.x && facingDirection ==-1)
            {
                Flip();
            }
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (player == null)
            {
                player = collision.transform;
            }
            //isChasing = true;
            ChangeState(EnemyState.Chasing);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb.linearVelocity = Vector2.zero;
            //isChasing = false;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        //exit the current anim
        if(enemyState == EnemyState.Idle)
        anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
        anim.SetBool("isChasing", false);

        //update current state
        enemyState = newState;

        //update new anim
        if(enemyState == EnemyState.Idle)
        anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
        anim.SetBool("isChasing", true);
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
}