using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public static MainMenu instance;

    PauseMenu specialsMenu;
    SettingsPanel settingsMenu;
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
        specialsMenu = PauseMenu.instance;
        settingsMenu = SettingsPanel.instance;
        CanvasGroup specialGrupa = specialsMenu.GetComponent<CanvasGroup>();
        CanvasGroup settingGrupa = settingsMenu.GetComponent<CanvasGroup>();
        specialGrupa.alpha = 0;
        specialGrupa.interactable = false;
        settingGrupa.alpha = 0;
        settingGrupa.interactable = false;
        settingGrupa.blocksRaycasts = false;
        //pauseMenu.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0.5f * System.Convert.ToInt32(isGamePaused));
    }
}
