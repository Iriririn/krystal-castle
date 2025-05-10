using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    //public static bool GameIsPaused = false;
    //public GameObject GameOverPanel;
    public GameController gameController;
    public PlayerHealth health;
    //bool gameHasEnded = true;

    public void Update()
    {
        gameController.EndGame();
        //health.PlayerDeath();
    }

    public void Restart()
    {
        //Debug.Log("Restarting...");
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMenu()
    {
        //Debug.Log("Loading Menu...");
        SceneManager.LoadScene("MainMenu");

    }

    public void QuitGame()
    {
        //Debug.Log("Quit Game");
        Application.Quit();
    }
}
