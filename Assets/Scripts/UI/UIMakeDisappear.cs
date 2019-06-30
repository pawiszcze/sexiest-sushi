using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMakeDisappear : MonoBehaviour
{
    public Canvas canvasToGo;
    Samurai player;
    
    private void Start()
    {
        player = Samurai.instance;
        gameObject.GetComponent<Button>().onClick.AddListener(FuckenOnClick);
    }

    public void FuckenOnClick()
    {
        Time.timeScale = 1;
        player.canControl = true;
        Destroy(canvasToGo.gameObject);
    }
}
