using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public AddCointLevel addCointLevel;
    public NotificationUnlockLevel unlockLevel;


    void Start()
    {

    }


    void Update()
    {

    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }



}
