using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraScript : MonoBehaviour
{
    public int DirectionID; // 1-Up, 2-Down

    SamuraiScript player;
    
    void Start()
    {
        player = SamuraiScript.instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (DirectionID == 1)
        {
            while(player.rig.velocity.y > 0)
            {
                transform.parent.parent = player.transform.parent;
            }
        }
    }
}
