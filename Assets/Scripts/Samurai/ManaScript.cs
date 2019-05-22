using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaScript : MonoBehaviour {

    public static ManaScript manaScript;
    float height = 1;
    SamuraiScript player;

    // Use this for initialization
    void Start () {
        manaScript = this;
        player = SamuraiScript.instance;
        Vector3 scale = new Vector3(1f, height, 1f);
        transform.localScale = scale;
    }
	
	// Update is called once per frame
	void Update () {
        height = player.currentMana / player.maxMana;
        Vector3 scale = new Vector3(1f, height, 1f);
        transform.localScale = scale;
	}
}
