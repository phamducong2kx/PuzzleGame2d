using System.Collections.Generic;
using UnityEngine;



public class WinLoseSystem : MonoBehaviour
{

    // private bool _ended;

    private void Start()
    {

    }
    private void Update()
    {

    }

    public void Evaluate()
    {


        bool checkPlank = CheckFallPlank();


        if (checkPlank)
        {


            GameStateManager.Instance.ChangeSate(GameStateCache.winGameState);

        }


        else if (GameManager.Instance.timerSystem != null && GameManager.Instance.timerSystem.timeRemaining <= 0f)
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
