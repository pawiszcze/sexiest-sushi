using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    GameManagerScript gmngr;
    SamuraiScript player;
    public GameObject spawnedEnemy;
    GameObject myLilBaby;
    FoeScript babyScript;
    SpriteRenderer babySprite;
    public bool isBabyDead;
    bool lostABaby;
    bool offScreen = true;
    int respawnTime;
    bool isActivated = false;
    
    void Start()
    {
        gmngr = GameManagerScript.instance;
        player = SamuraiScript.instance;
        Deactivation();
        ResetTime();
    }

    public void Activation()
    {
        isActivated = true;
        Respawn();
        ResetTime();
    }

    public void Deactivation()
    {
        isActivated = false;
        respawnTime = 0;
    }

    private void OnBecameVisible()
    {
        StopAllCoroutines();
    }

    private void OnBecameInvisible()
    {
        offScreen = true;
        if (isBabyDead && gmngr.difficultyLevel != 1 && isActivated)
        {
            StartCoroutine(RespawnDelay());
        }
    }

    private void ResetTime()
    {
        if (gmngr.difficultyLevel == 2)
        {
            respawnTime = 600;
        }
        else
        {
            respawnTime = 180;
        }
    }

    private void Respawn()
    {
        myLilBaby = Instantiate(spawnedEnemy, transform);
        babyScript = myLilBaby.GetComponent<FoeScript>();
        babySprite = myLilBaby.GetComponent<SpriteRenderer>();
        babyScript.parent = gameObject;
        babyScript.expValue = 0;
        babySprite.sprite = babyScript.spent;
        isBabyDead = false;
    }

    IEnumerator RespawnDelay()
    {
        int i = 0;
        while (i < respawnTime)
        {
            if (!gmngr.isGamePaused)
            {
                i++;
                yield return new WaitForSeconds(0);
            } else {
                yield return null;
            }
        }
        Respawn();
    }
}
