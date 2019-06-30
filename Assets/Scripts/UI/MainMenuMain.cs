using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuMain : MonoBehaviour
{
    public static MainMenuMain instance;
    CanvasGroup grupa;
    SettingsPanel settingsPanel;
    public Button resumeButton;
    public Button settingsButton;
    public Button mainMenuButton;
    public Button exitButton;
    public Button returnButton;
    GameManager gameManager;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        settingsPanel = SettingsPanel.instance;
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
        grupa = settingsPanel.gameObject.GetComponent<CanvasGroup>();
    }

    void Resume()
    {
       SceneManager.LoadScene(0);
    }

    void Settings()
    {
        grupa.alpha = 1;
        grupa.interactable = true;
        grupa.blocksRaycasts = true;
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
        grupa.alpha = 0;
        grupa.interactable = false;
        grupa.blocksRaycasts = false;
    }
}
