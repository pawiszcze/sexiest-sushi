using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenScript : MonoBehaviour
{
    public static OxygenScript ozScript;
    float width = 1;
    SamuraiScript player;
    public RawImage baseOxy;
    CanvasGroup group;

    // Use this for initialization
    void Start()
    {
        ozScript = this;
        player = SamuraiScript.instance;
        Vector3 scale = new Vector3(1f, width, 1f);
        transform.localScale = scale;
        group = baseOxy.gameObject.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.breathCurrent == player.breathBase)
        {
            group.alpha = 0;
        } else if(group.alpha != 1)
        {
            group.alpha = 1;
        }
        width = player.breathCurrent / player.breathBase;
        Vector3 scale = new Vector3(width, 1f, 1f);
        transform.localScale = scale;
        if (width < 0.3f)
        {
            baseOxy.color = Color.red;
        } else
        {
            baseOxy.color = new Color(0f, 0.5f, 0.1f, 1f);
        }
    }
}
