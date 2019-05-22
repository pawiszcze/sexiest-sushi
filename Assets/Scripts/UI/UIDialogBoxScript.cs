using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogBoxScript : MonoBehaviour {

    public static UIDialogBoxScript instance;
    public Image samuraiImage;
    public Image otherImage;

    private void Awake()
    {
        instance = this;
        samuraiImage = transform.GetChild(0).GetComponent<Image>();
        otherImage = transform.GetChild(1).GetComponent<Image>();
    }
}
