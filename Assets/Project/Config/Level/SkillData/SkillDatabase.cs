using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SkillDatabase", menuName = "ScrewPuzzle/SkillDatabase")]
public class SkillDatabase : ScriptableObject
{

    public List<SkillData> skillDatas = new List<SkillData>();
}




