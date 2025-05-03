using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameOverMenu GameOverMenu;
    int maxPlatform = 0;

    public void GameOver()
    {
        GameOverMenu.Setup(maxPlatform);
    }
    
}
