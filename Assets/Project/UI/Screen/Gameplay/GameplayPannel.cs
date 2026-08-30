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




    }

    public void SetBegin()
    {
        //set up time
        float timeLimit = LevelLoader.Instance.CurrentLevelData.timerLimit;
        float timeWarn = LevelLoader.Instance.CurrentLevelData.timerWanr;

        var listStar = LevelLoader.Instance.CurrentLevelData.ListMocTimeStar;



        GameManager.Instance.timerSystem.SetupTimeLevel(timeLimit, timeWarn, listStar);
        GameManager.Instance.warningSystem.SetupWarningTime();




        //set up starview
        starView.GenerateStarIcon();
        skillView.GenerateListSkill();

        //Khoi tao tetx cua level, cua coint
        levelText.text = $"Level {SaveManager.Data.currentLevel.ToString()}";
        coinview.text_coin.text = "0";
        timeView.SetupText();

        //kich hoat ui
        timeView.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        starView.gameObject.SetActive(true);
        skillView.gameObject.SetActive(true);

        //dang ki su kien

        SetupRegisterEvent();

    }

    public void SetupRegisterEvent()
    {
        timeView.RegisterTimeEvent();
        starView.RegisterEventStarView();
        GameManager.Instance.timerSystem.RegisterEventAddTime();
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
