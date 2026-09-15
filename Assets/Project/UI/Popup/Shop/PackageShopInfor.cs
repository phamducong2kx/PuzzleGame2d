using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PackageShopInfor : MonoBehaviour
{
    public string idPackage = "";
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
    public string keyAddressalbe = "";





    private void Awake()
    {

    }
    private void OnEnable()
    {

    }

    public void Refresh()
    {


        imagePackage.sprite = ShopPopup.Instance.dictionary[keyAddressalbe];
        imagePackage.enabled = true;
    }

    public void Setup(PackageShoppe package, Action onclickCallBack)
    {
        idPackage = package.idPackage;


        keyAddressalbe = package.iconAdressKey;
        imagePackage.enabled = true;
        imagePackage.sprite = ShopPopup.Instance.dictionary[keyAddressalbe];
        amount = package.amount;
        string name = package.namePackageType;
        if (package.typePacakgaeShopee == TypePackageShoppe.GoiSieuCap)
        {
            amountText.gameObject.SetActive(false);
        }
        //else  if (name.Equals("sieucap"))
        // {
        //     amountText.gameObject.SetActive(false);
        //     // amountText.enabled = false;
        // }
        else if (package.typePacakgaeShopee == TypePackageShoppe.VatPhamXu
            || package.typePacakgaeShopee == TypePackageShoppe.VatPhamTangCuong)
        {
            amountText.gameObject.SetActive(true);
            amountText.text = "x" + amount.ToString();
        }
        else
        {
            amountText.text = amount.ToString();
        }



        dolar = package.priceDolar;
        coint = package.priceCoint;
        amount = package.amount;
        typePk = package.typePacakgaeShopee;
        if (package.priceDolar == 0)
        {

            imageCoint.gameObject.SetActive(true);
            signTextDolar.gameObject.SetActive(false);
            priceTextDolar.text = package.priceCoint.ToString();
        }
        if (package.priceCoint == 0)
        {
            imageCoint.gameObject.SetActive(false);
            signTextDolar.gameObject.SetActive(true);
            priceTextDolar.text = package.priceDolar.ToString();
        }


        buytItemButton.onClick.RemoveAllListeners();
        buytItemButton.onClick.AddListener(() =>
        {
            onclickCallBack?.Invoke();
        });


    }
    //public void Setup(string idPackage, Sprite sprite, float priceDolar, float priceCoint, int? amount, TypePackageShoppe type, Action onclickCallBack)
    //{
    //    this.idPackage = idPackage;

    //    //image
    //    //tim image

    //    imagePackage.sprite = sprite;

    //    string name = GameConfigManager.Instance.itemLogic.GetPackageByID(idPackage).namePackageType;
    //    //  Debug.Log("name cua item hien tai la " + name);

    //    if (name.Equals("sieucap"))
    //    {
    //        amountText.gameObject.SetActive(false);
    //    }
    //    else if (type == TypePackageShoppe.VatPhamXu)
    //    {
    //        amountText.gameObject.SetActive(true);
    //        amountText.text = "x" + amount.ToString();
    //    }
    //    else
    //    {
    //        amountText.text = amount.ToString();
    //    }



    //    dolar = priceDolar;
    //    coint = priceCoint;
    //    this.amount = amount ?? 1;
    //    typePk = type;
    //    if (priceDolar == 0)
    //    {

    //        imageCoint.gameObject.SetActive(true);
    //        signTextDolar.gameObject.SetActive(false);
    //        priceTextDolar.text = priceCoint.ToString();
    //    }
    //    if (priceCoint == 0)
    //    {
    //        imageCoint.gameObject.SetActive(false);
    //        signTextDolar.gameObject.SetActive(true);
    //        priceTextDolar.text = priceDolar.ToString();
    //    }


    //    buytItemButton.onClick.RemoveAllListeners();
    //    buytItemButton.onClick.AddListener(() =>
    //    {
    //        onclickCallBack?.Invoke();
    //    });


    //}



    private void OnDisable()
    {
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

