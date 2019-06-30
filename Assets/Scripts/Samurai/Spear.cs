using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

    public static Spear instance;

    Samurai player;
    public Sprite basicSpear;
    public Sprite eSpear;
    public Sprite wSpear;
    public Sprite fSpear;
    public Sprite aSpear;
    public Sprite vSpear;
    
    void Awake() {
        instance = this;
	}

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
