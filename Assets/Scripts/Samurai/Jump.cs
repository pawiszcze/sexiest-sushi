using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    AudioManager audioManager;
    int jumpsAvailable;

    void Start()
    {
        jumpsAvailable = 1;
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpAction();
        }
    }

    void JumpAction()
    {
        //jumpsAvailable--;
        audioManager.playSound(audioManager.playerJumpSound);
        /*rig.gravityScale = 2;
        rig.drag = 2;
        rig.velocity = new Vector2(rig.velocity.x, 0);
        rig.AddForce(new Vector2(0, jumpSpeed));*/
    }
}
