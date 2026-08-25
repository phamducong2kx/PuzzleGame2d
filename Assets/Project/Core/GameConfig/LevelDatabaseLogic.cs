using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelDatabaseLogic : MonoBehaviour
{
    [SerializeField] private LevelDatabase levelDatabase;


    //tim so luong chapter co trong world
    public int GetNumberOfChapter(int wordlID)
    {

        var world = levelDatabase.listWorldData.FirstOrDefault(x => x.idWorldData == wordlID);
        if (world != null) return world.listChapterData.Count;
        return 0;

    }

    //tim xem chapter  co bao nhieu level
    public int GetNumberOfLevel(int chapterID, int worldID)
    {

        var world = levelDatabase.listWorldData.FirstOrDefault(x => x.idWorldData == worldID);
        if (world != null)
        {
            var chapter = world.listChapterData.FirstOrDefault(x => x.idChapterData == chapterID);
            if (chapter != null) return chapter.listLevelData.Count;

        }
        return 0;
    }

    //tim xem chapter cao nhat da mo khoa cua world hien tai
    public int GetChapterHighestOfCurrentWorld(int worldID)
    {
        //neu word hien tai khong phai world cao nhat
        if (worldID < SaveManager.Data.worldUnlockHighest)
        {
            //tra ve chapter cuoi cung cua world do
            var world = levelDatabase.listWorldData.FirstOrDefault(x => x.idWorldData == worldID);
            var last_chapter = world.listChapterData[world.listChapterData.Count - 1];
            return last_chapter.idChapterData;
        }
        else
        {
            //tim levelcao nhat
            int highestlevel = SaveManager.Data.highestUnlockLevel;
            //tim chapter tu level
            int chapterID = highestlevel / 10;
            if (highestlevel % 10 != 0) chapterID += 1;
            return chapterID;
        }

    }

    //tim kirm leveldata theo levelID
    public LevelData GetLevelDataByLevelID(int levelId)
    {
        //xem level nay thuoc world may
        int worldID = Mathf.CeilToInt((float)levelId / 50);
        int chapterId = Mathf.CeilToInt((float)levelId / 10);
        var world = levelDatabase.listWorldData.FirstOrDefault(x => x.idWorldData == worldID);
        if (world == null) return null;
        var chapter = world.listChapterData.FirstOrDefault(x => x.idChapterData == chapterId);
        if (chapter == null) return null;
        return chapter.listLevelData.FirstOrDefault(x => x.levelIndex == levelId);
    }

    //tim chapter theo level
    public int GetChapterByLevel(int levelId)
    {

        int chapterId = Mathf.CeilToInt((float)levelId / 10);
        return chapterId;
    }

    void Start()
    {

    }


    void Update()
    {

    }
}
