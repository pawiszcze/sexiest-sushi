using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoidSkill : Skill
{
    public static VoidSkill instance;

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
        cooldown = 500;
        manaCost = 15;
        skillID = 4;
    }

    public override void Activate()
    {
        base.Activate();/*
        if (m_image.sprite == activeImage)
        {
            m_image.sprite = cooldownImage;
        }
        else if (m_image.sprite == cooldownImage)
        {
            m_image.sprite = activeImage;
        }
        */
    }
}
