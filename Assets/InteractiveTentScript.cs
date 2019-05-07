using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTentScript : InteractiveScript {

    //GameManagerScript gmngr;
    public Canvas AuraUnlockCanvas;
    private Canvas createdCanvas;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        interactionName = "meditate";
       //gmngr = GameManagerScript.instance;
	}
	
	override public void Interact()
    {
        base.Interact();
       // gmngr.TogglePause();
       if(createdCanvas == null)
        {
            createdCanvas = Instantiate(AuraUnlockCanvas, gmngr.transform);
            Time.timeScale = 0;
            player.canControl = false;
        }
        /*if(AuraUnlockCanvas.GetComponent<CanvasGroup>().alpha == 0)
        {
            AuraUnlockCanvas.GetComponent<CanvasGroup>().alpha = 1;
            Time.timeScale = 0;
        }*/
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            /*if (AuraUnlockCanvas.GetComponent<CanvasGroup>().alpha == 1)
            {
                player.canControl = true;
                AuraUnlockCanvas.GetComponent<CanvasGroup>().alpha = 0;
                Time.timeScale = 1;
   
            }*/

        if(createdCanvas != null)
            {
                Destroy(createdCanvas.gameObject);
                Time.timeScale = 1;
                player.canControl = true;
            }
        }
    }
}
