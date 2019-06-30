using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIItemDescription : MonoBehaviour
{
    public int itemID;
    public Text titleBox;
    public Text descrBox;
    public TextAsset descriptions;
    bool isActive;
    CanvasGroup cGroup;
    protected GameManager gmngr;
    Button click;

    // Start is called before the first frame update
    void Start()
    {
        gmngr = GameManager.instance;
        cGroup = GetComponent<CanvasGroup>();
        Activate(gmngr.currentStageProgress);
        click = gameObject.AddComponent<Button>();
        click.onClick.AddListener(delegate { ChangeText(); });
    }

    // Update is called once per frame
    void ChangeText()
    {
        string correctText = descriptions.text;
        string newText = correctText.Substring(correctText.IndexOf(string.Concat(itemID + ":"))+2);
        titleBox.text = newText.Substring(0, newText.IndexOf("|"));
        descrBox.text = newText.Substring(newText.IndexOf("|") + 1, newText.IndexOf("*")-titleBox.text.Length-1);
        //Debug.Log(correctText.IndexOf("*"));
    }

    private void Activate(int count)
    {
        if (count >= itemID)
        {
            cGroup.alpha = 1;
            cGroup.interactable = true;
            cGroup.blocksRaycasts = true;
        }
    }
}
