using System.Collections.Generic;
using UnityEngine;



public class WinLoseSystem : MonoBehaviour
{

    public bool _ended = false;

    private void Start()
    {

    }
    private void Update()
    {

    }

    public void Setting()
    {
        _ended = false;
    }
    public void Evaluate()
    {


        bool checkPlank = CheckFallPlank();


        if (checkPlank)
        {
            _ended = true;

            GameStateManager.Instance.ChangeSate(GameStateCache.winGameState);

        }


        else if (GameManager.Instance.timerSystem != null && GameManager.Instance.timerSystem.timeRemaining <= 0f)
        {
            _ended = true;
            //chua xu li trunog hop nay
            //GameManager.Instance.timerSystem.isRunning = false;
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
