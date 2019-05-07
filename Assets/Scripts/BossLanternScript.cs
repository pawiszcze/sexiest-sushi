using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLanternScript : MonoBehaviour {

    public BossScript attachedBoss;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    Vector3 bossLocation;
    public bool isBossDead;

	// Use this for initialization
	void Start () {
        bossLocation = attachedBoss.transform.position;
        isBossDead = false;
	}
	
    public void DimLantern()
    {
        isBossDead = true;
        GetComponent<SpriteRenderer>().sprite = inactiveSprite;
    }

    public void HitLantern()
    {
        Debug.Log("BunP");
        if (isBossDead)
        {
            GetComponent<SpriteRenderer>().sprite = activeSprite;
            isBossDead = false;
            BossScript newBoss = Instantiate(attachedBoss, bossLocation, Quaternion.identity);
            newBoss.spawnLantern = this;
            bossLocation = newBoss.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SuordScript>())
        {
            HitLantern();
        }
    }
}
