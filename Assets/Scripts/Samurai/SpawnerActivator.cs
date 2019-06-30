using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActivator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.layer == 9 && collision.gameObject.tag == "Respawn")
        {
            collision.gameObject.GetComponent<Spawner>().Activation();
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && collision.gameObject.tag == "Respawn")
        {
            collision.gameObject.GetComponent<Spawner>().Deactivation();
        }
    }
}