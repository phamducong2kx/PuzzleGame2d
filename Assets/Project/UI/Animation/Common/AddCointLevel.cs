using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddCointLevel : MonoBehaviour
{

    public GameObject cointPrefab;


    private void OnEnable()
    {
        EventManager.OnUnlockLevel += HandleLevelCoint;
    }



    private void OnDisable()
    {
        EventManager.OnUnlockLevel -= HandleLevelCoint;
        StopCoroutine(AddCointRoutine());
    }


    public GameObject SpawnObject()
    {
        var coint = ObjectPooler.Instance.Spawn(cointPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        return coint;
    }

    private void HandleLevelCoint()
    {
        StartCoroutine(AddCointRoutine());
        //sinhra object
    }

    public IEnumerator AddCointRoutine()
    {
        yield return new WaitForSeconds(1f);

        var obj = SpawnObject();

        //target
        var target = UIManager.Instance.topBarZone.cointText.transform.position;
        //bay đến coint
        obj.transform.DOMove(target, 0.7f).SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                //so count kiem dc o level nay
                int levelCoint = UIManager.Instance.gameplayPannel.coinview.AllCointKiemDuoc();

                UIManager.Instance.topBarZone.AddCoint(levelCoint);

                //dua no ve pool
                ObjectPooler.Instance.Despawn(cointPrefab, obj);


            });

    }


    void Start()
    {

    }


    void Update()
    {

    }
}
