using System.Collections.Generic;
using UnityEngine;



public class WinLoseSystem
{

    private bool _ended;



    public void Evaluate()
    {


        bool checkPlank = CheckFallPlank();


        if (checkPlank)
        {


            GameStateManager.Instance.ChangeSate(GameStateCache.winGameState);

        }


        else if (UIManager.Instance.gameplayPannel.timeSystem != null && UIManager.Instance.gameplayPannel.timeSystem.timeRemaining <= 0f)
        {
            //chua xu li trunog hop nay

            GameStateManager.Instance.ChangeSate(GameStateCache.lossState);


        }

    }

    private bool CheckFallPlank()
    {
        if (LevelLoader.Instance == null) return false;
        foreach (var plank in LevelLoader.Instance.spawnedPlanks)
        {
            if (plank != null && !plank.hasFallen)
            {

                return false;
            }
        }

        return true;
    }
}
