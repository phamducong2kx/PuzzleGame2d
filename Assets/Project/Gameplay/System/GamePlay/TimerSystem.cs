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
    public List<float> listMocSao = new List<float>();
    public float MocStar_3 = 0;
    public float MocStar_2 = 0;
    public float MocStar_1 = 0;
    public int mocsao;
    public event Action<float, TimeState> OnTimerTick;
    public event Action<int, StarState> OnStarView;
    public bool isDotweeinRuning = false;

    public TimeState timeState = TimeState.Normal;

    private void Awake()
    {

    }
    private void OnEnable()
    {
        //  RegisterEventAddTime();
    }
    public void RegisterEventAddTime()
    {

        GameConfigManager.Instance.skillLogic.AddTimeSkill -= HandleAddTime;
        GameConfigManager.Instance.skillLogic.AddTimeSkill += HandleAddTime;
    }

    private void HandleAddTime()
    {
        timeRemaining += 40f;

        for (int i = listMocSao.Count; i >= 1; ++i)
        {
            if (timeRemaining >= listMocSao[i - 1])
            {
                mocsao = i;
                break;
            }
        }
        //sau do tim cai ham ma reset lai cac ngoi sao theo mocsao;
        UIManager.Instance.gameplayPannel.starView.ResetStar_TheoMocSao(mocsao);
        // float mocsao1 = LevelLoader.Instance.CurrentLevelData.thresh_time_star1;
        // float mocsao2 = LevelLoader.Instance.CurrentLevelData.thresh_time_star2;
        // float mocsao3 = LevelLoader.Instance.CurrentLevelData.thresh_time_star3;
        //  if (timeRemaining >= mocsao3) mocsao = 3;
        //  else if (timeRemaining >= mocsao2) mocsao = 2;
        //  else if (timeRemaining >= mocsao1) mocsao = 1;
        //  else mocsao = 0;

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

        //  if (mocsao == 3) CheckMocSao(3, MocStar_3);
        //  if (mocsao == 2) CheckMocSao(2, MocStar_2);
        //  if (mocsao == 1) CheckMocSao(1, MocStar_1);
        HandleMocSao();


        if (timeRemaining <= warnThreshold)
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

    private void HandleMocSao()
    {
        for (int i = listMocSao.Count; i >= 1; --i)
        {
            if (mocsao == i)
            {
                if (timeRemaining <= listMocSao[i - 1] + 10 && isDotweeinRuning == false)
                {
                    OnStarView?.Invoke(i, StarState.Warning);
                    isDotweeinRuning = true;
                }
                if (timeRemaining < listMocSao[i - 1])
                {
                    OnStarView?.Invoke(i, StarState.Loss);
                    isDotweeinRuning = false;
                    --mocsao;
                }

            }
            else
            {
                break;
            }
        }
    }




    public void SetupTimeLevel(float duration, float warnTime, List<float> listmocsao)
    {
        this.duration = duration;
        warnThreshold = warnTime;
        timeRemaining = duration;
        isRunning = true;
        listMocSao = listmocsao;
        mocsao = listMocSao.Count;
        timeState = TimeState.Normal;
    }

    public void ReloadTime(float time)
    {
        isRunning = true;
        timeRemaining = time;
    }
}
