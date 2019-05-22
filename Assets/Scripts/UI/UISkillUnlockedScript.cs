using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillUnlockedScript : MonoBehaviour {

    SamuraiScript player;
    public int skillID;
    bool unlocked;
    
    void Start () {
        player = SamuraiScript.instance;
        unlocked = false;
	}

    public void Unlock()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        unlocked = true;
    }    
}
