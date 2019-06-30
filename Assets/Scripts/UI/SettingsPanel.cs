using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour {

    public static SettingsPanel instance;
    
	void Awake() {
        instance = this;
	}
}
