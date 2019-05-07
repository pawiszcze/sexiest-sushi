using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveScript : MonoBehaviour {

    protected Samurai_Script player;
    protected GameManagerScript gmngr;
    protected string interactionName;
    protected bool isInteracting;
    protected GameObject newGO;

    // Use this for initialization
    protected virtual void Start () {
        player = Samurai_Script.instance;
        gmngr = GameManagerScript.instance;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null)
        {
            player = Samurai_Script.instance;
        }

        //Debug.Log(collision.gameObject);
        if (collision.gameObject.GetComponent<Samurai_Script>())
        {
            //Debug.Log("GO should be created");
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
        if (collision.gameObject.layer == 8)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.GetComponent<Collider2D>(), true);
        }

        if (player == null)
        {
            player = Samurai_Script.instance;
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

    // Update is called once per frame
    virtual public void Interact () {
        isInteracting = true;
        /*if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }*/
	}
}
