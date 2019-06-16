using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AuraUnlockScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text textBox;
    public RawImage frame;
    public int skillID;
    public Texture2D activeSprite;
    protected SamuraiScript player;
    GorintoScript tower;
    SpriteRenderer towerSprite;
    public static bool skillChanged = false;
    bool isActive = false;
    public static string defaultDescriptionText;
    string descriptionText;
    
    void Start()
    {
        player = SamuraiScript.instance;
        tower = GorintoScript.instance;
        towerSprite = tower.GetComponent<SpriteRenderer>();
        defaultDescriptionText = " ";
        Button click = gameObject.AddComponent<Button>();
        if(skillID == 5 && skillChanged != true){
            isActive = true;
            click.onClick.AddListener(delegate { ThisOneIsActive(); skillChanged = true; });
            ThisOneIsActive();
        }
        else if (player.skillLevels[skillID] > 2)
        {
            gameObject.GetComponent<RawImage>().texture = activeSprite;
            click.onClick.AddListener(delegate { ThisOneIsActive(); skillChanged = true; });
        }

        if(player.auraSelected == skillID)
        {
            ThisOneIsActive();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActive)
        {
            if (player.auraSelected != skillID)
            {
                SelectText();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData){}

    public void ThisOneIsActive()
    {
        player.auraSelected = skillID;
        Debug.Log(skillID);
        frame.transform.position = new Vector3(transform.position.x, transform.position.y, frame.transform.position.z);
        SelectText();
        textBox.text = descriptionText;
        towerSprite.sprite = tower.sprites[skillID];
    }

    private void SelectText()
    {
        switch (skillID)
        {
            case 0:
                descriptionText = "Earth Aura: Gives the projectiles a chance to be infused with Earth Energy. Earth Projectiles will stop enemies around it from moving for a few seconds.";
                break;
            case 1:
                descriptionText = "Water Aura: Gives the projectiles a chance to be infused with Water Energy. Water Projectiles will whatever.";
                break;
            case 2:
                descriptionText = "Fire Aura: Gives the projectiles a chance to be infused with Fire Energy. Fire Projectiles will burn enemies, duh.";
                break;
            case 3:
                descriptionText = "Air Aura: Gives the projectiles a chance to be infused with Air Energy. Air Projectiles will pierce enemies? I guess.";
                break;
            case 4:
                descriptionText = "Void Aura: Gives the projectiles a chance to be infused with Void Energy. Scares enemies off on hit or something.";
                break;
            case 5:
                descriptionText = "No Aura Selected: The projectiles will have no additional effects.";
                break;
            default:
                descriptionText = " ";
                break;
        }
    }
}
