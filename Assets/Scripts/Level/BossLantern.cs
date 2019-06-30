using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLantern : MonoBehaviour {

    public Boss attachedBoss;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    SpriteRenderer thisSprite;
    Vector3 bossLocation;
    public bool isBossDead;

	void Start () {
        bossLocation = attachedBoss.transform.position;
        isBossDead = false;
        thisSprite = GetComponent<SpriteRenderer>();
	}
	
    public void DimLantern()
    {
        isBossDead = true;
        thisSprite.sprite = inactiveSprite;
    }

    public void HitLantern()
    {
        if (isBossDead)
        {
            thisSprite.sprite = activeSprite;
            isBossDead = false;
            Boss newBoss = Instantiate(attachedBoss, bossLocation, Quaternion.identity);
            newBoss.spawnLantern = this;
            bossLocation = newBoss.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Blade")
        {
            HitLantern();
        }
    }
}