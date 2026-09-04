using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class PannelDayItem : MonoBehaviour
{

    public TextMeshProUGUI textDay;
    public RectTransform pannelItem;
    private int day;

    // xem hom nay la ngay may




    private void Awake()
    {



    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        transform.DOKill(true);
        transform.localScale = Vector3.one;
    }
    void Start()
    {



    }


    void Update()
    {

    }

    public void Animation()
    {
        transform.DOScale(1.2f, 0.5f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }



    public void SetUpPannel(int dayNumber)
    {
        day = dayNumber;

        textDay.text = $"Ngày {day}";

        RefreshPannel();




    }

    public void RefreshPannel()
    {

        StopAnimation();
        int currentDay = SaveManager.Data.currentDailyReward;


        if (day == currentDay + 1)
        {
            Animation();



        }

    }

    void StopAnimation()
    {
        transform.DOKill(false);
        transform.localScale = Vector3.one;
    }
}
