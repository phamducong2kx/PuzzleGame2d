using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WarningSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public float WarningTimeWating = 2f;
    public float WarningTimeRemaining = 10f;
    public event Action<float> warningSystemAction;
    public bool isPlaying;
    public float markTime;


    private void Awake()
    {

    }
    void Start()
    {

    }
    public void SetupWarningTime()
    {
        WarningTimeWating = 2f;
        WarningTimeRemaining = 10f;
        markTime = -1;
        isPlaying = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (!isPlaying) return;
        bool check = GameManager.Instance.holeSystem.AreAllHoleBackgroundCoverd();
        if (!check)
        {
            WarningTimeRemaining = 10f;
            WarningTimeWating = 2f;
            GameManager.Instance.timerSystem.isRunning = true;
            UIManager.Instance.gameplayPannel.timeView.SetupText();
            if (markTime > 0)
            {
                GameManager.Instance.timerSystem.timeRemaining = markTime;
                markTime = -1;
            }

            // 
        }
        else
        {
            if (WarningTimeWating >= 0)
            {
                WarningTimeWating -= Time.deltaTime;
            }
            if (WarningTimeWating < 0)
            {
                //danh dau lai thoi gian dang chay game
                markTime = GameManager.Instance.timerSystem.timeRemaining;
                GameManager.Instance.timerSystem.isRunning = false;




                WarningTimeRemaining -= Time.deltaTime;

                warningSystemAction?.Invoke(WarningTimeRemaining);
            }
            if (WarningTimeRemaining <= 0)
            {
                //chuyenr den los sytem
                //tat
                isPlaying = false;
                GameStateManager.Instance.ChangeSate(GameStateCache.lossState);
            }
        }
    }
}
