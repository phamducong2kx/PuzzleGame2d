using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillData", menuName = "ScrewPuzzle/SkillData")]
public abstract class SkillData : ScriptableObject
{
    public string idItem;
    public float Cooldown;
    public float price;
    public float selectTiming;
    public string desscribeSkill;

    public abstract ISKillState GetSkillState(Skill skill);
}
