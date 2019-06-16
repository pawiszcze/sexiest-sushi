using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLightScript : MonoBehaviour
{
    GameManagerScript gmngr;
    public int levelID;
    public Sprite activeSprite;
    SpriteRenderer thisSprite;

    // Start is called before the first frame update
    void Start()
    {
        thisSprite = transform.GetComponent<SpriteRenderer>();
        gmngr = GameManagerScript.instance;
    }

    // Update is called once per frame
    public void LightUp(int x)
    {
        transform.GetChild(x).GetComponent<SpriteRenderer>().sprite = activeSprite;
    }
}
