using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SkillDatabase", menuName = "ScrewPuzzle/SkillDatabase")]
public class SkillDatabase : ScriptableObject
{
    [SerializeReference]
    public List<SkillData> skillDatas = new List<SkillData>();
}


//[Serializable]
//public abstract class SkillData
//{
//    public string idItem;
//    public float Cooldown;
//    public float price;
//    public float selectTiming;

//    public abstract ISKillState GetSkillState(Skill skill);
//}




