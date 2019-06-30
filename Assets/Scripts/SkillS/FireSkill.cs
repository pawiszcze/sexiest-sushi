using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSkill : Skill
{
    public static FireSkill instance;

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
        cooldown = 100;
        manaCost = 20;
        skillID = 2;
        effectTime = 10;
        isEffectActive = false;
    }

    public override void Activate()
    {
        base.Activate();
    }

    protected override void Effect()
    {
        /*
         * K8's notes:
         * 
         * Just like in EarthSkillScript, you could change fireInRange to List<damageableScript>.
         */
        foreach(GameObject target in player.fireInRange) {
           
            if (!target.GetComponent<Damageable>().isBurning)
            {                
                StartCoroutine(Extinguish(target, effectTime));
            }
        }
    }

    /*
     * K8's notes:
     * 
     * Situation just like in EarthSkillScript.Free(GameObject other).
     */
    public IEnumerator Extinguish(GameObject other, int time)
    {
        other.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        
        int i = 0;
        Damageable targetScript;
        
            targetScript = other.GetComponent<Damageable>();
        targetScript.isBurning = true;
        
        isEffectActive = true;
        while (i < time * 60)
        {
            if (!gmngr.isGamePaused)
            {
                targetScript.isBurning = true;
                if (i % 60 == 59)
                {
                    if (other != null)
                    {
                        targetScript.GetDamaged(5, null);
                    }
                    else
                    {
                        yield break;
                    }
                }
                i++;
                yield return new WaitForSeconds(0);
            }
            else
            {
                yield return null;
            }
        }
        if (other != null)
        {
            other.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            targetScript.isBurning = false;
        }

        isEffectActive = false;
    }
}