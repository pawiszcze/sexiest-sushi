using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIReturn : MonoBehaviour
{
    public static UIReturn instance;
    SkillAssignmentManager mngr;

    public UISkillButtonFinal yinButton;
    public UISkillButtonFinal yangButton;
    public Button yesButton;
    private CanvasGroup group;
    public Sprite[] colors;
    public RawImage changedSprite;
    int column;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mngr = SkillAssignmentManager.instance;
        group = gameObject.GetComponent<CanvasGroup>();
        Button click = gameObject.AddComponent<Button>();
        click.onClick.AddListener(delegate { GoAway(false); });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (group.alpha == 1)
            {
                GoAway(false);
            }
        }
    }

    public void GoAway(bool confirmed)
    {
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
        if (confirmed)
        {
            Debug.Log("Bing");
            if (yinButton.isActive || yangButton.isActive)
            {
                mngr.Procedure(mngr.finalButtons[column], column);
                Debug.Log("Bang");
            }
        }
        yesButton.interactable = false;
        yinButton.Dezactivate();
        yangButton.Dezactivate();
    }

    public void ShowYourself(int spriteNumber)
    {
        column = spriteNumber;
        yinButton.Ini(spriteNumber);
        yangButton.Ini(spriteNumber);

        changedSprite.texture = colors[spriteNumber].texture;
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }
}
