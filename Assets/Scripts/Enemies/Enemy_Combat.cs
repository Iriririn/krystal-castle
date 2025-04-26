using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
<<<<<<< HEAD
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }
=======
        collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
>>>>>>> 6d943f248c7a1e04eb1148b551a6b81578dbd9a3
    }
}
