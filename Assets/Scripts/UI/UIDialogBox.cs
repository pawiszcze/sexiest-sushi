using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogBox : MonoBehaviour {

    public static UIDialogBox instance;
    public Image samuraiImage;
    public Image otherImage;

    private void Awake()
    {
        instance = this;
        samuraiImage = transform.GetChild(0).GetComponent<Image>();
        otherImage = transform.GetChild(1).GetComponent<Image>();
    }
}
