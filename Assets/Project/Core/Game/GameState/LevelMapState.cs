using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapState : IGameState
{
    public void EnterSate()
    {
        UIManager.Instance.homeManager.HideSelf();
        UIManager.Instance.levelMapManager.ActiveSelf();
    }

    public void Existstate()
    {
        UIManager.Instance.levelMapManager.HideSelf();
    }
}
