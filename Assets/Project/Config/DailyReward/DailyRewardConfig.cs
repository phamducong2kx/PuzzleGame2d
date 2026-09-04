using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DailyReward", menuName = "ScrewPuzzle/DilyReward")]
public class DailyRewardConfig : ScriptableObject
{
    public List<DailyReward> dailyRewards = new List<DailyReward>();


}


[Serializable]
public class DailyReward
{
    public int day = 1;
    public List<Item_DailyReward> listRewardItem = new List<Item_DailyReward>();

}

[Serializable]
public class Item_DailyReward
{
    public string idItem;
    public int amount = 0;
}
