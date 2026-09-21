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

    //trar veef so luong levle co trong game
    public int GetNumberLevelInGame()
    {
        int count = 0;
        var world = levelDatabase.listWorldData;
        //voiws moi word , xem danh sacsh cacs chaoter
        foreach (var x in world)
        {
            var chapter = x.listChapterData;
            foreach (var a in chapter)
            {
                count += a.listLevelData.Count();
            }
        }
        return count;
    }

    //lay scale cua 1 plank thoe id
    public Vector3 GetScaleByIDPLank(string idPlank, int currentLevel)
    {
        var x = GetLevelDataByLevelID(currentLevel).listPlankData.FirstOrDefault(x => x.plankId.Equals(idPlank));
        return new Vector3(x.scaleX, x.scaleY, 0);
    }

    public void InitLevelProgress()
    {
        var numberLevel = GetNumberLevelInGame();
        // SaveManager.Data.highestUnlockLevel = 1;
        for (int i = 1; i <= numberLevel; ++i)
        {
            var newLevel = new LevelProgress();
            newLevel.leveID = i;
            newLevel.isPlaying = false;
            newLevel.isUnlock = false;
            newLevel.star = 0;
            newLevel.isPass = false;
            if (i == 1) newLevel.isUnlock = true;
            SaveManager.Data.levelProgresses.Add(newLevel);
        }
    }

    void Start()
    {

    }


    void Update()
    {

    }
}
