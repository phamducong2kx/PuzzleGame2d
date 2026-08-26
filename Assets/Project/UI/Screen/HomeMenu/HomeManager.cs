using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public GameObject Background;
    public Button buttonLevel;
    public Button buttonAvatar;
    public Button dailyReward;
    public Button shoppe;
    public DailyRewardPopup dailyRewardPopup;
    public NoticeReward noticeReward;
    public AvatarPopup avatarPopup;



    private void Awake()
    {



    }

    void Start()
    {
        SetUpLevelButton();
        SetUpDailyRewardButton();
        SetUpSignNoticeDailyReward();
        SetupAvatarButton();
    }
    public void ActiveSelf()
    {
        gameObject.SetActive(true);
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }



    private void SetUpLevelButton()
    {
        buttonLevel.onClick.AddListener(() =>
        {
            GameStateManager.Instance.ChangeSate(GameStateCache.levelMapState);
        });
    }

    private void SetupAvatarButton()
    {
        //tim idavatar curent
        string idAvatarCurrent = GameConfigManager.Instance.playerDataLogic.GetIdCurrentAvatar(SaveManager.Data);
        //tim kiem sprite theo id
        var sprite = GameConfigManager.Instance.playerDataLogic.GetSpriteFormIdAvatar(idAvatarCurrent);
        //gan sprite cho avatar
        var image = buttonAvatar.GetComponent<Image>();
        image.sprite = sprite;

        buttonAvatar.onClick.AddListener(() =>
        {
            //hien thi popupAvatar
            avatarPopup.gameObject.SetActive(true);
        });
    }

    private void SetUpDailyRewardButton()
    {
        dailyReward.onClick.AddListener(() =>
        {
            //set active gameobejct popup
            dailyRewardPopup.gameObject.SetActive(true);

            //neu mo lan dau thi ko can refresh
            if (dailyRewardPopup.isFirstOpen == false)
            {
                //refresh lai cac pannel nhan qua
                dailyRewardPopup.RefreshPannelDayItem();
            }

            dailyRewardPopup.isFirstOpen = false;

        });
    }

    private void SetUpSignNoticeDailyReward()
    {

        TimeSpan time = DailyRewardTimeLogic.GetRemainingTime();
        if (time == TimeSpan.Zero)
        {

            noticeReward.HandleNoticeGetReward();
        }

        else
        {

            noticeReward.HandleDoneGetReward();
        }

    }


    public void RefreshNoticeDailyReward()
    {
        //notice xoa het dotweenn


        //sau đó set up lại ban đầu 
        SetUpSignNoticeDailyReward();
    }



    void Update()
    {

    }


}
