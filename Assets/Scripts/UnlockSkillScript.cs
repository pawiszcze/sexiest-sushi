﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkillScript : InteractiveScript {

    public UISkillUnlockedScript attachedSkill;

    public override void Interact()
    {
        if (player.currentSkillPoints > 0)
        {
            if (player.skillLevels[attachedSkill.skillID] == 0)
            {
                attachedSkill.Unlock();
                player.skillLevels[attachedSkill.skillID] = 1;
            }
        }
    }
}
