using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "LightNingSkill", menuName = "ScrewPuzzle/LightNingSkill")]
public class LightNingSkill : SkillData
{
    public override ISKillState GetSkillState(Skill skill)
    {
        return new LightNingState(skill);
    }
}


