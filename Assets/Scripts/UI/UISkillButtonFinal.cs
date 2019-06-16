using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillButtonFinal : MonoBehaviour
{
    public Sprite activeSprite;
    public bool isRight;
    public Sprite inactiveSprite;
    public static UISkillButtonFinal thisOneActive;
    public Text title;
    public Text desc;
    public RawImage toChange;
    public TextAsset descriptions;
    public Button yesButton;
    int columnNumba;
    int itemID; string correctText; string newText;

    public bool isActive;

    private void Start()
    {
        isActive = false;
    }

    public void Ini(int column)
    {
        correctText = descriptions.text;
        columnNumba = column;
        itemID = (column + 1) * 10;
        newText = correctText.Substring(correctText.IndexOf(string.Concat(itemID + ":")) + 3);
        title.text = newText.Substring(0, newText.IndexOf("|"));
        desc.text = newText.Substring(newText.IndexOf("|") + 1, newText.IndexOf("*") - title.text.Length - 1);
        toChange.texture = inactiveSprite.texture;
    }

    public void Click()
    {
        if (!isActive)
        {
            if (thisOneActive != null)
            {
                thisOneActive.Dezactivate();
            }
            Activate();
        } 
    }

    public void Dezactivate()
    {
        isActive = false;
        toChange.texture = inactiveSprite.texture;
        thisOneActive = null;
    }

    void Activate()
    {
        correctText = descriptions.text;
        itemID = 2 * columnNumba;
        if (isRight)
        {
            itemID++;
        }
        newText = correctText.Substring(correctText.IndexOf(string.Concat(itemID + ":")) + 2);
        title.text = newText.Substring(0, newText.IndexOf("|"));
        desc.text = newText.Substring(newText.IndexOf("|") + 1, newText.IndexOf("*") - title.text.Length - 1);
        yesButton.interactable = true;
        isActive = true;
        toChange.texture = activeSprite.texture;
        thisOneActive = this;
    }
}