using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankTypeLogic : MonoBehaviour
{
    public PlankTypeData plankTypeData;

    public Dictionary<PlankType, string> dictionary = new Dictionary<PlankType, string>();

    private void Awake()
    {
        SetUpDictionary();
    }
    private void Start()
    {

    }
    private void Update()
    {

    }
    //dua gia tri trong list prefab vao dic
    public void SetUpDictionary()
    {
        var listPlankTypeInfo = plankTypeData.list;
        foreach (var x in listPlankTypeInfo)
        {
            dictionary[x.type] = x.addressKey;
        }
    }


    //timf kiếm keyAdrers từ type
    public string GetKeyAddressFromType(PlankType plankType)
    {
        return dictionary[plankType];
    }


}
