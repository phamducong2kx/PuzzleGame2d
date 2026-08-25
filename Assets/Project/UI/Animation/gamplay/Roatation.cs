using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roatation : MonoBehaviour
{
    public float duration;


    private void OnEnable()
    {
        GameConfigManager.Instance.skillLogic.DrillSkillEvent += HandleEventDrillSkill;
    }

    private void HandleEventDrillSkill(Bolt bolt)
    {
        bolt.transform.localScale = Vector3.one * 1.4f;
        bolt.transform.DORotate(new Vector3(0, 0, -2520), duration, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        Debug.Log("xong da chay ani xaoy");
                    });


    }


    private void OnDisable()
    {
        GameConfigManager.Instance.skillLogic.DrillSkillEvent += HandleEventDrillSkill;

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
