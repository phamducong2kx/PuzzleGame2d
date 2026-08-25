using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeSkill", menuName = "ScrewPuzzle/TimeSkill")]
public class TimeSkill : SkillData
{
    public override ISKillState GetSkillState(Skill skill)
    {
        return new TimeSkillState(skill);
    }
}
