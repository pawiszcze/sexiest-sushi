using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public static MainMenu instance;

    PauseMenuScript specialsMenu;
    SettingsPanelScript settingsMenu;
    //public bool isGamePaused;

    private void Awake()
    {
        instance = this;
    }

    /*void Init()
    {
        isGamePaused = false;
    }*/

    private void Start()
    {
        specialsMenu = PauseMenuScript.instance;
        settingsMenu = SettingsPanelScript.instance;
        specialsMenu.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        specialsMenu.gameObject.GetComponent<CanvasGroup>().interactable = false;
        settingsMenu.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        settingsMenu.gameObject.GetComponent<CanvasGroup>().interactable = false;
        settingsMenu.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //pauseMenu.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0.5f * System.Convert.ToInt32(isGamePaused));
    }
}
