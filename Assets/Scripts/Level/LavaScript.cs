using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    SamuraiScript player;
    GameManagerScript gmngr;
    FireSkillScript burnies;

    private void Start()
    {
        gmngr = GameManagerScript.instance;
        player = SamuraiScript.instance;
        burnies = FireSkillScript.instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.touchingLava = true;
            if (gmngr.currentStageProgress < 5)
            {
                player.GetDamaged(player.currentHealth, GetComponent<Collider2D>());
            }
            else
            {
                if (player.repairBoots != null)
                {
                    player.StopCoroutine(player.repairBoots);
                }
                player.burningBoots = player.StartCoroutine(player.BurnOff());
            }
        }
    }


    private void Update()
    {
        if (player.lavaVulnerable)
        {
            if (gmngr.currentStageProgress >= 5 && player.touchingLava && !player.isBurning)
            {
                player.isBurning = true;
                player.GetDamaged(10, null);
                player.BurningThingie = burnies.StartCoroutine(burnies.Extinguish(player.gameObject, 5));
            }
        }
        if (player.touchingWater && player.BurningThingie != null)
        {
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            player.isBurning = false;
            burnies.StopCoroutine(player.BurningThingie);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.repairBoots = player.StartCoroutine(player.BootsRestore());
            if (player.burningBoots != null)
            {
                player.StopCoroutine(player.burningBoots);
            }
            player.touchingLava = false;
        }
    }
}
