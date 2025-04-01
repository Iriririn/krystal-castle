using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class mainMenuEvents : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private VisualElement _userInterface;


    private void Awake()
    {
        _userInterface = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        _startButton = _userInterface.Q<Button>("Start");
        _startButton.clicked += OnStartClicked;

        _loadGameButton = _userInterface.Q<Button>("LoadGame");
        _loadGameButton.clicked += OnLoadGameClicked;

        _exitButton = _userInterface.Q<Button>("ExitButton");
        _exitButton.clicked += OnExitClicked;
    }

    private void OnStartClicked()
    {
        //_saveStatus.text = ""; 
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("GameScene"); 
    }

    private void OnLoadGameClicked()
    {
        if (PlayerPrefs.HasKey("playerStarted")) 
        {
            //_saveStatus.text = "";
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            //_saveStatus.text = "No Save Game Found";
        }

    }

    private void OnExitClicked()
    {
        //_saveStatus.text = "";
        Application.Quit();
    }
    
}
