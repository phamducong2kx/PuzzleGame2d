



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
    public static readonly HomeState homeState = new HomeState();
    public static readonly LevelSelectState levelSelecState = new LevelSelectState();
    public static readonly IGameState playingState = new PlayingSate();
    public static readonly PauseState pauseState = new PauseState();

    public static readonly WinState winGameState = new WinState();
    public static readonly LossState lossState = new LossState();
    public static readonly LevelMapState levelMapState = new LevelMapState();




}




















