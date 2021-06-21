using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    AudioManager audioManager;

    public Samurai player;
    CanvasGroup settingsCanvas;
    CoinDisplay coinDisplay;
    PauseMenu pauseMenu;
    SettingsPanel settingsMenu;
    public GameObject pauseMenuPrefab;
    GameObject pauseMenuClone;
    public int[] coinsCollected;
    public int[] coinsInLevels;
    public CoinLight[] lights;
    public bool isGamePaused;
    public int currentStageProgress;
    //public AudioClip backgroundMusic;
    public AudioClip coinSound;
    public int difficultyLevel = 2;

    int numberOfLevels = 10;

    private void Awake()
    {
        instance = this;
        coinsCollected = new int[numberOfLevels];
        coinsInLevels = new int[numberOfLevels];
        coinsCollected[0] = 0;
        coinsInLevels[0] = 0;
        currentStageProgress = 0;
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        audioManager.playMusic(audioManager.backgroundMusic, 0.2f);
       
        isGamePaused = false;
        player = Samurai.instance;
        coinDisplay = CoinDisplay.instance;
        pauseMenu = PauseMenu.instance;
        settingsMenu = SettingsPanel.instance;
        //settingsCanvas = settingsMenu.gameObject.GetComponent<CanvasGroup>();

        for (int i=0; i< numberOfLevels; i++)
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
            pauseMenu = pauseMenuClone.gameObject.GetComponentInChildren<PauseMenu>();
            settingsMenu = pauseMenuClone.gameObject.GetComponentInChildren<SettingsPanel>();
            settingsCanvas = settingsMenu.gameObject.GetComponent<CanvasGroup>();
        }
        else if (settingsCanvas.interactable)
        {
            settingsCanvas.alpha = 0;
            settingsCanvas.interactable = false;
            settingsCanvas.blocksRaycasts = false;
            isGamePaused = !isGamePaused;
        } else
        {
            Destroy(pauseMenuClone);
        }
    }

    public void UpdateCoins(int level)
    {
        float tempCoins = coinsCollected[level];
        float tempMax = coinsInLevels[level];
        float temp = (tempCoins / tempMax) * 10;
        int x = Mathf.FloorToInt(temp);
        if (x > 0)
        {
            lights[level].LightUp(x - 1);
        }
        coinDisplay.GetComponent<Text>().text = coinsCollected[level] + "/" + coinsInLevels[level];
    }

}
