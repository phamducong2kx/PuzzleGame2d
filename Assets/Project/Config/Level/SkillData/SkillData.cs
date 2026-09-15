using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SkillStateCache
{
    public static readonly ISKillState defaultSkillState = new DefaultState();
    public static readonly ISKillState drillSkillState = new DrillSkillState();
    public static readonly ISKillState lightningSkillState = new LightNingState();
    public static readonly ISKillState timeSkillState = new TimeSkillState();
}

[Serializable]
public enum SkillType
{
    normal,
    timeSkill,
    dirillSkill,
    lightningSkill
}

[Serializable]
public class SkillData
{
    public string idItem;
    public float Cooldown;
    public float price;
    public float selectTiming;
    public string desscribeSkill;
    public SkillType skillType;

}


