using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDialogTextScript : MonoBehaviour {

    public static UIDialogTextScript instance;

    private void Awake()
    {
        instance = this;
    }
}
