



using System;
using System.Collections;
using System.Data;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public interface IGameState
{
    void EnterSate();
    void Existstate();
}


public static class GameStateCache
{
    public static readonly IGameState loadingState = new LoadingState();
    public static readonly IGameState homeState = new HomeState();
    public static readonly IGameState levelMapState = new LevelMapState();

    public static readonly IGameState levelSelecState = new LevelSelectState();
    public static readonly IGameState playingState = new PlayingSate();
    public static readonly IGameState pauseState = new PauseState();

    public static readonly IGameState winGameState = new WinState();
    public static readonly IGameState lossState = new LossState();
}

//public static class SkillStateCache
//{
//    public static readonly ISKillState lightningSkillState = new LightNingState();
//    public static readonly ISKillState drillSkillState = new HomeState();
//    public static readonly ISKillState timeSkillState = new LevelMapState();


//}




















