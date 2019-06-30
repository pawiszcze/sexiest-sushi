using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkill : Interactive {

    public UISkillUnlocked attachedSkill;

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
