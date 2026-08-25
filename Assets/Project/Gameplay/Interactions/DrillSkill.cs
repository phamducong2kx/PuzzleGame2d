using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillSkillState : ISKillState
{
    public Skill skill;
    public Coroutine timeCorotine;
    public int solantapBolt = 0;


    public DrillSkillState(Skill skill)
    {
        this.skill = skill;
    }
    public void OnEnterState()
    {


        //neu nhu nhan lan thu 2 thi phai thoar ra
        if (skill.isFirstTouch == false)
        {
            VisualSelectBolt();

            //chuyen sang state dèault
            InputHandler.Instance.SetStrategy(new DefaultState());

        }
        else
        {
            VisualActiveSkill();
        }
    }

    public IEnumerator timeRotine()
    {
        while (skill.selectTiming > 0 && !skill.activeSkill)
        {
            yield return new WaitForSeconds(1);
            skill.selectTiming -= 1;
        }
    }

    public void OnExitState()
    {
        if (skill.activeSkill)
        {
            skill.isCoolDownRunning = true;
            skill.ChangeUpdateUsingSkill(-1);
            skill.isFirstTouch = false;
            solantapBolt = 0;
            skill.activeSkill = false;
            Time.timeScale = 1f;
        }


    }

    public async void OntapBolt(Bolt bolt)
    {
        ++solantapBolt;
        if (solantapBolt > 1) return;
        //animation
        GameConfigManager.Instance.skillLogic.InvokeDrillSKillEvent(bolt);

        await System.Threading.Tasks.Task.Delay(1000);

        //cat dut lien ket cua bolt voi cac plank hien tai
        bolt.RemoveConnectToHole_OfBolt();

        //sau khi cat dut xong lien ket thi no se co rigibody va roi xuong 
        bolt.transform.localScale = Vector3.one;
        bolt.SetDynamicRigibody();
        bolt.transform.DOKill();
        VisualSelectBolt();
        skill.activeSkill = true;
        InputHandler.Instance.SetStrategy(new DefaultState());
    }

    public void OntapHole(Hole hole)
    {

    }

    public void OntapPlank(Plank plank)
    {

    }

    public void VisualSelectBolt()
    {
        var listBolt = LevelLoader.Instance.spawnedBolts;
        //danh sach cac bolt visual binh thuong
        foreach (var x in listBolt)
        {
            x.VisualDefaultSkill();
        }

        //time hoat dong binh thuong chay lai corotine
        UIManager.Instance.gameplayPannel.timeSystem.isRunning = true;
        Time.timeScale = 1f;

        //dong coroutine


        //disactive square
        UIManager.Instance.gameplayPannel.Square_Skill.SetActive(false);

        //thay doi layer cho drillSkill
        skill.CloseCanvasSortingLayer();
    }
    public void VisualActiveSkill()
    {
        var listBolt = LevelLoader.Instance.spawnedBolts;
        foreach (var x in listBolt)
        {
            x.VisualDrillSkill();
        }

        //dong bang time luon
        UIManager.Instance.gameplayPannel.timeSystem.isRunning = false;
        // Time.timeScale = 0f;

        //kich hoat time star courotine
        timeCorotine = GameStateManager.Instance.StartCoroutine(timeRotine());

        //active square
        UIManager.Instance.gameplayPannel.Square_Skill.SetActive(true);

        //thay doi layer cho drillSkill
        skill.OpenCanvasSortingLayer();
    }
}
