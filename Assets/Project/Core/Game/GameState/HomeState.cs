using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeState : IGameState
{
    public void EnterSate()
    {
        //  SaveManager.ClearData();
        SaveManager.LoadData();
        //  if (UIManager.Instance == null)
        //  {
        //      Debug.Log("uimanager chua kho itoa");
        //  }

        //set up home : ddAX SET UP TRONG START CUA HOMEpANNEL
        int a = GameConfigManager.Instance.levelDatabaseLogic.GetChapterHighestOfCurrentWorld(1);
        Debug.Log(a);
    }

    public void Existstate()
    {

    }
}