using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Button buttonExist;
    public Transform pannelListWorldItem;

    private void Awake()
    {

    }


    private void OnEnable()
    {
        SetUpLevelMap();
        SetUpExistButton();
    }

    private void OnDisable()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void SetUpExistButton()
    {
        buttonExist.onClick.AddListener(() =>
        {
            //disable cái popup này
            gameObject.SetActive(false);

            // home state
            GameStateManager.Instance.ChangeSate(GameStateCache.homeState);


            //refresh laij cais notice aniamtuon
            UIManager.Instance.homeManager.RefreshNoticeDailyReward();

            //active home menu screen
            UIManager.Instance.homeManager.ActiveSelf();


        });
    }

    private void SetUpLevelMap()
    {
        var listItem = pannelListWorldItem.GetComponentsInChildren<LevelConWorld>();

        foreach (var x in listItem)
        {
            x.SetupIcon();
        }
    }

    public void ActiveSelf()
    {
        gameObject.SetActive(true);
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }

    public void Refresh()
    {
        SetUpLevelMap();
    }



}
