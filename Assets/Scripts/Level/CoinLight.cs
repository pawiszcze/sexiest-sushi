using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLight : MonoBehaviour
{
    GameManager gmngr;
    public int levelID;
    public Sprite activeSprite;
    SpriteRenderer thisSprite;
    
    void Start()
    {
        thisSprite = transform.GetComponent<SpriteRenderer>();
        gmngr = GameManager.instance;
    }

    /*
     * K8's question:
     * What is this method doing?
     */
    public void LightUp(int x)
    {
        transform.GetChild(x).GetComponent<SpriteRenderer>().sprite = activeSprite;
    }
}
