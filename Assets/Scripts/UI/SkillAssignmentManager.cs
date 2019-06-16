using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillAssignmentManager : MonoBehaviour {

    public static SkillAssignmentManager instance;

    SamuraiScript player;
    UIReturnScript screen;

    public TextAsset textFile;
    public string[] table1;
    public Button[] finalButtons;
    public Button earthBase;
    public Button waterBase;
    public Button fireBase;
    public Button airBase;
    public Button voidBase;
    public Text numberOfSkillpoints;

    public Sprite[] sprites;
    public Sprite earthSprite;
    public Sprite waterSprite;
    public Sprite fireSprite;
    public Sprite airSprite;
    public Sprite voidSprite;
    public Sprite frame;

    SkillButton earthButt;
    SkillButton waterButt;
    SkillButton fireButt;
    SkillButton airButt;
    SkillButton voidButt;

	void Start () {
        player = SamuraiScript.instance;
        screen = UIReturnScript.instance;
        earthButt = earthBase.GetComponent<SkillButton>();
        waterButt = waterBase.GetComponent<SkillButton>();
        fireButt = fireBase.GetComponent<SkillButton>();
        airButt = airBase.GetComponent<SkillButton>();
        voidButt = voidBase.GetComponent<SkillButton>();
        instance = this;

        table1 = textFile.text.Split('|');
        GetSkillpointNumber();

        if(player.skillLevels[0] != 0)
        {
            for (int i = 0; i < player.skillLevels[0]; i++)
            {
                earthButt.Unlocked(0, frame, earthSprite);
                earthBase.interactable = false;
                if (i < 4)
                {
                    
                    earthBase = earthButt.nextButton;
                    earthBase.onClick.AddListener(delegate { Unlock(earthBase, 0); });
                    earthBase.interactable = true;
                    earthButt = earthBase.GetComponent<SkillButton>();
                }
            }
        }

        if(player.skillLevels[1] != 0)
        {
            for (int i = 0; i < player.skillLevels[1]; i++)
            {
                waterButt.Unlocked(1, frame, waterSprite);
                waterBase.interactable = false;
                if (i <4)
                {
                    waterBase = waterButt.nextButton;
                    waterBase.onClick.AddListener(delegate { Unlock(waterBase, 1); });
                    waterBase.interactable = true;
                    waterButt = waterBase.GetComponent<SkillButton>();
                }
            }

        }

        if (player.skillLevels[2] != 0)
        {

            for (int i = 0; i < player.skillLevels[2]; i++)
            {
                fireButt.Unlocked(2, frame, fireSprite);
                fireBase.interactable = false;
                if (i <4)
                {
                    fireBase = fireButt.nextButton;
                    fireBase.onClick.AddListener(delegate { Unlock(fireBase, 2); });
                    fireBase.interactable = true;
                    fireButt = fireBase.GetComponent<SkillButton>();
                }
            }
        }

        if (player.skillLevels[3] != 0)
        {
            for (int i = 0; i < player.skillLevels[3]; i++)
            {
                airButt.Unlocked(3, frame, airSprite);
                airBase.interactable = false;
                if (i <4)
                {
                    airBase = airButt.nextButton;
                    airBase.onClick.AddListener(delegate { Unlock(airBase, 3); });
                    airBase.interactable = true;
                    airButt = airBase.GetComponent<SkillButton>();
                }
            }

        }

        if (player.skillLevels[4] != 0)
        {
            for (int i = 0; i < player.skillLevels[4]; i++)
            {
                voidButt.Unlocked(4, frame, voidSprite);
                voidBase.interactable = false;
                if (i <4)
                {
                    voidBase = voidButt.nextButton;
                    voidBase.onClick.AddListener(delegate { Unlock(voidBase, 4); });
                    voidBase.interactable = true;
                    voidButt = voidBase.GetComponent<SkillButton>();
                }

            }
        }
    }
	
    void Unlock(Button clicked, int column)
    {
        if (player.currentSkillPoints > 0)
        {
            switch (column)
            {
                case 0:
                    if (player.skillLevels[column] < 4)
                    {
                        Procedure(clicked, column);
                        earthBase = earthButt.nextButton;
                        earthBase.onClick.AddListener(delegate { Unlock(earthBase, 0); });
                        earthButt = earthBase.GetComponent<SkillButton>();
                    } else
                    {
                        screen.ShowYourself(column);
                    }
                    break;
                case 1:
                    if (player.skillLevels[column] < 4)
                    {
                        Procedure(clicked, column);
                        waterBase = waterButt.nextButton;
                        waterBase.onClick.AddListener(delegate { Unlock(waterBase, 1); });
                        waterButt = waterBase.GetComponent<SkillButton>();
                    }
                    else
                    {
                        screen.ShowYourself(column);
                    }
                    break;
                case 2:

                    if (player.skillLevels[column] < 4)
                    {
                        Procedure(clicked, column);
                        fireBase = fireButt.nextButton;
                        fireBase.onClick.AddListener(delegate { Unlock(fireBase, 2); });
                        fireButt = fireBase.GetComponent<SkillButton>();
                    }
                    else
                    {
                        screen.ShowYourself(column);
                    }
                    break;
                case 3:
                    if (player.skillLevels[column] < 4)
                    {
                        Procedure(clicked, column);
                        airBase = airButt.nextButton;
                        airBase.onClick.AddListener(delegate { Unlock(airBase, 3); });
                        airButt = airBase.GetComponent<SkillButton>();
                    }
                    else
                    {
                        screen.ShowYourself(column);
                    }
                    break;
                case 4:
                    if (player.skillLevels[column] < 4)
                    {
                        Procedure(clicked, column);
                        voidBase = voidButt.nextButton;
                        voidBase.onClick.AddListener(delegate { Unlock(voidBase, 4); });
                        voidButt = voidBase.GetComponent<SkillButton>();
                    }
                    else
                    {
                        screen.ShowYourself(column);
                    }
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
            
        }
    }

    public void Procedure(Button clicked, int column)
    {
        clicked.GetComponent<SkillButton>().Unlocked(column, frame, sprites[column]);
        clicked.interactable = false;
        player.skillLevels[column]++;
        player.currentSkillPoints--;
        GetSkillpointNumber();
    }

    void GetSkillpointNumber()
    {
        numberOfSkillpoints.text = System.String.Concat("Available Skillpoints: ", player.currentSkillPoints);
    }
}