using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using UnityEngine;


[System.Serializable]
public class LevelProgress
{
    //level id 
    public int leveID;
    //da choi chua
    public bool isPlaying = false;
    //so sao  cua level
    public int star = 0;
    //man nay da mo khoa hay chua
    public bool isUnlock = false;
    //level nay da pass hay chua
    public bool isPass = false;
}


[System.Serializable]
public class SkillProgress
{
    public string idSkill;
    public int amount;
    public float cooldownRemaining;

}



[Serializable]
public class PlayerData
{

    //id
    public string userId;

    //id level hiện tại
    public int currentLevel = 1;

    //id level cao nhat 
    public int highestUnlockLevel = 1;

    //id world cao nhat
    public int worldUnlockHighest = 1;

    //world hien tai
    public int currentWorld = 1;
    //id chapter cao nhat
    public int chapterUnlockHighest = 1;

    //chapter hien tai
    public int currentChapter;

    //số lượng coint đang có
    public int coint;

    //id avatar cua player
    public string currentIdAvatar;

    //mot dictionary chua danh sacsh cacs level va da choi level do hay chua
    public List<LevelProgress> levelProgresses = new List<LevelProgress>();


    public List<SkillProgress> listSkill = new List<SkillProgress>();


    //check xemn đang ở ngày mấy nhận quà 
    public int currentDailyReward = 0;

    public long lastClaimOSTicks = 0;

    public long lastClaimDateTime = 0;

    //public bool isGetReward = false;


    public PlayerData()
    {
        userId = Guid.NewGuid().ToString();
        currentLevel = 1;
        highestUnlockLevel = 1;
        coint = 0;
    }
}

