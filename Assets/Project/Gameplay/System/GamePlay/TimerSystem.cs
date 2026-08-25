using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeState
{
    Normal,
    Warning,
    TimeOut
}

public class TimerSystem : MonoBehaviour
{
    [Header("Runtime state")]
    public float duration;
    public float timeRemaining;
    public bool isRunning;

    [Header("Config")]
    public float warnThreshold;
    public float MocStar_3 = 0;
    public float MocStar_2 = 0;
    public float MocStar_1 = 0;
    public int mocsao = 0;
    public event Action<float, TimeState> OnTimerTick;
    public event Action<int, StarState> OnStarView;
    public bool isDotweeinRuning = true;

    public TimeState timeState = TimeState.Normal;

    private void Awake()
    {

    }
    private void OnEnable()
    {

    }
    public void RegisterEventAddTime()
    {
        GameConfigManager.Instance.skillLogic.AddTimeSkill += HandleAddTime;
    }

    private void HandleAddTime()
    {
        timeRemaining += 40f;

        float mocsao1 = LevelLoader.Instance.CurrentLevelData.thresh_time_star1;
        float mocsao2 = LevelLoader.Instance.CurrentLevelData.thresh_time_star2;
        float mocsao3 = LevelLoader.Instance.CurrentLevelData.thresh_time_star3;
        if (timeRemaining >= mocsao3) mocsao = 3;
        else if (timeRemaining >= mocsao2) mocsao = 2;
        else if (timeRemaining >= mocsao1) mocsao = 1;
        else mocsao = 0;
        //sau do tim cai ham ma reset lai cac ngoi sao theo mocsao;
        UIManager.Instance.gameplayPannel.starView.ResetStar_TheoMocSao(mocsao);
    }

    private void OnDisable()
    {
        // StopCoroutine(TimeRoutine());
        GameConfigManager.Instance.skillLogic.AddTimeSkill -= HandleAddTime;
    }
    private void Start()
    {

    }

    private void Update()
    {
        if (!isRunning) return;
        timeRemaining -= Time.deltaTime;

        if (mocsao == 3) CheckMocSao(3, MocStar_3);
        if (mocsao == 2) CheckMocSao(2, MocStar_2);
        if (mocsao == 1) CheckMocSao(1, MocStar_1);



        if (timeRemaining <= warnThreshold && timeRemaining >= 0)
        {
            timeState = TimeState.Warning;
        }
        if (timeRemaining <= 0)
        {
            timeState = TimeState.TimeOut;
            isRunning = false;
        }

        OnTimerTick?.Invoke(timeRemaining, timeState);
    }


    private void CheckMocSao(int threshStar, float time_mocsao)
    {
        if (timeRemaining <= time_mocsao + 10 && isDotweeinRuning)
        {
            OnStarView?.Invoke(threshStar, StarState.Warning);
            isDotweeinRuning = false;
        }
        if (timeRemaining < time_mocsao)
        {
            OnStarView?.Invoke(threshStar, StarState.Loss);
            isDotweeinRuning = true;
            --mocsao;

        }
    }



    //set up casc giá tr? m?i cho time
    public void SetupTimeLevel(float duration, float warnTime, float star3, float star2, float star1)
    {
        this.duration = duration;
        warnThreshold = warnTime;
        timeRemaining = duration;
        isRunning = true;
        MocStar_1 = star1;
        MocStar_2 = star2;
        MocStar_3 = star3;
        mocsao = 3;
        timeState = TimeState.Normal;
    }
}
