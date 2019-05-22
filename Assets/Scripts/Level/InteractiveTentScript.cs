using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTentScript : InteractiveScript {
    
    public Canvas AuraUnlockCanvas;
    private Canvas createdCanvas;

    override protected void Start () {
        base.Start();
        interactionName = "meditate";
	}
	
	override public void Interact()
    {
        base.Interact();
       if(createdCanvas == null)
        {
            createdCanvas = Instantiate(AuraUnlockCanvas, gmngr.transform);
            Time.timeScale = 0;
            player.canControl = false;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (createdCanvas != null)
            {
                Destroy(createdCanvas.gameObject);
                Time.timeScale = 1;
                player.canControl = true;
            }
        }
    }
}
