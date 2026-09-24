using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TimeState
//{
//    Normal,
//    Warning,
//    TimeOut
//}

public class TimerSystem : MonoBehaviour
{
    [Header("Runtime state")]
    public float duration;
    public float timeRemaining;
    public bool isRunning;

    [Header("Config")]
    public float warnThreshold;
    public List<float> listMocSao = new List<float>();

    public int mocsao;
    // public event Action<float, TimeState> OnTimerTick;
    public event Action<float> OnTimerTick;
    public event Action<float> OnTimeWarning;
    public event Action<int, StarState> OnStarView;
    public bool isDotweeinRuning = false;
    public bool isEventTimeCall = false;

    //public TimeState timeState = TimeState.Normal;



    public void SetupTimeLevel(float duration, float warnTime, List<float> listmocsao)
    {
        this.duration = duration;
        warnThreshold = warnTime;
        timeRemaining = duration;
        isRunning = true;
        listMocSao = listmocsao;
        mocsao = listMocSao.Count;
        //  timeState = TimeState.Normal;
    }
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

        for (int i = listMocSao.Count; i >= 1; --i)
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

        GameConfigManager.Instance.skillLogic.AddTimeSkill -= HandleAddTime;
    }
    private void Start()
    {

    }


    private void Update()
    {

        if (isRunning == false)
        {
            //Debug.LogWarning($"[TimerSystem] isRunning bị đổi thành FALSE tại Frame {Time.frameCount}!\n" +
            //                 $"Chi tiết nguồn gọi:\n{System.Environment.StackTrace}");
            return;
        }
        //if (isRunning == true)
        //{
        //    Debug.LogWarning($"[TimerSystem] isRunning bị đổi thành TRUE tại Frame {Time.frameCount}!\n" +
        //                     $"Chi tiết nguồn gọi:\n{System.Environment.StackTrace}");
        //    // return;
        //}

        timeRemaining -= Time.deltaTime;
        HandleMocSao();
        // Debug.Log("van chay tipe");
        OnTimerTick?.Invoke(timeRemaining);
        if (timeRemaining <= warnThreshold)
        {
            OnTimeWarning?.Invoke(timeRemaining);

        }




    }
    private void HandleMocSao()
    {
        for (int i = listMocSao.Count; i >= 1; --i)  //3
        {
            if (mocsao == i)
            {
                if (timeRemaining <= listMocSao[i - 1] + 10 && isDotweeinRuning == false)
                {
                    Debug.Log($"lucs nafy i = {i} va bat dau chay evet");
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

        }
    }







    public void ReloadTime(float time)
    {
        isRunning = true;
        timeRemaining = time;
    }
}
