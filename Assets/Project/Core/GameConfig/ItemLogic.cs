using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    //tham chieu toi file itemconfig 
    [SerializeField] private ItemData itemDataConfig;
    [SerializeField] private ShopeDatabaseSO itemShopeeConfig;

    //1 map để chứa id - item
    public Dictionary<string, ItemInfo> itemDict = new Dictionary<string, ItemInfo>();




    private void Awake()
    {
        InnitDatabase();
    }

    private void InnitDatabase()
    {
        itemDict.Clear();
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

    //lay skill theo id
    public ItemInfo GetItemInfoById(string id)
    {
        return itemDict[id];
    }

    //lay danh sách các gói vật phẩm theo type
    public List<PackageShoppe> GetPackageByType(TypePackageShoppe type)
    {
        var list = new List<PackageShoppe>();
        var listPackage = itemShopeeConfig.listPackgeShoppe;
        foreach (var x in listPackage)
        {
            if (x.typePacakgaeShopee == type) list.Add(x);
        }
        return list;
    }

    //lay danh sacsh cacs gois va pham co banner
    public List<PackageShoppe> GetPackageByBanner()
    {
        var list = new List<PackageShoppe>();
        var listPackage = itemShopeeConfig.listPackgeShoppe;
        foreach (var x in listPackage)
        {
            if (x.hasBanner == true) list.Add(x);
        }
        return list;
    }

    //get  1 item package theo id 
    public PackageShoppe GetPackageByID(string idPack)
    {

        var listPackage = itemShopeeConfig.listPackgeShoppe.FirstOrDefault(x => x.idPackage.Equals(idPack));
        return listPackage;
    }


    void Start()
    {

    }


    void Update()
    {

    }
}
