using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthSkillScript : SkillScript
{

    public static EarthSkillScript instance;

    private void Awake()
    {
        instance = this;
    }

    void Init()
    {
        m_image = GetComponent<Image>();
        m_image.sprite = activeImage;
    }

    protected override void Start()
    {
        base.Start();
        skillID = 0;
        cooldown = 100;
        manaCost = 10;
        effectTime = 1;
    }

    public override void Activate()
    {
        base.Activate();
    }

    protected override void Effect()
    {
        foreach (GameObject target in player.earthInRange)
        {
                target.GetComponent<FoeScript>().currentSpeed = 0;
                StartCoroutine(Free(target));
        }
    }

    private IEnumerator Free(GameObject other)
    {
        int i = 0;
        isEffectActive = true;
        FoeScript enemyScript = other.GetComponent<FoeScript>();
        while (i < effectTime * 60)
        {
            if (!gmngr.isGamePaused)
            {
                i++;
                yield return new WaitForSeconds(0);
            } else
            {
                yield return null;
            }
        }
        if (other != null)
        {
            enemyScript.currentSpeed = enemyScript.maxSpeed;
        }
        isEffectActive = false;
    }
}
