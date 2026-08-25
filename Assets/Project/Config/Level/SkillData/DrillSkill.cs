using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DrillSkill", menuName = "ScrewPuzzle/DrillSkill")]
public class DrillSkill : SkillData
{
    public override ISKillState GetSkillState(Skill skill)
    {
        return new DrillSkillState(skill);
    }
}
