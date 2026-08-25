using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;




public class SkillLogic : MonoBehaviour
{

    [SerializeField] public SkillDatabase skillDatabase;

    public event Action AddTimeSkill;

    //envent lien quan den skill drill
    public event Action<Bolt> DrillSkillEvent;
    public event Action<Plank> LightNingSKillEvent;

    public void InvokeAddTimeSkill()
    {
        AddTimeSkill?.Invoke();
    }


    //invoke event drillSkillEvent;
    public void InvokeDrillSKillEvent(Bolt bolt)
    {
        DrillSkillEvent?.Invoke(bolt);
    }

    //invoke lightningskill
    public void InvokeLightNingSkill(Plank plank)
    {
        LightNingSKillEvent?.Invoke(plank);
    }
    private void Awake()
    {


    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    //lay danh sacsh
    public List<SkillData> GetList()
    {
        return skillDatabase.skillDatas;
    }


    //lay so luong skill theo id
    public int GetAmountSkillById(string id)
    {
        var listSkillProgress = SaveManager.Data.listSkill;
        var skill = listSkillProgress.FirstOrDefault(x => x.idSkill.Equals(id));
        if (skill == null) return -1;
        return skill.amount;
    }


    //dua het danh sachs skill voa data
    public void SetUPSkill()
    {
        var list = SaveManager.Data.listSkill;
        list.Clear();
        SaveManager.SaveData();
        Debug.Log("so luong trong list la " + list.Count);
    }



    void Start()
    {
      //  SetUPSkill();
        var list = SaveManager.Data.listSkill;
        //Debug.Log("danh sacsh casc skill hjient ai");
        foreach (var skill in list)
        {
            Debug.Log("skill  co id la " + skill.idSkill + " so luong la " + skill.amount + " , cooldown la " + skill.cooldownRemaining);
        }
    }


    void Update()
    {

    }


}
