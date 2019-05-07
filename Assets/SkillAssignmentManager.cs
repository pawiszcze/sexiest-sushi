using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillAssignmentManager : MonoBehaviour {

    public static SkillAssignmentManager instance;

    Samurai_Script player;

    public TextAsset textFile;
    public string[] table1;
    public Button earthBase;
    public Button waterBase;
    public Button fireBase;
    public Button airBase;
    public Button voidBase;
    public Text numberOfSkillpoints;

    public Sprite earthSprite;
    public Sprite waterSprite;
    public Sprite fireSprite;
    public Sprite airSprite;
    public Sprite voidSprite;
    public Sprite frame;

	void Start () {
        player = Samurai_Script.instance;
        instance = this;

        table1 = textFile.text.Split('|');
        GetSkillpointNumber();

        if(player.skillLevels[0] != 0)
        {
            for (int i = 0; i < player.skillLevels[0]; i++)
            {
                earthBase.GetComponent<SkillButton>().Unlocked(0, frame, earthSprite);
                earthBase.interactable = false;
                if (i < 4)
                {
                    earthBase = earthBase.GetComponent<SkillButton>().nextButton;
                    earthBase.onClick.AddListener(delegate { Unlock(earthBase, 0); });
                    earthBase.interactable = true;
                }
            }
        }

        if(player.skillLevels[1] != 0)
        {
            for (int i = 0; i < player.skillLevels[1]; i++)
            {
                waterBase.GetComponent<SkillButton>().Unlocked(1, frame, waterSprite);
                waterBase.interactable = false;
                if (i <4)
                {
                    waterBase = waterBase.GetComponent<SkillButton>().nextButton;
                    waterBase.onClick.AddListener(delegate { Unlock(waterBase, 1); });
                    waterBase.interactable = true;
                }
            }

        }

        if (player.skillLevels[2] != 0)
        {

            for (int i = 0; i < player.skillLevels[2]; i++)
            {
                fireBase.GetComponent<SkillButton>().Unlocked(2, frame, fireSprite);
                fireBase.interactable = false;
                if (i <4)
                {
                    fireBase = fireBase.GetComponent<SkillButton>().nextButton;
                    fireBase.onClick.AddListener(delegate { Unlock(fireBase, 2); });
                    fireBase.interactable = true;
                }
            }
        }

        if (player.skillLevels[3] != 0)
        {
            for (int i = 0; i < player.skillLevels[3]; i++)
            {
                airBase.GetComponent<SkillButton>().Unlocked(3, frame, airSprite);
                airBase.interactable = false;
                if (i <4)
                {
                    airBase = airBase.GetComponent<SkillButton>().nextButton;
                    airBase.onClick.AddListener(delegate { Unlock(airBase, 3); });
                    airBase.interactable = true;
                }
            }

        }

        if (player.skillLevels[4] != 0)
        {
            for (int i = 0; i < player.skillLevels[4]; i++)
            {
                voidBase.GetComponent<SkillButton>().Unlocked(4, frame, voidSprite);
                voidBase.interactable = false;
                if (i <4)
                {
                    voidBase = voidBase.GetComponent<SkillButton>().nextButton;
                    voidBase.onClick.AddListener(delegate { Unlock(voidBase, 4); });
                    voidBase.interactable = true;
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
                    clicked.GetComponent<SkillButton>().Unlocked(0, frame, earthSprite);
                    Debug.Log(player.skillLevels[column]);
                    if (player.skillLevels[column] < 4)
                    {
                        earthBase = earthBase.GetComponent<SkillButton>().nextButton;
                        earthBase.onClick.AddListener(delegate { Unlock(earthBase, 0); });
                        earthBase.interactable = true;
                    }
                    break;
                case 1:
                    clicked.GetComponent<SkillButton>().Unlocked(1, frame, waterSprite);
                    if (player.skillLevels[column] < 4)
                    {
                        waterBase = waterBase.GetComponent<SkillButton>().nextButton;
                        waterBase.onClick.AddListener(delegate { Unlock(waterBase, 1); });
                    }
                    break;
                case 2:

                    clicked.GetComponent<SkillButton>().Unlocked(2, frame, fireSprite);
                    if (player.skillLevels[column] < 4)
                    {
                        fireBase = fireBase.GetComponent<SkillButton>().nextButton;
                        fireBase.onClick.AddListener(delegate { Unlock(fireBase, 2); });
                    }
                    break;
                case 3:
                    clicked.GetComponent<SkillButton>().Unlocked(3, frame, airSprite);
                    if (player.skillLevels[column] < 4)
                    {
                        airBase = airBase.GetComponent<SkillButton>().nextButton;
                        airBase.onClick.AddListener(delegate { Unlock(airBase, 3); });
                    }
                    break;
                case 4:
                    clicked.GetComponent<SkillButton>().Unlocked(4, frame, voidSprite);
                    if (player.skillLevels[column] < 4)
                    {
                        voidBase = voidBase.GetComponent<SkillButton>().nextButton;
                        voidBase.onClick.AddListener(delegate { Unlock(voidBase, 4); });
                    }
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
            clicked.interactable = false;
            player.skillLevels[column]++;
            player.currentSkillPoints--;
            GetSkillpointNumber();
        }
    }

    void GetSkillpointNumber()
    {
        numberOfSkillpoints.text = System.String.Concat("Available Skillpoints: ", player.currentSkillPoints);
    }
}
