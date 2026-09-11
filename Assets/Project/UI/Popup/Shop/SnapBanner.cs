using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class SnapBanner : MonoBehaviour
{
    // Start is called before the first frame update
    public Button previous;
    public Button next;
    public int indexList = 0;
    public PackageShopInfor packBanner = null;
    public PackageShopInfor packageShopInfoBannerPrefab;
    void Start()
    {
        SetupPreviousButton();
        SetupNextButton();
    }

    //gen cacs package co banner
    public void SetupFirst() // prefab ,lam, con cua contetn
    {
        //tao obnject trong pannel va gan cho bien thma chieu
        packBanner = Instantiate(packageShopInfoBannerPrefab, transform);


        //danh sacsh cacs package coa banner
        var listbanner = GameConfigManager.Instance.itemLogic.GetPackageByBanner();
        var x = listbanner[0];

        //setup object nay
        packBanner.Setup(x.idPackage, x.iconAdressKey, x.priceDolar, x.priceCoint, x.amount, x.typePacakgaeShopee, () =>
        {
            //button se lam gi nhi ? cung logic tuong tu nhu 
            ShopPopup.Instance.HandleButtonBuyItem(x.idPackage);
        });

        //set up 2 nut previsou va next
        SetupButton(listbanner.Count);
    }

    public void SetupButton(int count)
    {
        if (indexList == 0)
        {
            previous.gameObject.SetActive(false);
            next.gameObject.SetActive(true);
        }

        if (indexList == count - 1)
        {
            previous.gameObject.SetActive(true);
            next.gameObject.SetActive(false);
        }

        if (indexList > 0 && indexList < count - 1)
        {
            previous.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
        }


    }
    private void SetupPreviousButton()
    {
        var list = GameConfigManager.Instance.itemLogic.GetPackageByBanner();

        previous.onClick.AddListener(() =>
        {
            --indexList;
            ButtonApparence(list, indexList);

        });
    }

    private void SetupNextButton()
    {
        var list = GameConfigManager.Instance.itemLogic.GetPackageByBanner();
        next.onClick.AddListener(() =>
        {
            ++indexList;
            ButtonApparence(list, indexList);
        });
    }

    private void ButtonApparence(List<PackageShoppe> list, int _index)
    {
        SetupButton(list.Count);

        packBanner.Setup(list[_index].idPackage, list[_index].iconAdressKey, list[_index].priceDolar, list[_index].priceCoint, list[_index].amount, list[_index].typePacakgaeShopee, () =>
        {
            ShopPopup.Instance.HandleButtonBuyItem(list[_index].idPackage);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
