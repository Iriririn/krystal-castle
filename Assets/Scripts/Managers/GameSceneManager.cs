using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    [SerializeField] private int Score = 0;
    [SerializeField]  private int Lives = 3;

    private int _totalCoins;
    private Vector3 playerPosition;
    //UI
    private VisualElement _root;
    private Label _scoreLabel;
    private Label _livesLabel;
    private Button _mainMenu;

    private void OnEnable()
    {
        Instance = this; //create a running instance of Sample Game Manager

        //get the UI elements
        _root = GetComponent<UIDocument>().rootVisualElement;
        //query from the root visual element, basically look for the labels
        _scoreLabel = _root.Q<Label>("Score");
        _livesLabel = _root.Q<Label>("Lives");
        _mainMenu = _root.Q<Button>("MainMenu");

        _mainMenu = _root.Q<Button>("MainMenu");
        _mainMenu.clicked += MainMenuClicked;
    }

    private void Start()
    {
        //when game level is loaded, check if there is existing playerprefs
        if (!IsNewGame())
            LoadGame(); //if it is not a new game, load the existing playerprefs data

        //set up initial UI text on screen
        _scoreLabel.text = "Score: " + Score.ToString();
        _livesLabel.text = "Lives: " + Lives.ToString();
        //find the total coins to determine the win conditon
        _totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;

    }
    private void MainMenuClicked()
    {
        PlayerPrefs.Save(); //save all preferences before loadng to start screen
        SceneManager.LoadScene("StartScreen"); // load the start screen
    }

    public void AddScore(int scoreValue)
    {
        Score += scoreValue; // add to total score
        _scoreLabel.text = "Score: " + Score.ToString(); // update the UI
        PlayerPrefs.SetInt("score", Score); //save the data to player prefs
        Debug.Log("SCORE: " + Score.ToString());
    }

    public void SubtractLives(int livesValue)
    {
        Lives -= livesValue; //subtract to total lives
        _livesLabel.text = "Lives: " + Lives.ToString(); //update the UI
        PlayerPrefs.SetInt("lives", Lives); // save the data
        Debug.Log("LIVES: " + Lives.ToString());
    }

    public void AddLives(int livesValue)
    {
        Lives += livesValue; // add to total lives
        _livesLabel.text = "Lives: " + Lives.ToString(); // update the UI
        PlayerPrefs.SetInt("lives", Lives); //save the data
        Debug.Log("LIVES: " + Lives.ToString());
    }

    public bool GameOver()
    {
        if (Lives <= 0)
        {
            //lose
            PlayerPrefs.SetString("status","You Lose!"); //set win/lose status to carry over to End Screen
            SceneManager.LoadScene("EndScreen"); // load the end screen
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GameWin()
    {
        if (Score >= _totalCoins) //check if the score is higher than or equal to the total coins found in the level
        {
            PlayerPrefs.SetString("status", "You Win!"); //set win/lose status to carry over to End Screen
            SceneManager.LoadScene("EndScreen");  // load the end screen
        }
    }

    void LoadGame()
    {
        //get save data
        Score = PlayerPrefs.GetInt("score", 0); //if score does not exist, default to 0
        Lives = PlayerPrefs.GetInt("lives", 0); //if lives does not exist, default to 0

        if (Lives <= 0) //if player died, reset values
        {
            Lives = 3;
            PlayerPrefs.SetInt("lives", Lives); //set lives key as the value of Lives variable
            Score = 0;
            PlayerPrefs.SetInt("score", Score); //set the value of score key as the value of Score variable
            
            //delete old player positions
            PlayerPrefs.DeleteKey("playerPositionX"); 
            PlayerPrefs.DeleteKey("playerPositionY");
            PlayerPrefs.DeleteKey("playerPositionZ");
        }
    }

    public bool IsNewGame()
    {
        if (PlayerPrefs.HasKey("playerStarted"))
        {
            return false; //load the game if playerStarted key does not exist
        }
        else
        {
            PlayerPrefs.SetInt("playerStarted", 1); // 1 in this context means TRUE
            return true;
        }
    }

    public Vector3 GetPlayerPosition()
    {
        playerPosition.x = PlayerPrefs.GetFloat("playerPositionX", 0);
        playerPosition.y = PlayerPrefs.GetFloat("playerPositionY", 0);
        playerPosition.z = PlayerPrefs.GetFloat("playerPositionZ", 0);
        return playerPosition;
    }
}
