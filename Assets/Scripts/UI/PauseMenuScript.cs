using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

    public static PauseMenuScript instance;
    SettingsPanelScript settingsPanel;
    CanvasGroup settingsGroup;
    public Button resumeButton;
    public Button settingsButton;
    public Button mainMenuButton;
    public Button exitButton;
    public Button returnButton;
    GameManagerScript gameManager;
    
	void Awake() {
        instance = this;
	}

    private void Start()
    {
        gameManager = GameManagerScript.instance;
        settingsPanel = SettingsPanelScript.instance;
        settingsGroup = settingsPanel.gameObject.GetComponent<CanvasGroup>();
        Button btn1 = resumeButton.GetComponent<Button>();
        Button btn2 = settingsButton.GetComponent<Button>();
        Button btn3 = mainMenuButton.GetComponent<Button>();
        Button btn4 = exitButton.GetComponent<Button>();
        Button btn5 = returnButton.GetComponent<Button>();
        btn1.onClick.AddListener(Resume);
        btn2.onClick.AddListener(Settings);
        btn3.onClick.AddListener(MainMenu);
        btn4.onClick.AddListener(Exit);
        btn5.onClick.AddListener(Return);
    }

    void Resume()
    {
        gameManager.TogglePause();
    }

	void Settings()
    {
        settingsGroup.alpha = 1;
        settingsGroup.interactable = true;
        settingsGroup.blocksRaycasts = true;
    }

    void MainMenu()
    {
        SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadScene(1);
    }

    void Exit()
    {
        Application.Quit();
    }

    void Return()
    {
        settingsGroup.alpha = 0;
        settingsGroup.interactable = false;
        settingsGroup.blocksRaycasts = false;
    }
}
