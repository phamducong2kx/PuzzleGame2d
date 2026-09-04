using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData", menuName = "ScrewPuzzle/ItemData")]
public class ItemData : ScriptableObject
{
    public List<ItemInfo> listItem;
}


[Serializable]
public class ItemInfo
{
    public string id;

    public Sprite icon;

    public ItemType type;
}

public enum ItemType
{
    Coint,
    Skill,
}

