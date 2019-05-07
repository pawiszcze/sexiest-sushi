using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuMainScript : MonoBehaviour
{

    public static MainMenuMainScript instance;
    SettingsPanelScript settingsPanel;
    public Button resumeButton;
    public Button settingsButton;
    public Button mainMenuButton;
    public Button exitButton;
    public Button returnButton;
    GameManagerScript gameManager;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameManager = GameManagerScript.instance;
        settingsPanel = SettingsPanelScript.instance;
        Button btn1 = resumeButton.GetComponent<Button>();
        //Button btn2 = settingsButton.GetComponent<Button>();
        //Button btn3 = mainMenuButton.GetComponent<Button>();
        //Button btn4 = exitButton.GetComponent<Button>();
        //Button btn5 = returnButton.GetComponent<Button>();
        btn1.onClick.AddListener(Resume);
        //btn2.onClick.AddListener(Settings);
        //btn1.onClick.AddListener(Resume);
        ///btn1.onClick.AddListener(Resume);
        //btn5.onClick.AddListener(Return);
    }

    void Resume()
    {
       SceneManager.LoadScene(0);
    }

    void Settings()
    {
        settingsPanel.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        settingsPanel.gameObject.GetComponent<CanvasGroup>().interactable = true;
        settingsPanel.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void MainMenu()
    {

    }

    void Exit()
    {
        Application.Quit();
    }

    void Return()
    {
        settingsPanel.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        settingsPanel.gameObject.GetComponent<CanvasGroup>().interactable = false;
        settingsPanel.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
