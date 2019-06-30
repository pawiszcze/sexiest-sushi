using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthSkill : Skill
{

    public static EarthSkill instance;

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
        /*
         * K8's notes:
         * 
         * List player.earthInRange could be of type List<FoeScript> instead of List<GameObject>.
         * As a result, you don't need to call GetComponent below in order to get FoeScript instance,
         * these instructions wolud look like this:
         * 
         *  foreach (FoeScript target in player.earthInRange)
         *  {
         *          target.currentSpeed = 0;
         *          StartCoroutine(Free(target.gameObject));    // FoeScript at the top level is a MonoBehaviour
         *  }
         */
        foreach (GameObject target in player.earthInRange)
        {
                target.GetComponent<Foe>().currentSpeed = 0;
                StartCoroutine(Free(target));
        }
    }

    /*
     * K8's notes:
     * 
     * You could change other type from GameObject to FoeScript.
     * It would let you get rid of this GetComponent() call,
     * and you still would have access to gameObject as mentioned above.
     */
    private IEnumerator Free(GameObject other)
    {
        int i = 0;
        isEffectActive = true;
        Foe enemyScript = other.GetComponent<Foe>();
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
