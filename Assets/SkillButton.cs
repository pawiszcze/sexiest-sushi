using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    public static SkillButton active;

    SkillAssignmentManager mngr;

    public int skillID;
    public Button nextButton;
    private Samurai_Script player;
    private DescriptionBlockScript block;

    string skillName;
    string skillDesc;

    private Text title;
    private Text description;

    private bool isMouseOver;

    private void Awake()
    { 
        player = Samurai_Script.instance;
    }

    private void Start()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;

        mngr = SkillAssignmentManager.instance;
        block = DescriptionBlockScript.instance;
        title = block.transform.GetChild(0).gameObject.GetComponent<Text>();
        description = block.transform.GetChild(1).gameObject.GetComponent<Text>();
        isMouseOver = false;
    }

    public void Unlocked(int column, Sprite frame, Sprite newSprite)
    {
        GameObject newSpriteHack = new GameObject();
        newSpriteHack.transform.parent = transform;
        Image newSpriteHackImage = newSpriteHack.gameObject.AddComponent<Image>();
        newSpriteHackImage.transform.localPosition = new Vector3(0, 0, -0.5f);
        newSpriteHackImage.sprite = newSprite;
        newSpriteHackImage.rectTransform.localScale = Vector3.one;

        GameObject clickedFrame = new GameObject();
        clickedFrame.transform.parent = transform;
        Image skillFrame = clickedFrame.gameObject.AddComponent<Image>();
        skillFrame.sprite = frame;
        skillFrame.transform.localPosition = new Vector3(0, 0, -1f);
        skillFrame.rectTransform.localScale = Vector3.one;

        if (player.skillLevels[column] < 4) {
            nextButton.interactable = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        Debug.Log("assgrab");
        active = this;
        block.GetComponent<CanvasGroup>().alpha = 1;
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        if (active == this)
        {
            block.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    private void Update()
    {
        float offsetVert = 0f;
        float offsetHori = 0f;
        if (isMouseOver)
        {
            string[] table2 = mngr.table1[skillID].Split('/');
            title.text = table2[0];
            title.fontSize = 18;
            description.text = table2[1];

            if (Input.mousePosition.y + 5f + block.gameObject.GetComponent<RectTransform>().sizeDelta.y < Screen.height) {

                offsetVert = 5f;
            } else
            {
                offsetVert = -5f;
            }

            if (Input.mousePosition.x + block.gameObject.GetComponent<RectTransform>().sizeDelta.x / 2 + 3f < Screen.height)
            {

                offsetHori = Screen.width / 50 + 3f;
            }
            else
            {
                offsetHori = -Screen.width / 50 - 3f;
            }

            block.transform.position = Input.mousePosition + new Vector3(offsetHori, offsetVert, 0);
        }

        Debug.Log("Screen height: " + Screen.height + ", mouse plus offset pos: " + Input.mousePosition.y + 5);
    }
}
