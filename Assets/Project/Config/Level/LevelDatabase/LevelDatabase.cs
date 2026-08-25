using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "yesss", menuName = "ScrewPuzzle/Level Database")]
public class LevelDatabase : ScriptableObject
{
    public List<WorldData> listWorldData = new List<WorldData>();


}


[Serializable]
public class ChapterData
{
    public int idChapterData;
    public string chapterName;
    public List<LevelData> listLevelData = new List<LevelData>();
}
[Serializable]
public class WorldData
{
    public int idWorldData;
    public string worldName;
    public Sprite workBadge;
    public List<ChapterData> listChapterData = new List<ChapterData>();

}
