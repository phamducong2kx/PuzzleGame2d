using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameplayPannel gameplayPannel;
    public HomeManager homeManager;
    public TopZone topBarZone;
    public LevelMenu pannelLevelSelect;
    public GameObject pannelWingame;
    public LevelMapManager levelMapManager;

    public static UIManager Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {

            Destroy(gameObject);
            return;
        }
        Instance = this;
    }




    public void ActivePanelLevelComplate()
    {
        pannelWingame.SetActive(true);
    }






    public void HidePanelLevelComplate()
    {
        pannelWingame.SetActive(false);
    }




}
