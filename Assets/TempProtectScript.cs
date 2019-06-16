using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempProtectScript : MonoBehaviour
{
    public static TempProtectScript bootsScript;
    public RawImage bootsBase;
    float width = 1;
    SamuraiScript player;
    CanvasGroup group;

    // Use this for initialization
    void Start()
    {
        bootsScript = this;
        player = SamuraiScript.instance;
        Vector3 scale = new Vector3(1f, width, 1f);
        transform.localScale = scale;
        group = bootsBase.gameObject.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.bootsCurrent == player.bootsBase)
        {
            group.alpha = 0;
        }
        else if (group.alpha != 1)
        {
            group.alpha = 1;
        }
        width = player.bootsCurrent / player.bootsBase;
        Vector3 scale = new Vector3(width, 1f, 1f);
        transform.localScale = scale;
    }
}
