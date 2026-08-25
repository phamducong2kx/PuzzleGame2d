using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSkillState : ISKillState
{
    private Skill skill;
    public TimeSkillState(Skill _skill)
    {
        skill = _skill;
    }

    public void OnEnterState()
    {
        // cộng time , va aniamtion đồng hồ chạy
        GameConfigManager.Instance.skillLogic.InvokeAddTimeSkill();

        // tro ve trang thai default
        InputHandler.Instance.SetStrategy(new DefaultState());


    }

    public void OnExitState()
    {
        //dua isfirsttouch ve false
        skill.isFirstTouch = false;
        //count down dme nguoc o day 
        skill.isCoolDownRunning = true;

        skill.ChangeUpdateUsingSkill(-1);

    }

    public void OntapBolt(Bolt bolt)
    {

    }

    public void OntapHole(Hole hole)
    {

    }

    public void OntapPlank(Plank plank)
    {

    }


}
