using System.Collections.Generic;
using UnityEngine;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Skill skillPrefab;
    public List<Skill> listSkill = new List<Skill>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //generrate danh sacsh cac cskill
    public void GenerateListSkill()
    {
        if (listSkill.Count > 0) return;

        //duyet danh sách các skill trong skilldatabase
        var list = GameConfigManager.Instance.skillLogic.GetList();
        foreach (var x in list)
        {
            //khoi tao preab skill
            var skill = Instantiate(skillPrefab, transform);

            //set up skilldatabase va set up cac thong os khac
            skill.SetUp(x);

            //add voa list
            listSkill.Add(skill);





        }
    }



}
