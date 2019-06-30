    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirSkill : Skill
{
    public static AirSkill instance;

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
        cooldown = 60;
        manaCost = 5;
        skillID = 3;
        effectTime = 10;
        isEffectActive = false;
    }

    public override void Activate()
    {
        base.Activate();
    }

    protected override void Effect()
    {
        base.Effect();
        StartCoroutine(WearOff());
    }

    private IEnumerator WearOff()
    {
        int i = 0;
        player.maxSpeed *= 2;
        isEffectActive = true;
        while (i < effectTime * 60)
        {
            if (!gmngr.isGamePaused)
            {
                i++;
                yield return new WaitForSeconds(0);
            }
            else
            {
                yield return null;
            }
        }

        player.maxSpeed /= 2;
        isEffectActive = false;
    }
}
