using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanelScript : MonoBehaviour {

    public static SettingsPanelScript instance;
    
	void Awake() {
        instance = this;
	}
}
