using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private int idLevelData;
    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textUnlockLevel;
    public Image imageBg;
    public Image passLevel;

    public Transform listStar;
    public Sprite isPassLevel;
    public Sprite lockLevel;
    public Sprite unlockLevel;


    public Button buttonLevel;


    private void Awake()
    {

    }
    //set up icon , hieu ung cho tung button
    public void Setup(int index)
    {
        idLevelData = index;
        var data = SaveManager.Data;

        //check xem levle nay da unlock hay chua
        if (GameConfigManager.Instance.playerDataLogic.CheckLevelUnlock(data, index))
        {
            //image unlock 
            imageBg.sprite = unlockLevel;

            //textPrefab
            textLevel.text = index.ToString();


            //hien tuong tac voi buton
            buttonLevel.interactable = true;


            //unlock moi  hien thi so sao va hien thi so sao
            listStar.gameObject.SetActive(true);
            DisplayStar(index);

            //neu level nay chua choi thi no se la new
            if (!GameConfigManager.Instance.playerDataLogic.CheckLevelPlaying(data, index))
            {

                textUnlockLevel.text = "new";
                PhongtothunhoAnimation.PlayEffectSmallToBig(transform, -1, 1.2f);
            }
            else
            {
                textUnlockLevel.text = "";
            }

            //neu level nay da pass thi so co dau tich xanh
            if (GameConfigManager.Instance.playerDataLogic.CheckLevelPass(data, index))
            {
                passLevel.gameObject.SetActive(true);
                passLevel.sprite = isPassLevel;
            }
            else
            {
                passLevel.gameObject.SetActive(false);
            }


        }
        else
        {
            //khong unlock thi ko hien thi so sao
            listStar.gameObject.SetActive(false);
            textUnlockLevel.text = "";
            textLevel.text = "";
            imageBg.sprite = lockLevel;
            passLevel.gameObject.SetActive(false);
            buttonLevel.interactable = false;
        }
        buttonLevel.onClick.RemoveAllListeners();
        buttonLevel.onClick.AddListener(() =>
        {
            //setup levelc urrnt hien ta
            SaveManager.SetCurrenLevel(index);
            //setup chapter hien tai,tim chapter theo level
            int chapterCurrent = GameConfigManager.Instance.levelDatabaseLogic.GetChapterByLevel(index);
            SaveManager.SetCurrenChapter(chapterCurrent);


            //chuyen den state gameplay
            GameStateManager.Instance.ChangeSate(GameStateCache.playingState);
        });

    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        //set up khi disable button nay
        PhongtothunhoAnimation.KillAnimation(transform);
    }

    private void DisplayStar(int levelIndex)
    {
        //tim xem level nay co bao nhieu sao
        int star = GameConfigManager.Instance.playerDataLogic.GetNumberStarOfLevel(SaveManager.Data, levelIndex);

        var list = listStar.GetComponentsInChildren<StarICon>();

        for (int i = 0; i < list.Count(); ++i)
        {
            if (star > 0)
            {
                list[i].SetYellowStar();
                --star;
            }
            else list[i].SetWhiteStar();


        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
