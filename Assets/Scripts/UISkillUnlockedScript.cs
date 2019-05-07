using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillUnlockedScript : MonoBehaviour {

    Samurai_Script player;
    public int skillID;
    bool unlocked;
    
    void Start () {
        player = Samurai_Script.instance;
        unlocked = false;
	}


    public void Unlock()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        unlocked = true;
    }
    
}
