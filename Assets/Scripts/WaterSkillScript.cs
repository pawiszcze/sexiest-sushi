using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterSkillScript : SkillScript
{
    int healingAmount = 30;
    public static WaterSkillScript instance;

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
        cooldown = 250;
        skillID = 1;
        manaCost = 25;
    }

    public override void Activate()
    {
        if (player.currentHealth < player.maxHealth)
        {
            base.Activate();

        }
        
    }

    protected override void Effect()
    {
        if (player.currentHealth < (player.maxHealth - healingAmount))
        {
            player.currentHealth += healingAmount;
        }
        else
        {
            player.currentHealth = player.maxHealth;
        }
    }

}
