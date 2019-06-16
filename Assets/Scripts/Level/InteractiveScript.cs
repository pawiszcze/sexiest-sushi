using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveScript : MonoBehaviour {

    protected SamuraiScript player;
    protected GameManagerScript gmngr;
    protected string interactionName;
    protected bool isInteracting;
    protected GameObject newGO;

    protected virtual void Start () {
        player = SamuraiScript.instance;
        gmngr = GameManagerScript.instance;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Armor rack touched");
        if (player == null)
        {
            player = SamuraiScript.instance;
        }

        if (collision.gameObject.tag == "Player")
        {
            if (transform.childCount == 0)
            {
                newGO = new GameObject("myTextGO");
                newGO.transform.SetParent(this.transform);
                TextMesh myText = newGO.AddComponent<TextMesh>();
                myText.transform.position = new Vector3(transform.position.x, transform.position.y, gameObject.transform.position.z-1);
                myText.characterSize = 0.15f;
                myText.anchor = TextAnchor.MiddleCenter;
                myText.text = "Press E to " + interactionName;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (player == null)
        {
            player = SamuraiScript.instance;
        }

        if (collision.gameObject == player.gameObject)
        {
            player.canInteract = true;
            player.interactWith = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            player.canInteract = false;
            player.interactWith = null;
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }
    
    virtual public void Interact () {
        isInteracting = true;
	}
}
