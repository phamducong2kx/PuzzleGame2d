using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WarningSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public float WarningTimeWating = 2f;

    public bool end;
    public AudioSource audioSource;

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
        isCalled = false;
    }
    public void SetupOnDisable()
    {

        isCountDown = false;
        isRuning = false;
        isCalled = false;
    }
    public bool isCountDown = false;
    public float timeRemaing;
    public float timeDuration;
    public event Action<float> warningSystemAction;
    public bool isRuning;
    public bool isCalled = false;
    private void Update()
    {
        if (!isRuning) return;
        //check liên tục trong update , khi mà check = true thì đếm nguwocsj 2s, nếu như trong 
        //2s đó ko check == false, hủy đếm ngược ngay , cứ lặp lại như vậy thôi 
        var check = GameManager.Instance.holeSystem.AreAllHoleBackgroundCoverd();
        if (check == true)
        {
            Debug.Log("các hole bag đã chưa bị lấp kín");
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
                    GameManager.Instance.timerSystem.isRunning = false;
                    timeRemaing -= Time.deltaTime;
                    //goi event
                    warningSystemAction?.Invoke(timeRemaing);
                    if (isCalled == false)
                    {
                        audioSource = AudioManagement.Instance.SpawnObject(AddressableLabels.TIKTACK_TIME);
                        //AudioSource = AudioManagement.Instance.PlaySound(AddressableLabels.TIKTACK_TIME, 1, 10.3f);
                        isCalled = true;
                    }
                }
                else
                {
                    AudioManagement.Instance.StopSound(audioSource);

                    isRuning = false;
                    isCalled = false;
                    //Chuyển sang state loss
                    GameStateManager.Instance.ChangeSate(GameStateCache.lossState);

                }
            }


        }
        else
        {
            AudioManagement.Instance.StopSound(audioSource);
            //tắt âm thanh
            end = GameManager.Instance.winLoseSystem._ended;
            if (end == true)
            {
                GameManager.Instance.timerSystem.isRunning = false;
            }
            else GameManager.Instance.timerSystem.isRunning = true;
            //chgeck xem neu het gio thi tat isRuning

            SetupWarningTime();
            UIManager.Instance.gameplayPannel.timeView.SetupText();
            //  Debug.Log("các hole bag chưa bị lấp kín");

        }

    }
}
