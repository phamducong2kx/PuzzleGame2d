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
    public TextMeshProUGUI warningLabel;
    [Header("Visual")]
    public Color normalColor = Color.white;
    public Color warnColor = Color.red;
    public float pulseScale = 1.2f;
    public float pulseDuration = 0.4f;
    public bool isRuning = false;
    public float toadoX;

    private void Awake()
    {
        toadoX = warningLabel.rectTransform.anchoredPosition.x;
    }
    public void SetupText()
    {
        warningLabel.rectTransform.anchoredPosition = new Vector2(toadoX, warningLabel.rectTransform.anchoredPosition.y);
    }
    private void OnEnable()
    {
        //  RegisterTimeEvent();
    }

    private void OnDisable()
    {


        //huy naimation
        PhongtothunhoAnimation.KillAnimation(label.transform);
        PhongtothunhoAnimation.KillAnimation(warningLabel.transform);
        GameManager.Instance.timerSystem.OnTimerTick -= HandleTick;
        GameManager.Instance.warningSystem.warningSystemAction -= HandleWarningSystemAction;


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
                HandleTimeOut(remaining);

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
        //mịc đích là để cho dot sin hra 1 đoi tuong thoi
        if (isRuning)
        {
            isRuning = false;
            PhongtothunhoAnimation.PlayEffectSmallToBig(label.transform, -1, 1.2f);
        }


    }

    public void HandleTimeOut(float remaining)
    {
        label.text = FormatString(remaining);
        //huy dotteenw
        PhongtothunhoAnimation.KillAnimation(label.transform);
        GameManager.Instance.winLoseSystem.Evaluate();
    }
    private void HandleWarningSystemAction(float reamingtime)
    {
        label.color = warnColor;
        label.text = FormatWarningSystem(reamingtime);

        Vector2 targetWarningText = new Vector2(0, warningLabel.rectTransform.anchoredPosition.y);
        warningLabel.rectTransform.anchoredPosition = Vector2.MoveTowards(
             warningLabel.rectTransform.anchoredPosition,
             targetWarningText,
             100 * Time.deltaTime);
        warningLabel.text = "Không còn khoảng trống !!";
        //  PhongtothunhoAnimation.PlayEffectSmallToBig(warningLabel.transform, -1, 1.2f);

    }

    public void RegisterTimeEvent()
    {
        GameManager.Instance.timerSystem.OnTimerTick -= HandleTick;
        GameManager.Instance.timerSystem.OnTimerTick += HandleTick;
        GameManager.Instance.warningSystem.warningSystemAction -= HandleWarningSystemAction;
        GameManager.Instance.warningSystem.warningSystemAction += HandleWarningSystemAction;

    }



    public string FormatString(float time)
    {
        time = Mathf.CeilToInt(time);
        //  if (time <= 0) time = 0;
        int minute = Mathf.FloorToInt(time / 60);
        if (time < 0) Debug.Log("minute la  " + minute);
        int secs = Mathf.FloorToInt(time % 60);
        if (time < 0) Debug.Log("secs la  " + secs);
        return $"{minute:00}:{secs:00}";
    }

    public string FormatWarningSystem(float time)
    {
        var resultTime = Mathf.CeilToInt(time);
        return resultTime.ToString();
    }


    private void Update()
    {


    }
}