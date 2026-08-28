using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankTypeLogic : MonoBehaviour
{
    public PlankTypeData plankTypeData;

    public Dictionary<PlankType, GameObject> dictionary = new Dictionary<PlankType, GameObject>();

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
            dictionary[x.type] = x.prefab;
        }
    }
    //ham lay prefab theo type
    public GameObject GetPrefabByPlankType(PlankType plankType)
    {
        return dictionary[plankType];
    }

}
