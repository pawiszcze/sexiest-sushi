﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillScript : MonoBehaviour {

    protected Samurai_Script player;
    protected bool assigned = false;
    public Image cd_image;
    protected Image m_image;
    protected int cooldown;
    protected int skillID;
    protected int effectTime;
    public int manaCost;
    protected bool isCoolingDown;
    protected bool isEffectActive;
    protected Sprite activeImage;
    protected Sprite cooldownImage;

    protected GameManagerScript gmngr;

    protected virtual void Effect()
    {

    }

    protected virtual void Start()
    {
        isCoolingDown = false;
        if (!assigned)
        {
            player = Samurai_Script.instance;
            assigned = true;
            gmngr = GameManagerScript.instance;
        }
    }
    
	virtual public void Activate() {
        if (player.skillLevels[skillID] > 0)
        {
            if (player.currentMana >= manaCost && !isCoolingDown)
            {
                player.currentMana = player.currentMana - manaCost;
                Vector3 scale = new Vector3(1f, 0, 1f);
                transform.localScale = scale;
                Effect();

                StartCoroutine(SkillCooldown());


            }
        }
    }

    private IEnumerator SkillCooldown()
    {
        int i = 0;
        isCoolingDown = true;
        while (i < cooldown) {
            if (!gmngr.isGamePaused)
            {
                i++;
                cd_image.GetComponent<Image>().fillAmount = i * 1.0f / cooldown;
                yield return new WaitForSeconds(0);
            }
            else
            {
                Debug.Log(cd_image.GetComponent<Image>().fillAmount);
                yield return null;
            }
        }

        Vector3 scale = new Vector3(1f, 1f, 1f);

        transform.localScale = scale;
        isCoolingDown = false;
    }
}
