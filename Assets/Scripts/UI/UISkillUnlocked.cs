using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillUnlocked : MonoBehaviour {

    Samurai player;
    public int skillID;
    bool unlocked;
    
    void Start () {
        player = Samurai.instance;
        unlocked = false;
	}

    public void Unlock()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        unlocked = true;
    }    
}
