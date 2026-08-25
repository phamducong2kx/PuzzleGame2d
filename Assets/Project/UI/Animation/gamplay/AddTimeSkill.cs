using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTimeSkill : MonoBehaviour
{
    public GameObject clockPrefab;
    public GameObject obj = null;
    public Transform home;
    private void OnEnable()
    {
        GameConfigManager.Instance.skillLogic.AddTimeSkill += HandleAddTime;
    }
    private void OnDisable()
    {
        GameConfigManager.Instance.skillLogic.AddTimeSkill -= HandleAddTime;
        StopCoroutine(Corotine());
        if (obj != null)
            PhongtothunhoAnimation.KillAnimation(obj.transform);
    }

    private void HandleAddTime()
    {
        //sinh ra object nay
        obj = ObjectPooler.Instance.Spawn(clockPrefab, home.position, Quaternion.identity);
        //phong to thu nho trong vai giay


        StartCoroutine(Corotine());
        //sau do dua ve poool
    }

    public IEnumerator Corotine()
    {
        //no co chay voa day ko vay addimecotine
        Debug.Log("chay zo day ko bro");
        PhongtothunhoAnimation.PlayEffectSmallToBig(obj.transform, 2,1.2f);
        yield return new WaitForSeconds(2);
        ObjectPooler.Instance.Despawn(clockPrefab, obj);
        PhongtothunhoAnimation.KillAnimation(obj.transform);

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
