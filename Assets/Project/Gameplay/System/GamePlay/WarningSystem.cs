using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WarningSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public float WarningTimeWating = 2f;




    private void Awake()
    {

    }
    void Start()
    {

    }
    private void OnEnable()
    {
        //   SetupWarningTime();
    }
    private void OnDisable()
    {
        SetupOnDisable();
    }

    public void SetupWarningTime()
    {
        WarningTimeWating = 2;
        timeDuration = 10;
        isCountDown = false;
        isRuning = true;
        timeRemaing = 0;
    }
    public void SetupOnDisable()
    {

        isCountDown = false;
        isRuning = false;
    }
    public bool isCountDown = false;
    public float timeRemaing;
    public float timeDuration;
    public event Action<float> warningSystemAction;
    public bool isRuning;
    private void FixedUpdate()
    {
        if (!isRuning) return;
        //check liên tục trong update , khi mà check = true thì đếm nguwocsj 2s, nếu như trong 
        //2s đó ko check == false, hủy đếm ngược ngay , cứ lặp lại như vậy thôi 
        var check = GameManager.Instance.holeSystem.AreAllHoleBackgroundCoverd();
        if (check == true)
        {
            if (isCountDown == false)
            {

                if (timeRemaing < WarningTimeWating)
                {
                    timeRemaing += Time.deltaTime;
                }
                else
                {
                    isCountDown = true;
                    timeRemaing = timeDuration;
                }
            }
            else
            {
                if (timeRemaing > 0)
                {
                    timeRemaing -= Time.deltaTime;
                    //goi event
                    warningSystemAction?.Invoke(timeRemaing);
                }
                else
                {
                    isRuning = false;
                    //Chuyển sang state loss
                    GameStateManager.Instance.ChangeSate(GameStateCache.lossState);

                }
            }


        }
        else
        {
            SetupWarningTime();
            UIManager.Instance.gameplayPannel.timeView.SetupText();
        }

    }
}
