using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    bool gameHasEnded = false;

    public GameObject completeLevelUI;
    public GameObject gameOverUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    public void CompleteLevel()
    {
        Debug.Log ("Quest Complete!");
        completeLevelUI.SetActive(true);
    }
    

    //public void NextLevel()
    //{
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    //public void LoadScene(string sceneName)
    //{
        //SceneManager.LoadSceneAsync(sceneName);
    //}

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            gameOverUI.SetActive(true);
        }
    }


}
