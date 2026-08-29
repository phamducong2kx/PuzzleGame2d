using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayPannel : MonoBehaviour
{

    public CoinView coinview;
    public TimerSystem timeSystem;
    public WinLoseSystem winLoseSystem;
    public TimerUiView timeView;
    public Button pauseButton;
    public TextMeshProUGUI levelText;
    public StarView starView;
    public SkillView skillView;
    public GameObject Square_Skill;
    public SkiillShopPopup skiillShopPopup;
    public LossPopup lossPopup;


    //tham chiếu pausePannel
    public PauseManager pannelPause;


    private void Awake()
    {


        winLoseSystem = new WinLoseSystem();

    }

    public void SetBegin()
    {
        //set up time
        float timeLimit = LevelLoader.Instance.CurrentLevelData.timerLimit;
        float timeWarn = LevelLoader.Instance.CurrentLevelData.timerWanr;
        ///  float thres_star_1 = LevelLoader.Instance.CurrentLevelData.thresh_time_star1;
        //  float thres_star_2 = LevelLoader.Instance.CurrentLevelData.thresh_time_star2;
        //  float thres_star_3 = LevelLoader.Instance.CurrentLevelData.thresh_time_star3;
        var listStar = LevelLoader.Instance.CurrentLevelData.ListMocTimeStar;

        //khoi tao time vaf time va set upo timeView
        //   timeSystem.SetupTimeLevel(timeLimit, timeWarn, thres_star_3, thres_star_2, thres_star_1);

        timeSystem.SetupTimeLevel(timeLimit, timeWarn, listStar);


        //  timeView.HandleNormalTime(timeSystem.timeRemaining);


        //set up starview
        starView.GenerateStarIcon();
        skillView.GenerateListSkill();

        //Khoi tao tetx cua level, cua coint
        levelText.text = $"Level {SaveManager.Data.currentLevel.ToString()}";
        coinview.text_coin.text = "0";

        //kich hoat ui
        timeView.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        starView.gameObject.SetActive(true);
        skillView.gameObject.SetActive(true);

        //dang ki su kien
        timeView.RegisterTimeEvent();
        starView.RegisterEventStarView();
        timeSystem.RegisterEventAddTime();


    }



    private void OnEnable()
    {

    }
    private void OnDisable()
    {




    }

    void Start()
    {
        pauseButton.onClick.AddListener(() =>
        {
            GameStateManager.Instance.ChangeSate(GameStateCache.pauseState);
        });

    }

    void Update()
    {

    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }

    public void ActiveSelf()
    {
        gameObject.SetActive(true);
    }


    public void WinUI()
    {
        starView.gameObject.SetActive(false);
        timeView.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        skillView.gameObject.SetActive(false);
        Square_Skill.SetActive(false);
        levelText.text = "";
    }

    public void ActivePauseUI()
    {
        pannelPause.gameObject.SetActive(true);
    }

    public void HidePauseUI()
    {
        pannelPause.gameObject.SetActive(false);
    }





}
