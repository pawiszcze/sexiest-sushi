using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraConfirmButtonScript : MonoBehaviour
{
    public Canvas auraCanvas;
    Samurai_Script player;

    // Start is called before the first frame update
    private void Start()
    {
        player = Samurai_Script.instance;
        gameObject.GetComponent<Button>().onClick.AddListener(FuckenOnClick);
    }

    public void FuckenOnClick()
    {
        Debug.Log("Screen succesfully destroyed via Butten");
        Time.timeScale = 1;
        player.canControl = true;
        Destroy(auraCanvas.gameObject);
    }
}
