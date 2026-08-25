using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{


    public static AnimationManager Instance;
    public GamplayAnimation gamePlayAnimation;
    public LevelManager levelAnimationManager;
    public DailyRewardAnimation dailyRewardAnimation;
    public NoticeAnimation noticeAnimation = new NoticeAnimation();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;

    }
    void Start()
    {

    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }








    // Update is called once per frame
    void Update()
    {

    }
}
