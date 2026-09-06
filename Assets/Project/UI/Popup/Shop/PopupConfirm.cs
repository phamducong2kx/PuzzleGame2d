using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.ReloadAttribute;

public class PopupConfirm : MonoBehaviour
{
    public IconItemBuy itemPrefab;
    public PackageShopInfor packageXacNhanPrefab;
    public Button exxistButton;
    public RectTransform pannelList;
    public List<RectTransform> listiconItem;

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
        //cho nutys mua vô hiệu đã
        packageXacNhanPrefab.buytItemButton.enabled = false;

        //vo hieu hoa nut ẽisty
        exxistButton.interactable = false;


        //liet ke danh sách cacs item trong package 
        foreach (var x in package.listItemId)
        {
            //voi moi item,lay id cua tiem do 
            var idItem = x.idItem;

            //tim dc item do
            var item = GameConfigManager.Instance.itemLogic.GetItemInfoById(idItem);

            //xem no la loai gi , add vao luon , hoat anh sau
            var amount = x.amount * package.amount;

            //cong iteem vao kho 
            GameConfigManager.Instance.playerDataLogic.BuyResource(item.type, item.id, amount);


            //tao 1 object tu prefab;
            var itemReward = ObjectPooler.Instance.Spawn(itemPrefab.gameObject, new Vector3(0, 0, 0), itemPrefab.transform.rotation);

            //set up cho no
            var componentItem = itemReward.GetComponent<IconItemBuy>();
            componentItem.Setup(item.icon, amount);


            //Đưa vào danh sách để quản lí 

            listiconItem.Add(itemReward.GetComponent<RectTransform>());

        }

        //trừ tiền
        GameConfigManager.Instance.playerDataLogic.RemoveCoint(int.Parse(package.priceCoint.ToString()));

        //chay aniamtion
        var target = ShopPopup.Instance.cointText;
        AnimationManager.Instance.gamePlayAnimation.cointBurst.PlayAnimationBuyItem(listiconItem, pannelList, target.rectTransform,
            () =>
        {
            //thay doi tetx trong tarrget
            target.text = (SaveManager.Data.coint).ToString();
        },
            () =>
            {
                //khi tat car item da den noi , mo khoa  nut butotn va exixts
                //cho nutys mua vô hiệu đã
                packageXacNhanPrefab.buytItemButton.enabled = true;

                //vo hieu hoa nut ẽisty
                exxistButton.interactable = true;
            }

        );

    }


    // Update is called once per frame
    void Update()
    {

    }
}
