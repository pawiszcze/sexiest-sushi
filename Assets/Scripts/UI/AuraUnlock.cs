using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AuraUnlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text textBox;
    public RawImage frame;
    public int skillID;
    public Texture2D activeSprite;
    protected Samurai player;
    Gorinto tower;
    SpriteRenderer towerSprite;
    public static bool skillChanged = false;
    bool isActive = false;
    public static string defaultDescriptionText;
    string descriptionText;
    
    void Start()
    {
        player = Samurai.instance;
        tower = Gorinto.instance;
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
            if (gameObject.GetComponent<RawImage>() != null) { gameObject.GetComponent<RawImage>().texture = activeSprite; }
            
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
                descriptionText = "Earth Aura description placeholder";
                break;
            case 1:
                descriptionText = "Water Aura description placeholder";
                break;
            case 2:
                descriptionText = "Fire Aura description placeholder";
                break;
            case 3:
                descriptionText = "Air Aura description placeholder";
                break;
            case 4:
                descriptionText = "Void Aura description placeholder";
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
