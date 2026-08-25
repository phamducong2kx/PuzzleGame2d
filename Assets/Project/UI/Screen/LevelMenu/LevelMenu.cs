using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{


    public LevelButton buttonPrefab;
    public ChapterICon chapterIcon;
    public Transform gridPannelItem;
    public Transform gridPannelChappter;
    public Button returnHomeButton;
    public Button goRight;
    public Button goLeft;
    public SnapScrolling snapScroliing;





    private void Awake()
    {
        // Debug.Log("khoi tao level select");
        Generate_LevelButton();
        Generate_Chapter();
        SetUpReturnButton();
    }

    private void OnEnable()
    {

        SetUpChapterCenter();

    }
    private void OnDisable()
    {

    }
    void Start()
    {
        SetupOnClickButtonChanegPage();
    }

    void Update()
    {

    }

    //taoj trước 10 button có sẵn
    private void Generate_LevelButton()
    {
        for (int i = 1; i <= 10; ++i)
        {
            //khoi tao 1 chapter tu prefab
            var LevelButton = Instantiate(buttonPrefab, gridPannelItem);



        }
    }

    //gen ra danh sách các chapter và levelData tương ứng
    private void Generate_Chapter()
    {
        //tim world hien tai
        int currentWorldID = SaveManager.Data.currentWorld;


        //tim chapter cao nhat  cua world hien tai
        int highestChapterID = GameConfigManager.Instance.levelDatabaseLogic.GetChapterHighestOfCurrentWorld(currentWorldID);
        //  Debug.Log("chapter cao nhat o world hien tai la " + highestChapterID);
        //tim so chapter co trong world nay
        int numberOfChapter = GameConfigManager.Instance.levelDatabaseLogic.GetNumberOfChapter(currentWorldID);

        //  Debug.Log("so luong chapter la " + numberOfChapter);
        //id chaoter bat dau la :
        int idChapterBegin = (currentWorldID - 1) * 5;
        //  Debug.Log("idchapterbegin la  " + idChapterBegin);

        //taoj ra danh sacsh cacs chapter tu prefab
        for (int i = idChapterBegin + 1; i <= idChapterBegin + numberOfChapter; ++i)
        {
            //khoi tao 1 chapter tu prefab
            var chapterButton = Instantiate(chapterIcon, gridPannelChappter);

            //setup thuoc tinh cho no
            chapterButton.Setup(i);
            // Debug.Log("khoi tao thamh cong 1 button chapter ");
        }

        //set up chapter cao nhat world nay lam trung tam
        snapScroliing.SetUPListItem();
        //snapScroliing.SetupChapter_Center(highestChapterID);

        //// set up logic cho 2 nut dich chuyen tria phai

        //SetupActiceButtonChangePage(snapScroliing.targetChapterIndex, snapScroliing.chapters.Count);


        ////dem xem chapter cao nhat hien dang co cua world hien tai bao nhieu level;
        //int numberLevelOfHighestChapter = UIManager.Instance.levelMapManager.levelDatabase.GetNumberOfLevel(highestChapterID, currentWorldID);


        ////taoj danh sacsh cac levelbutotn tuong uwng cua chapter nay
        ////levelID bat dau


        //RefreshButtonIcon(highestChapterID);
    }

    private void SetUpChapterCenter()
    {
        int currentWorldID = SaveManager.Data.currentWorld;

        int highestChapterID = GameConfigManager.Instance.levelDatabaseLogic.GetChapterHighestOfCurrentWorld(currentWorldID);
        snapScroliing.SetupChapter_Center(highestChapterID);

        // set up logic cho 2 nut dich chuyen tria phai

        SetupActiceButtonChangePage(snapScroliing.targetChapterIndex, snapScroliing.chapters.Count);


        //dem xem chapter cao nhat hien dang co cua world hien tai bao nhieu level;
        int numberLevelOfHighestChapter = GameConfigManager.Instance.levelDatabaseLogic.GetNumberOfLevel(highestChapterID, currentWorldID);


        //taoj danh sacsh cac levelbutotn tuong uwng cua chapter nay
        //levelID bat dau


        RefreshButtonIcon(highestChapterID);
    }


    //set up nut return ve trang lavelmap
    private void SetUpReturnButton()
    {
        returnHomeButton.onClick.RemoveAllListeners();
        returnHomeButton.onClick.AddListener(() =>
        {
            //chuyen ve trang home
            GameStateManager.Instance.ChangeSate(GameStateCache.levelMapState);

            //set up screen
            UIManager.Instance.levelMapManager.ActiveSelf();

            //refresh lai cac nut de mo lai animation
            UIManager.Instance.levelMapManager.Refresh();



        });
    }

    //kich hoat ui
    public void ActiveSelf()
    {
        gameObject.SetActive(true);
    }

    //dong ui
    public void HideSelf()
    {
        gameObject.SetActive(false);
    }

    //reload lại danh sách các level
    public void RefreshButtonIcon(int chapterID)
    {
        int worldID = SaveManager.Data.currentWorld;
        //  Debug.Log("world id hien tai la " + worldID);
        int numberLevel = GameConfigManager.Instance.levelDatabaseLogic.GetNumberOfLevel(chapterID, worldID);
        //   Debug.Log($"so luong level cua chapter {chapterID} la {numberLevel}");
        int idBegin = (chapterID - 1) * 10;


        //dlist levefress
        var list = SaveManager.Data.levelProgresses;
        foreach (var x in list)
        {
            //  Debug.Log($"level co id la {x.leveID} , trang thai unlock la {x.isUnlock}");
        }

        var listLevelButton = gridPannelItem.GetComponentsInChildren<LevelButton>(true);
        //   Debug.Log($"so luong button level cua chapter {listLevelButton.Count()}");

        if (numberLevel < 10)
        {
            int distance = 10 - numberLevel;
            for (int i = 0; i < distance; ++i)
            {
                listLevelButton[i].gameObject.SetActive(false);
            }
        }
        else if (numberLevel == 10)
        {

            for (int i = 0; i < 10; ++i)
            {
                listLevelButton[i].gameObject.SetActive(true);
            }

        }
        listLevelButton = gridPannelItem.GetComponentsInChildren<LevelButton>();

        int j = 0;

        for (int i = idBegin + 1; i <= idBegin + numberLevel; ++i)
        {
            //   Debug.Log("listbutton nay co id la " + i);
            listLevelButton[j].Setup(i);
            ++j;
        }

    }

    //kich hoat nut sang trai sang phai
    public void SetupActiceButtonChangePage(int currentTarget, int count)
    {


        if (count == 1)
        {
            goRight.gameObject.SetActive(false);
            goLeft.gameObject.SetActive(false);

        }
        //truong hop cuoi chapter
        else if (currentTarget == count - 1)
        {
            goRight.gameObject.SetActive(false);
            goLeft.gameObject.SetActive(true);

        }
        else if (currentTarget == 0)
        {
            goLeft.gameObject.SetActive(false);
            goRight.gameObject.SetActive(true);

        }
        else
        {
            goLeft.gameObject.SetActive(true);
            goRight.gameObject.SetActive(true);


        }



    }

    private void SetupOnClickButtonChanegPage()
    {

        goRight.onClick.AddListener(() =>
        {

            ++snapScroliing.targetChapterIndex;
            //lay thong tin cua chapter nay
            var chapter = snapScroliing.chapters[snapScroliing.targetChapterIndex].GetComponent<ChapterICon>();
            int idCahpter = chapter.chapterID;
            //   Debug.Log("id cua chapter nay la" + idCahpter);

            SetupActiceButtonChangePage(snapScroliing.targetChapterIndex, snapScroliing.chapters.Count);
            RefreshButtonIcon(idCahpter);
        });


        goLeft.onClick.AddListener(() =>
        {

            --snapScroliing.targetChapterIndex;
            var chapter = snapScroliing.chapters[snapScroliing.targetChapterIndex].GetComponent<ChapterICon>();
            int idCahpter = chapter.chapterID;
            // Debug.Log("id cua chapter nay la" + idCahpter);
            SetupActiceButtonChangePage(snapScroliing.targetChapterIndex, snapScroliing.chapters.Count);
            RefreshButtonIcon(idCahpter);
        });
    }




}
