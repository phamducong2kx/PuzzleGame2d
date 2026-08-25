using System;
using UnityEngine;

// SYSTEM LAYER — Cross-scene singleton, quản lý state toàn bộ game
// DontDestroyOnLoad → tồn tại xuyên suốt session
// Mọi system/UI subscribe OnGameStateChanged để tự điều chỉnh

public class GameStateManager : MonoBehaviour
{
    // singleton 
    public static GameStateManager Instance { get; private set; }



    //state hiện tại;
    public IGameState currentGameState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Lúc đầu thì  current State dang là homeState;
        ChangeSate(GameStateCache.homeState);


    }
    private void OnEnable()
    {

    }

    private void Start()
    {

    }


    public void ChangeSate(IGameState gameState)
    {
        if (currentGameState == gameState) return;

        //state thoát state cũ 
        currentGameState?.Existstate();

        //gán cho state mới
        currentGameState = gameState;

        //vào state mới
        currentGameState.EnterSate();



    }






    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
