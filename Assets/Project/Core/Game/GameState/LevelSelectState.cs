using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectState : IGameState
{
    public void EnterSate()
    {
        
        
        //reset lại time
        Time.timeScale = 1;

        //khóa ui home
        UIManager.Instance.homeManager.HideSelf();

        //khao ui gameplay
        UIManager.Instance.gameplayPannel.HideSelf();
        //mo ui topzone
        UIManager.Instance.topBarZone.ActiveSelf();

        // mở ui level select
        UIManager.Instance.pannelLevelSelect.ActiveSelf();

        //refresh level button if it already had before
        //  UIManager.Instance.pannelLevelSelect.RefreshButtonIcon();



    }

    public void Existstate()
    {//thoat map hien tai

        UIManager.Instance.pannelLevelSelect.HideSelf();
    }
}

