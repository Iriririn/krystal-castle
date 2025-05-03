using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject GameOverPanel;

    public void Update()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void Restart ()
    {
        Debug.Log("Restarting...");
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        SceneManager.LoadScene("MenuScene");

    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    internal void Setup(int maxPlatform)
    {
        gameObject.SetActive(true);
        throw new NotImplementedException();
    }
}
