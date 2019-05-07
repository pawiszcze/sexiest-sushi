using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript instance;

    Samurai_Script player;
    CoinDisplayScript coinDisplay;
    PauseMenuScript pauseMenu;
    SettingsPanelScript settingsMenu;
    public GameObject pauseMenuPrefab;
    GameObject pauseMenuClone;
    public int[] coinsCollected;
    public int[] coinsInLevels;
    public bool isGamePaused;
    public int currentStageProgress;
    public AudioClip backgroundMusic;
    public AudioClip coinSound;

    private void Awake()
    {
        instance = this;
        coinsCollected = new int[10];
        coinsInLevels = new int[10];
        coinsCollected[0] = 0;
        coinsInLevels[0] = 0;
        currentStageProgress = 0;
    }

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().clip = backgroundMusic;
        gameObject.GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<AudioSource>().volume = 0.02f;
        isGamePaused = false;
        player = Samurai_Script.instance;
        coinDisplay = CoinDisplayScript.instance;
        pauseMenu = PauseMenuScript.instance;
        settingsMenu = SettingsPanelScript.instance;

        for(int i=0; i<10; i++)
        {
            coinsInLevels[i] += coinsCollected[i];
        }
    }

    void Update () {
        if (player.canControl && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = 1 * System.Convert.ToInt32(!isGamePaused);
        if (isGamePaused)
        {
            pauseMenuClone = Instantiate(pauseMenuPrefab);
            pauseMenu = pauseMenuClone.gameObject.GetComponentInChildren<PauseMenuScript>();
            settingsMenu = pauseMenuClone.gameObject.GetComponentInChildren<SettingsPanelScript>();
        }
        else if (settingsMenu.gameObject.GetComponent<CanvasGroup>().interactable)
        {
            settingsMenu.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            settingsMenu.gameObject.GetComponent<CanvasGroup>().interactable = false;
            settingsMenu.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            isGamePaused = !isGamePaused;
        } else
        {
            Destroy(pauseMenuClone);
        }
    }

    public void UpdateCoins(int level)
    {
        coinDisplay.GetComponent<Text>().text = coinsCollected[level] + "/" + coinsInLevels[level];
        //Debug.Log(level);
    }
}
