using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.ReloadAttribute;

public class PopupConfirm : MonoBehaviour
{
    public PackageShopInfor packageXacNhanPrefab;
    public Button exxistButton;
    public RectTransform pannelList;


    void Start()
    {
        SetupButotnExist();
    }

    public void OpenPopup(PackageShoppe package)
    {
        gameObject.SetActive(true);
        packageXacNhanPrefab.Setup(package.idPackage, package.iconOverrite, package.priceDolar, package.priceCoint, package.amount, package.typePacakgaeShopee, () =>
        {
            HandleBuy(package);
        });

    }
    public void SetupButotnExist()
    {
        exxistButton.onClick.RemoveAllListeners();
        exxistButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void HandleBuy(PackageShoppe package)
    {
        //liet ke danh sách cacs item trong package 
        foreach (var x in package.listItemId)
        {
            //voi moi item,lay id cua tiem do 
            var idItem = x.idItem;
            //tim dc item do
            var item = GameConfigManager.Instance.itemLogic.GetItemInfoById(idItem);
            //xem no la loai gi , add vao luon , hoat anh sau
            var amount = x.amount * package.amount;
            GameConfigManager.Instance.playerDataLogic.AddResource(item.type, "1", amount);
            //chay 1 event 
            //animation , khi maf nos 
            //phat event : danh sách item , vị trí sinh ra, vị trí đến , logic sử lí
            EventManager.InvokeGetItem();

        }

    }


    // Update is called once per frame
    void Update()
    {

    }
}
