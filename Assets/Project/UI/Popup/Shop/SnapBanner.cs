using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //setup object nay
        packBanner.Setup(listbanner[0], () =>
        {
            //button se lam gi nhi ? cung logic tuong tu nhu 
            ShopPopup.Instance.HandleButtonBuyItem(listbanner[0].idPackage);
        });

        //dua voa lisst
        ShopPopup.Instance.listPackageInfo.Add(packBanner);

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
        //  var sprite = ShopPopup.Instance.listSprite.FirstOrDefault(a => a.name.Equals(list[_index].iconAdressKey));
        packBanner.Setup(list[_index], () =>
           {
               ShopPopup.Instance.HandleButtonBuyItem(list[_index].idPackage);
           });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
