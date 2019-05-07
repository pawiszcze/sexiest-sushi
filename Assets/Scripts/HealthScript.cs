using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    public static HealthScript healthScript;
    float height = 1;
    Samurai_Script player;

    // Use this for initialization
    void Start()
    {
        healthScript = this;
        player = Samurai_Script.instance;
        Vector3 scale = new Vector3(1f, height, 1f);
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        height = player.currentHealth / player.maxHealth;
        Vector3 scale = new Vector3(1f, height, 1f);
        transform.localScale = scale;
    }
}
