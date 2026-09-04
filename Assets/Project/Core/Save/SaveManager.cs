using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveManager
{
    private const string SAVE_KEY = "PLAYER_DATA_SAVE";
    //du lioeu nguoi choi tren ram
    public static PlayerData Data { get; private set; }

    //tai du lieu khi vua moi mo game
    public static void LoadData()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            Data = JsonUtility.FromJson<PlayerData>(json);
            Data.highestUnlockLevel = 1;
            for (int i = 0; i < 8; ++i)
            {
                Data.levelProgresses[i].isPlaying = false;
                Data.levelProgresses[i].isUnlock = false;
                Data.levelProgresses[i].star = 0;
                Data.levelProgresses[i].isPass = false;


            }
            Data.levelProgresses[0].isUnlock = true;
            // Data.coint = 0;




            SaveData();
        }
        else
        {
            Data = new PlayerData();

            SaveData();
        }
    }

    public static void SaveData()
    {
        if (Data == null) return;
        string json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    //mở khóa level tiếp theo
    public static bool UnlockNewLevel(int star)
    {
        if (Data == null) return false;
        if (Data.currentLevel == Data.highestUnlockLevel)
        {
            //update level cu
            var levelOld = SaveManager.Data.levelProgresses[Data.highestUnlockLevel - 1];
            levelOld.isUnlock = true;
            levelOld.isPlaying = true;
            levelOld.star = star;
            levelOld.isPass = true;

            //update level moi 
            ++Data.highestUnlockLevel;
            var level = SaveManager.Data.levelProgresses[Data.highestUnlockLevel - 1];
            level.isUnlock = true;
            level.isPlaying = false;
            level.star = 0;
            level.star = 0;
            level.isPass = false;


            SaveData();


            return true;

        }
        return false;

    }

    //set curent level
    public static void SetCurrenLevel(int level)
    {
        Data.currentLevel = level;
        SaveData();
    }

    //set curent world
    public static void SetCurrenWorld(int world)
    {
        Data.currentWorld = world;
        SaveData();
    }

    //set current chapter
    public static void SetCurrenChapter(int chapterID)
    {
        Data.currentChapter = chapterID;
        SaveData();
    }
    //coongj coint
    public static void AddCoint(int coint)
    {
        Data.coint += coint;
        SaveData();
    }



    //clear data
    public static void ClearData()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        SaveData();
    }





    public static void Save_Get_dailyReward_Successfull()
    {



        //luu ngay nhan qua
        Data.currentDailyReward += 1;
        //luu time
        Data.lastClaimDateTime = DateTime.UtcNow.Ticks;
        Data.lastClaimOSTicks = (uint)System.Environment.TickCount;
        //luu data
        SaveData();
    }

    public static void Save_Star(int idLevel, int star)
    {
        var level = Data.levelProgresses[idLevel - 1];
        if (level.star < star)
        {
            level.star = star;
            SaveData();
        }

    }

    //thay doi so lunog skill sau khi dung skill

}
