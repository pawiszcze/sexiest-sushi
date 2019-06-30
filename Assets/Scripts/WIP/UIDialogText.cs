using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDialogText : MonoBehaviour {

    public static UIDialogText instance;

    private void Awake()
    {
        instance = this;
    }
}
