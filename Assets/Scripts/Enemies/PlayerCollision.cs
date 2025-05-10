using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public HeroAnim movement; //reference the player movement script

    //function runs when it hits the object? get info about the collision and call it "collisionInfo"
    void OnCollisionEnter2D(UnityEngine.Collision2D collisionInfo)
    {
        //check if the object we collided with has a tag called "Obstacle"
        if (collisionInfo.collider.tag == "Obstacle")
        {
            //movement.enabled = false;
            FindAnyObjectByType<GameController>().EndGame();
        }
    }
}
