
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;





public class DailyRewardPopup : MonoBehaviour
{
    [SerializeField] private DailyRewardConfig dailyRewardConfig;
    [SerializeField] private Transform listItemPanel;
    [SerializeField] private PannelDayItem panelDayItem;
    [SerializeField] private ItemDailyReward itemDailyReward;
    [SerializeField] private Button existButton;
    [SerializeField] private Button getRewardButton;
    [SerializeField] private TextMeshProUGUI countDownTime;

    private bool isWatingTime = true;
    public bool isFirstOpen = true;





    private void Awake()
    {
        Genertate();
        SetUpExistButton();

    }

    //tao ra danh sach cacs panell
    private void Genertate()
    {

        var listReward = dailyRewardConfig.dailyRewards;

        //tao danh sacsh pannelDayitem tu lisdtPannel
        for (int i = 0; i < listReward.Count; ++i)
        {
            // khoi tao 1 cai pannelDay;
            var pannelDay = Instantiate(panelDayItem, listItemPanel);

            //set up cho pannel do
            pannelDay.SetUpPannel(i + 1);

            //duyeetj danh sach cac phan qua tu day i+1
            var itemList_for_day = listReward[i].listRewardItem;

            //taoj 1 bien itemInfor 
            var itemInfo = new ItemInfo();
            for (int j = 0; j < itemList_for_day.Count; ++j)
            {
                //tao 1 obj item 
                var item = Instantiate(itemDailyReward, pannelDay.pannelItem);

                //voi id dau tien ,can tim item infor tuong ung
                itemInfo = GameConfigManager.Instance.itemLogic.GetItemInfoById(itemList_for_day[j].idItem);

                item.amount.text = itemList_for_day[j].amount.ToString();
                item.imageItem.sprite = itemInfo.icon;

            }



        }


    }

    void Start()
    {

    }


    void Update()
    {
        if (isWatingTime == false) return;
        TimeSpan remainingTime = DailyRewardTimeLogic.GetRemainingTime();

        if (remainingTime == TimeSpan.Zero)
        {
            isWatingTime = false;
            getRewardButton.gameObject.SetActive(true);
            countDownTime.transform.parent.gameObject.SetActive(false);
            SetUpGetRewardButton();
          
        }
        else
        {
         
            //đếm ngược time
            countDownTime.text = DailyRewardTimeLogic.ConvertTimeSpantoString(remainingTime);
        }




    }

    private void SetUpExistButton()
    {
        existButton.onClick.AddListener(() =>
        {
            //disable cái popup này
            gameObject.SetActive(false);

            //refresh laij cais notice aniamtuon
            UIManager.Instance.homeManager.RefreshNoticeDailyReward();

        });
    }

    private void SetUpGetRewardButton()
    {
        getRewardButton.onClick.RemoveAllListeners();
        getRewardButton.onClick.AddListener(() =>
        {

            //active object animation
            AnimationManager.Instance.dailyRewardAnimation.Active();

            //lay ngay hom do
            int day = SaveManager.Data.currentDailyReward;

            //laays danh sacsh qua cua ngay hom do
            var listReward = dailyRewardConfig.dailyRewards[day].listRewardItem;

            //cong vao database luon,sau do moi phat animation
            foreach (var x in listReward)
            {
                //tu id tim ra items
                var item = GameConfigManager.Instance.itemLogic.GetItemInfoById(x.idItem);

                //cong vao nguon tai nguyen
                SaveManager.AddResource(item.type, x.amount);
            }
            //phat event animation
            EventManager.InvokeGetDailyReward(listReward);

            //danh sach = true
            isWatingTime = true;

            //set active = true;
            countDownTime.transform.parent.gameObject.SetActive(true);

            //set up time 
            SaveManager.Save_Get_dailyReward_Successfull();

            //sau cung refresh lai pannel
            RefreshPannelDayItem();

        });


    }

    public void RefreshPannelDayItem()
    {
        var listPannel = listItemPanel.GetComponentsInChildren<PannelDayItem>();



        foreach (var x in listPannel)
        {
            x.RefreshPannel();
        }
    }







}
