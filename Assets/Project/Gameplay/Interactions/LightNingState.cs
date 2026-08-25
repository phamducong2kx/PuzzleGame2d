using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LightNingState : ISKillState
{
    public Skill skill;
    public int solantapPlank = 0;
    public LightNingState(Skill _skill)
    {
        skill = _skill;
    }




    public void OnEnterState()
    {


        //neu nhu nhan lan thu 2 thi phai thoar ra
        if (skill.isFirstTouch == false)
        {
            //chuyển sang trạng thái chọn plank
            VisualSelectPlank();

            //nếu đang nhấn skill mà nhấn lần nữa sẽ mất skill
            InputHandler.Instance.SetStrategy(new DefaultState());

        }
        else
        {
            VisualActiveSkill();
        }
    }



    public void OnExitState()
    {
        if (skill.activeSkill)
        {
            skill.isCoolDownRunning = true;
            skill.ChangeUpdateUsingSkill(-1);
            skill.isFirstTouch = false;
            solantapPlank = 0;
            skill.activeSkill = false;
            Time.timeScale = 1f;
        }


    }

    public void OntapBolt(Bolt bolt)
    {
        //tap bolt không ăn thua nhé 
    }

    public void OntapHole(Hole hole)
    {

    }

    public async void OntapPlank(Plank plank)
    {
        ++solantapPlank;
        if (solantapPlank > 1) return;
        //kich hoat su kien lightningKSill

        GameConfigManager.Instance.skillLogic.InvokeLightNingSkill(plank);

        //duyetj danh sacsh casc hole của plank
        foreach (var hole in plank.holes)
        {
            //tim bolt current hien tai cua no
            var boltCurrent = hole.currentBolt;
            if (boltCurrent != null)
            {
                plank.RemoveBoltConnection(boltCurrent);
                boltCurrent.plankHoles.Remove(hole);

            }



        }
        //cho plank co hasFallen = true
        if (plank.hasFallen == false) plank.hasFallen = true;
        //sau do dua thang nay ve poool
        var prefabPlank = ObjectPooler.Instance.GetPrefabObject(plank.gameObject);
        ObjectPooler.Instance.Despawn(prefabPlank, plank.gameObject);


        await Task.Delay(1000);




        VisualSelectPlank();
        skill.activeSkill = true;
        InputHandler.Instance.SetStrategy(new DefaultState());
        UIManager.Instance.gameplayPannel.winLoseSystem.Evaluate();


    }

    public void VisualSelectPlank()
    {
        var listPlank = LevelLoader.Instance.spawnedPlanks;

        //danh sach cac bolt visual binh thuong
        foreach (var x in listPlank)
        {
            x.VisualDefaultSkill();
            x.SetDynamicRigibody();
            PhongtothunhoAnimation.KillAnimation(x.transform);

        }

        //time hoat dong binh thuong chay lai corotine
        UIManager.Instance.gameplayPannel.timeSystem.isRunning = true;
        Time.timeScale = 1f;

        //disactive square
        UIManager.Instance.gameplayPannel.Square_Skill.SetActive(false);

        //thay doi layer cho drillSkill
        skill.CloseCanvasSortingLayer();
    }
    public void VisualActiveSkill()
    {
        var listPlank = LevelLoader.Instance.spawnedPlanks;

        //duyet danh sach
        foreach (var x in listPlank)
        {
            x.VisualLightNingSkill();
            x.SetStaticRigibody();
            PhongtothunhoAnimation.PlayEffectSmallToBig(x.transform, -1, 1.03f);
        }

        //dung dem thoi gian
        UIManager.Instance.gameplayPannel.timeSystem.isRunning = false;


        //active square
        UIManager.Instance.gameplayPannel.Square_Skill.SetActive(true);

        //thay doi layer cho drillSkill
        skill.OpenCanvasSortingLayer();
    }
}
