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

    public async void Setup(string idPackage, string bannerAddressable, float priceDolar, float priceCoint, int? amount, TypePackageShoppe type, Action onclickCallBack)
    {
        this.idPackage = idPackage;

        //image
        //imagePackage.sprite = iconPackage;
        await LoadImageFromCache(bannerAddressable);

        string name = GameConfigManager.Instance.itemLogic.GetPackageByID(idPackage).namePackageType;
        Debug.Log("name cua item hien tai la " + name);

        if (name.Equals("sieucap"))
        {
            amountText.gameObject.SetActive(false);
        }
        else if (type == TypePackageShoppe.VatPhamXu)
        {
            amountText.gameObject.SetActive(true);
            amountText.text = "x" + amount.ToString();
        }
        else
        {
            amountText.text = amount.ToString();
        }



        dolar = priceDolar;
        coint = priceCoint;
        this.amount = amount ?? 1;
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

    private async Task LoadImageFromCache(string key)
    {
        var handleImage = Addressables.LoadAssetAsync<Sprite>(key);
        var sprite = await handleImage.Task;
        if (handleImage.Status == AsyncOperationStatus.Succeeded)
        {
            imagePackage.sprite = sprite;
            Debug.Log("load anh thanh cong");

        }
        else
        {
            Debug.Log("Load anh khong thanh cong");
        }
        Addressables.Release(handleImage);
    }



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

