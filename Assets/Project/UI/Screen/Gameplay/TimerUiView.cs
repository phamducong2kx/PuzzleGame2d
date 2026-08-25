using DG.Tweening;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class TimerUiView : MonoBehaviour
{

    [Header("References")]
    public TextMeshProUGUI label;

    [Header("Visual")]
    public Color normalColor = Color.white;
    public Color warnColor = Color.red;
    public float pulseScale = 1.2f;
    public float pulseDuration = 0.4f;
    public bool isRuning = false;


    private void OnEnable()
    {

    }

    private void OnDisable()
    {


        //huy naimation
        PhongtothunhoAnimation.KillAnimation(label.transform);
        UIManager.Instance.gameplayPannel.timeSystem.OnTimerTick -= HandleTick;
    }



    public void HandleTick(float remaining, TimeState timeState)
    {

        switch (timeState)
        {
            case TimeState.Normal:
                isRuning = true;
                HandleNormalTime(remaining);
                break;
            case TimeState.Warning:

                HandleWarningTime(remaining);
                break;
            case TimeState.TimeOut:
               
                label.text = FormatString(remaining);
                UIManager.Instance.gameplayPannel.winLoseSystem.Evaluate();
                break;
        }




    }

    public void HandleNormalTime(float remaining)
    {
        if (label == null) return;
        label.color = normalColor;
        label.text = FormatString(remaining);
    }
    public void HandleWarningTime(float remaining)
    {
        if (label == null) return;
        label.color = warnColor;
        label.text = FormatString(remaining);
        if (isRuning)
        {
            isRuning = false;
            PhongtothunhoAnimation.PlayEffectSmallToBig(label.transform, -1, 1.2f);
        }


    }


    public void RegisterTimeEvent()
    {
        UIManager.Instance.gameplayPannel.timeSystem.OnTimerTick += HandleTick;
    }

    public string FormatString(float time)
    {
        time = Mathf.CeilToInt(time);
        if (time <= 0) time = 0;
        int minute = Mathf.FloorToInt(time / 60);
        if (time < 0) Debug.Log("minute la  " + minute);
        int secs = Mathf.FloorToInt(time % 60);
        if (time < 0) Debug.Log("secs la  " + secs);
        return $"{minute:00}:{secs:00}";
    }


    private void Update()
    {


    }
}