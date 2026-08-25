using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingSate : IGameState
{



    public void EnterSate()
    {
        if (GameManager.Instance.isResume)
        {
            Time.timeScale = 1f;
            GameManager.Instance.isResume = false;
            return;
        }


        if (GameManager.Instance.isRefresh)
        {
            GameManager.Instance.isRefresh = false;

            EventManager.InvokeRefreshLevel();

            //despawn các ngôi sao
            UIManager.Instance.gameplayPannel.starView.Despawn();

            //
            InputHandler.Instance.pickedBolt = null;

            

            //clear level cũ
            LevelLoader.Instance.ClearLevel();
        }

        //load level
        int currentLevel = SaveManager.Data.currentLevel;

        LevelLoader.Instance.LoadLevel(currentLevel);



        //hien ui gamplay  va set up level textPrefab, coin view, newStar view , time , deu nam trong setbegin cua gameplaypannel
        UIManager.Instance.gameplayPannel.ActiveSelf();
        UIManager.Instance.gameplayPannel.SetBegin();

        //hide các ui 
        UIManager.Instance.pannelLevelSelect.HideSelf();
        //khoa ui topbar
        UIManager.Instance.topBarZone.HideSelf();

        // kích hoạt animation object 
        AnimationManager.Instance.gamePlayAnimation.Active();

        AnimationManager.Instance.levelAnimationManager.Hide();



    }

    public void Existstate()
    {

    }
}