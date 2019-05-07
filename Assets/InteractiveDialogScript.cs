using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveDialogScript : InteractiveScript
{

    public TextAsset textFile;
    public Sprite otherDialogSprite;
    public CanvasGroup DialogBox;
    public GameObject dBoxPrefab;
    GameObject dBoxClone;
    UIDialogTextScript dText;
    UIDialogBoxScript dBox;
    int whichLine;
    string[] dialogSeries;
    bool areYouTalking;
    bool isTyping;
    float typeSpeedDefault = 0.12f;
    float typeSpeed;
    CanvasGroup mainUICanvasGroup;

    override protected void Start()
    {
        base.Start();
        mainUICanvasGroup = UIOverlayScript.instance.GetComponent<CanvasGroup>();
        interactionName = "talk";
        dialogSeries = textFile.text.Split('|');
        whichLine = 0;
        areYouTalking = true;
        typeSpeed = typeSpeedDefault;
    }
    
    override public void Interact()
    {
        base.Interact();
        newGO.GetComponent<TextMesh>().text = " ";
        mainUICanvasGroup.alpha = 0;
        dBoxClone = Instantiate(dBoxPrefab, gmngr.transform);
        dBox = dBoxClone.GetComponentInChildren<UIDialogBoxScript>();
        dText = dBoxClone.GetComponentInChildren<UIDialogTextScript>();
        DialogBox = dBoxClone.GetComponentInChildren<CanvasGroup>();
        whichLine = 0;
        areYouTalking = true;
        isTyping = false;
        dialogSeries = textFile.text.Split('|');
        Time.timeScale = 1 * System.Convert.ToInt32(gmngr.isGamePaused);
        gmngr.isGamePaused = !gmngr.isGamePaused;
        player.canControl = !player.canControl;
        DialogBox.alpha = 1 * System.Convert.ToInt32(gmngr.isGamePaused);
        dBox.samuraiImage.sprite = player.dialogSprite;
        dBox.otherImage.sprite = otherDialogSprite;
        AdvanceDialog();
    }

    private void Update()
    {
        if (isInteracting)
        {
            if (!player.canControl)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    if (player.interactWith == this)
                    {
                        if (gmngr.isGamePaused)
                        {
                            if (whichLine < dialogSeries.Length)
                            {
                                if (typeSpeed == 0)
                                {
                                    typeSpeed = typeSpeedDefault;
                                }
                                else
                                {
                                    typeSpeed = 0f;
                                }
                                AdvanceDialog();

                            }
                            else
                            {
                                newGO.GetComponent<TextMesh>().text = "Press E to " + interactionName;
                                mainUICanvasGroup.alpha = 1;
                                Destroy(dBoxClone);
                                Time.timeScale = 1 * System.Convert.ToInt32(gmngr.isGamePaused);
                                gmngr.isGamePaused = !gmngr.isGamePaused;
                                player.canControl = !player.canControl;
                                DialogBox.alpha = 1 * System.Convert.ToInt32(gmngr.isGamePaused);
                                isInteracting = false;
                            }
                        }
                    }
                }
            }
        }
    }

    private void AdvanceDialog()
    {
        areYouTalking = dialogSeries[whichLine].StartsWith("Y");
        string textToType = "";
        IEnumerator typing = FastType("");

        if (!isTyping)
        {
            if (areYouTalking)
            {
                dBox.samuraiImage.color = new Color(1, 1, 1, 1);
                dBox.GetComponent<UIDialogBoxScript>().otherImage.color = new Color(0, 0, 0, 0);
                dText.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
            }
            else
            {
                dBox.samuraiImage.color = new Color(0, 0, 0, 0);
                dBox.otherImage.color = new Color(1, 1, 1, 1);
            }
            dialogSeries[whichLine] = dialogSeries[whichLine].Remove(0, 1);
            textToType = dialogSeries[whichLine];
            typing = FastType(textToType);
            StartCoroutine(typing);
        }
        else
        {
            dText.GetComponent<Text>().text = textToType;
        }
    }

    private IEnumerator FastType(string text)
    {
        int i = 0;
        string currentText = " ";
        while (i < text.Length)
        {
            isTyping = true;
            currentText = string.Concat(currentText, text[i]);
            Debug.Log(text.Length);
            dText.GetComponent<Text>().text = currentText;
            float start = Time.realtimeSinceStartup;

            while (Time.realtimeSinceStartup < start + typeSpeed)
            {
                yield return null;
            }
            i++;
        }
        whichLine++;
        isTyping = false;
    }
}
