using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript instance;

    public SamuraiScript player;
    CanvasGroup settingsCanvas;
    CoinDisplayScript coinDisplay;
    PauseMenuScript pauseMenu;
    SettingsPanelScript settingsMenu;
    public GameObject pauseMenuPrefab;
    GameObject pauseMenuClone;
    public int[] coinsCollected;
    public int[] coinsInLevels;
    public CoinLightScript[] lights;
    public bool isGamePaused;
    public int currentStageProgress;
    public AudioClip backgroundMusic;
    public AudioClip coinSound;
    public int difficultyLevel = 2;

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
        player = SamuraiScript.instance;
        coinDisplay = CoinDisplayScript.instance;
        pauseMenu = PauseMenuScript.instance;
        settingsMenu = SettingsPanelScript.instance;
        //settingsCanvas = settingsMenu.gameObject.GetComponent<CanvasGroup>();



        for (int i=0; i<10; i++)
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
        Debug.Log(temp);
        if (x > 0)
        {
            lights[level].LightUp(x - 1);
        }
        coinDisplay.GetComponent<Text>().text = coinsCollected[level] + "/" + coinsInLevels[level];
    }

}
