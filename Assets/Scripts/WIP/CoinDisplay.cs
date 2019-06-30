using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour {

    public static CoinDisplay instance;

	// Use this for initialization
	void Awake() {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
