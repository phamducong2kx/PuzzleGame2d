using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CoinView : MonoBehaviour
{

    public TextMeshProUGUI text_coin;

    private void Awake()
    {

    }


    public int AllCointKiemDuoc()
    {

        string text_coint = text_coin.text.ToString();


        return int.Parse(text_coint);
    }




    public void UpdateTextCoin()
    {
        text_coin.text = (int.Parse(text_coin.text) + 1).ToString();

        transform.DOKill();
        transform.localScale = Vector3.one;

        transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo).SetLink(gameObject, LinkBehaviour.KillOnDisable);
    }


    private void OnDisable()
    {
        transform.DOKill();
        transform.localScale = Vector3.one;
    }


    void Update()
    {

    }
}
