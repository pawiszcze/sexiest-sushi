using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

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
        /*
         * K8's notes:
         * 
         * Situation like in SpearProjectileScript:
         * You are performing multiple calls of Getcomponent to get the same component
         * of AudioSource and do something with its variables.
         * 
         * Create a local variable here to cache result of GetComponent call
         * and use this variable to perform operations on AudioSource members.
         * And don't forget about null check!
         * E.g.
         *  
         *  var audioSource = gameObject.GetComponent<AudioSource>();
         *  if (audioSource != null)
         *  {
         *      audioSource.clip = backgroundMusic;
         *      audioSource.loop = true;
         *      audioSource.Play();
         *      audioSource.volume = 0.02f;
         *  }
         * 
         */
        gameObject.GetComponent<AudioSource>().clip = backgroundMusic;
        gameObject.GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<AudioSource>().volume = 0.02f;
        isGamePaused = false;
        player = Samurai.instance;
        coinDisplay = CoinDisplay.instance;
        pauseMenu = PauseMenu.instance;
        settingsMenu = SettingsPanel.instance;
        //settingsCanvas = settingsMenu.gameObject.GetComponent<CanvasGroup>();


        /*
         * K8's notes:
         * 
         * AFAIK this number of 10 means the amount of levels in your game,
         * and this number is the same as the numbers in lines 29 and 30 above.
         * 
         * You could create a variable (or even a constant) for this number 
         * and use it instead of writing everywhere the value of 10.
         * I'm pointing this out because if you need to change this value, you would
         * have to find all places that value is used. There's a high change that you could
         * miss some places and later had hard time debugging what is wrong.
         * 
         * Generally, such 'magic numbers' are so-called bad smell ;o Avoid them.
         * 
         */
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
        //Debug.Log(temp);
        if (x > 0)
        {
            lights[level].LightUp(x - 1);
        }
        coinDisplay.GetComponent<Text>().text = coinsCollected[level] + "/" + coinsInLevels[level];
    }

}
