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
    private Samurai player;
    private DescriptionBlock block;
    Vector2 blockSize;
    private CanvasGroup blockCanvas;
    private string skillName;
    private string skillDesc;


    private Text title;
    private Text description;

    private bool isMouseOver;

    private void Awake()
    { 
        player = Samurai.instance;
    }

    private void Start()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;

        mngr = SkillAssignmentManager.instance;
        block = DescriptionBlock.instance;
        blockSize = block.gameObject.GetComponent<RectTransform>().sizeDelta;
        blockCanvas = block.GetComponent<CanvasGroup>();
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
        active = this;
        blockCanvas.alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        if (active == this)
        {
            blockCanvas.alpha = 0;
        }
    }

    private void Update()
    {
        float offsetVert;
        float offsetHori;
        if (isMouseOver)
        {
            if(skillID == 0 || skillID == 5 || skillID == 10 || skillID ==15 ||skillID ==20)
            {
                offsetVert = -30f;
            } else
            {
                offsetVert = 30f;
            }
            string[] table2 = mngr.table1[skillID].Split('/');
            title.text = table2[0];
            title.fontSize = 18;
            description.text = table2[1];

            if (skillID < 5)
            {
                //blockSize = block;
                offsetHori = 200f ;
            } else if (skillID > 19)
            {
                offsetHori = -200f;
            } else
            {
                offsetHori = 0;
            }

            if (Input.mousePosition.y + 5f + blockSize.y < Screen.height) {
              //  offsetVert = 5f;
            } else
            {
               // offsetVert = -5f;
            }

            //if (Input.mousePosition.x + blockSize.x / 2 + 3f < Screen.height)
            //{
            //    offsetHori = Screen.width / 50 + 3f + blockSize.x/2;
            //}
            //else
            //{
            //    offsetHori = -Screen.width / 50 - 3f - blockSize.x/2;
            //}

            block.transform.position = Input.mousePosition + new Vector3(offsetHori, offsetVert, 0);
        }
    }
}
