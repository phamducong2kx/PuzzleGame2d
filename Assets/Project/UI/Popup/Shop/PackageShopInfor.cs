using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackageShopInfor : MonoBehaviour
{
    public string idPackage;
    public Image imagePackage;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI priceTextDolar;
    public float dolar;
    public float coint;
    public int amount;
    public TypePackageShoppe typePk;
    public TextMeshProUGUI signTextDolar;
    public Image imageCoint;
    public Button buytItemButton;



    private void Awake()
    {

    }

    public void Setup(string idPackage, Sprite iconPackage, float priceDolar, float priceCoint, int amount, TypePackageShoppe type, Action onclickCallBack)
    {
        this.idPackage = idPackage;
        imagePackage.sprite = iconPackage;

        if (type == TypePackageShoppe.VatPhamXu)
        {

            amountText.text = "x" + amount.ToString();
        }
        else
        {
            amountText.text = amount.ToString();

        }

        dolar = priceDolar;
        coint = priceCoint;
        this.amount = amount;
        typePk = type;
        if (priceDolar == 0)
        {

            imageCoint.gameObject.SetActive(true);
            signTextDolar.gameObject.SetActive(false);
            priceTextDolar.text = priceCoint.ToString();
        }
        if (priceCoint == 0)
        {
            imageCoint.gameObject.SetActive(false);
            signTextDolar.gameObject.SetActive(true);
            priceTextDolar.text = priceDolar.ToString();
        }

        buytItemButton.onClick.RemoveAllListeners();
        buytItemButton.onClick.AddListener(() =>
        {
            onclickCallBack?.Invoke();
        });


    }


    public void Setup(string idPackage, Sprite iconPackage, float priceDolar, float priceCoint)
    {
        this.idPackage = idPackage;
        imagePackage.sprite = iconPackage;

        if (priceDolar == 0)
        {

            imageCoint.gameObject.SetActive(true);
            signTextDolar.gameObject.SetActive(false);
            priceTextDolar.text = priceCoint.ToString();
        }
        if (priceCoint == 0)
        {
            imageCoint.gameObject.SetActive(false);
            signTextDolar.gameObject.SetActive(true);
            priceTextDolar.text = priceDolar.ToString();
        }


    }







    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

