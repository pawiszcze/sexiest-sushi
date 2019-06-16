using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveMakeUIScript : InteractiveScript
{
    public Canvas AssignedCanvas;
    protected Canvas createdCanvas;
    public string interName;

    override protected void Start()
    {
        base.Start();
        interactionName = interName;
    }

    override public void Interact()
    {
        base.Interact();
        if (createdCanvas == null)
        {
            createdCanvas = Instantiate(AssignedCanvas, gmngr.transform);
            Time.timeScale = 0;
            player.canControl = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
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
