using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlankType
{
    TypeRectangleBig,
    TypeRectangleSamll,
    TypeTriagle,
    TypeCicrleBig,
    TypeCirlceSmall,
    Sqaure
}

[CreateAssetMenu(fileName = "plankTypeData", menuName = "ScrewPuzzle/PlankTypeData")]
public class PlankTypeData : ScriptableObject
{
    public List<PlankTypeInfo> list = new List<PlankTypeInfo>();
}

[Serializable]
public class PlankTypeInfo
{
    public PlankType type;
    public GameObject prefab;
}