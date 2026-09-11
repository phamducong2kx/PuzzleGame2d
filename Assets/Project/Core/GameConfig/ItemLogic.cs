using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    //tham chieu toi các file SO
    [SerializeField] private ItemData itemDataConfig;
    [SerializeField] private ShopeDatabaseSO itemShopeeConfig;

    //dư liệu trên ram
    public Dictionary<string, ItemInfo> itemDict = new Dictionary<string, ItemInfo>();
    public List<PackageShoppe> runtimePackageList = new List<PackageShoppe>();






    private void Awake()
    {

        InnitDatabase();
    }

    private void InnitDatabase()
    {

        //nap dictionary item vao ram
        itemDict.Clear();
        if (itemDataConfig != null)
        {
            foreach (var x in itemDataConfig.listItem)
            {
                if (!itemDict.ContainsKey(x.id))
                {
                    itemDict[x.id] = x;
                }
                else
                {
                    Debug.Log("item bi trung lap");
                }
            }
        }

        //nap anh sasch cac gói package vào ram
        runtimePackageList.Clear();
        if (itemShopeeConfig != null)
        {
            runtimePackageList = itemShopeeConfig.listPackgeShoppe;
        }


    }



    #region GETTERS


    //lay skill theo id
    public ItemInfo GetItemInfoById(string id)
    {
        if (itemDict.TryGetValue(id, out ItemInfo itemInfor)) return itemInfor;
        return null;
    }


    //lay danh sách các gói vật phẩm theo type
    public List<PackageShoppe> GetPackageByType(TypePackageShoppe type)
    {
        return runtimePackageList.Where(x => x.typePacakgaeShopee == type).ToList();
    }


    //lay danh sacsh cacs gois vat pham co banner
    public List<PackageShoppe> GetPackageByBanner()
    {
        return runtimePackageList.Where(x => x.hasBanner).ToList();
    }


    //get  1 item package theo id 
    public PackageShoppe GetPackageByID(string idPack)
    {

        return itemShopeeConfig.listPackgeShoppe.FirstOrDefault(x => x.idPackage.Equals(idPack));

    }

    #endregion





    void Start()
    {

    }


    void Update()
    {

    }


}
