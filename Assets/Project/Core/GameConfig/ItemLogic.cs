using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    //tham chieu toi file itemconfig 
    [SerializeField] private ItemData itemDataConfig;

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



    void Start()
    {

    }


    void Update()
    {

    }
}
