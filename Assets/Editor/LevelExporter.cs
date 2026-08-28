using log4net.Core;
using log4net.Util;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static PlasticGui.Diff.GetDiffPlasticLinkSpec;


public class LevelExporter : EditorWindow
{
    //cấu hình dữ liệu trên giao diện
    private string outputFolder = "Assets/Project/Config/Level/LevelData";
    private string outputFolderLevelDatabse = "Assets/Project/Config/Level/LevelDatabase";
    private int levelIndex = 1;
    private float timerLimit = 180f;
    private float timerWarn = 10f;
    private int numberLevelOfChapter = 10;
    private int numberChapterOfWorld = 5;


    [MenuItem("Tools/ScrewPuzzle/Export Current Scene to LevelData")]

    //theem menu item nafy vaof menu barr, khi click sẽ gọi method showWindow, method phải là static vì nó gọi
    //trực tiếp, file này phải năm trong asset/Editor

    public static void ShowWindow()
    {
        //tạp editor window và hiển thị lên màn hình
        GetWindow<LevelExporter>("Level Exporter");
    }

    //cửa sổ sẽ chạy code này để hiển thị giao diện
    private void OnGUI()
    {
        GUILayout.Label("Export Scene → LevelData", EditorStyles.boldLabel);
        GUILayout.Space(8);

        outputFolder = EditorGUILayout.TextField("Ouput folder", outputFolder);

        levelIndex = EditorGUILayout.IntField("Level Index", levelIndex);

        timerLimit = EditorGUILayout.FloatField("TimerLimit (s)", timerLimit);

        timerWarn = EditorGUILayout.FloatField("TimeWarn (s)", timerWarn);

        numberLevelOfChapter = EditorGUILayout.IntField("Level of chapter", numberLevelOfChapter);

        numberChapterOfWorld = EditorGUILayout.IntField("Chpater of World", numberChapterOfWorld);


        GUILayout.Space(8);

        if (GUILayout.Button("Export now", GUILayout.Height(30)))
        {
            Export();
        }

    }

    private LevelDatabase GetLevelDatabase()
    {
        //tìm kiem trong asset
        var arrayGUID = AssetDatabase.FindAssets("t:LevelDataBase");
        if (arrayGUID.Length == 0)
        {
            //taoj 1 asset 
            Debug.Log("Chua co assset levelDatabase thi tao moi ");

            if (!Directory.Exists(outputFolderLevelDatabse))
            {
                Directory.CreateDirectory(outputFolderLevelDatabse);
            }

            LevelDatabase levelDatabase = ScriptableObject.CreateInstance<LevelDatabase>();
            string fullAsset = Path.Combine(outputFolderLevelDatabse, "LevelDataBase.asset");

            AssetDatabase.CreateAsset(levelDatabase, fullAsset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return levelDatabase;
        }
        else
        {
            string path = AssetDatabase.GUIDToAssetPath(arrayGUID[0]);
            return AssetDatabase.LoadAssetAtPath<LevelDatabase>(path);
        }

    }

    private void AddLevelDataToLevelDatabase(LevelDatabase levelDB, LevelData levelData)
    {
        Debug.Log("level moi co id la " + levelData.levelIndex);
        for (int i = 0; i < levelDB.listWorldData.Count; i++)
        {
            //danh sachs cac chpater trong  1 world
            var listChapter = levelDB.listWorldData[i].listChapterData;
            var x = listChapter.Count;
            if (x == 0 || (x < numberChapterOfWorld && listChapter[x - 1].listLevelData.Count == numberLevelOfChapter))
            {
                //them 1 chapter
                var newChapter = new ChapterData();
                newChapter.idChapterData = x + 1;
                newChapter.chapterName = (x + 1).ToString();
                //add voa dah sach
                listChapter.Add(newChapter);

            }
            if (x == numberChapterOfWorld && listChapter[x - 1].listLevelData.Count == numberLevelOfChapter)
            {
                continue;
            }

            for (int j = 0; j < listChapter.Count; j++)
            {
                var numberLevel = listChapter[j].listLevelData.Count;
                var distanceNumber = numberLevelOfChapter - numberLevel;
                if (distanceNumber > 0)
                {
                    listChapter[j].listLevelData.Add(levelData);
                    //sau đo vào levelProgress trong playerdata

                    var list = SaveManager.Data.levelProgresses;
                    Debug.Log("trong list co " + list.Count + " levle");
                    var newLevel = new LevelProgress();
                    newLevel.leveID = levelData.levelIndex;
                    newLevel.isPlaying = false;
                    newLevel.star = 0;
                    if (levelData.levelIndex == 1)
                    {
                        newLevel.isUnlock = true;
                    }
                    else newLevel.isUnlock = false;
                    list.Add(newLevel);
                    SaveManager.SaveData();
                    Debug.Log("luu thanh cong");
                    var listxy = SaveManager.Data.levelProgresses;
                    Debug.Log("listxy co " + listxy.Count + " level");
                    return;
                }

            }
        }
        var listx = SaveManager.Data.levelProgresses;
        foreach (var x in listx)
        {
            Debug.Log($"level co id la {x.leveID} , trang thai unlock la {x.isUnlock}");
        }
    }

    private void Export()
    {
        List<PlankData> plankDatas = ExportPlank();

        BackgroundData bgData = ExportBackground();

        List<BoltData> boltDatas = ExportBolt();




        // 4. Tạo LevelData instance
        var levelData = ScriptableObject.CreateInstance<LevelData>();
        levelData.levelIndex = levelIndex;
        levelData.timerLimit = timerLimit;
        levelData.timerWanr = timerWarn;
        levelData.coint = 0;
        levelData.listPlankData = plankDatas;
        levelData.listBoltData = boltDatas;
        levelData.bgData = bgData;



        //nếu chưa có đường dẫn thư mục thì tạo đường dẫn thư mục này
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        //đặt tên cho file leveldata sẽ tạo trong đg dẫn thư muc
        string fileName = $"{outputFolder}/level_{levelIndex:000}.asset";

        //tạo file asset
        AssetDatabase.CreateAsset(levelData, fileName);

        //khai bao levelDataBase
        var levelDatabase = GetLevelDatabase();

        //thay doi database
        if (levelIndex != 1)
        {
            AddLevelDataToLevelDatabase(levelDatabase, levelData);
            //luu tahy doi levelDatabse
            EditorUtility.SetDirty(levelDatabase);
        }
        else
        {
            Debug.Log("level index == 1 , hay tao lai level moi ");
        }



        //lưu lại
        AssetDatabase.SaveAssets();

        //refreah laij thuw muc object
        AssetDatabase.Refresh();

    }



    private List<PlankData> ExportPlank()
    {
        //tìm kiếm các instance của compoenent Plank có trên sence
        var listPlank = FindObjectsOfType<Plank>();

        //tao moi mot danh sách plankData để trả về danh sách này
        var listPlankData = new List<PlankData>();

        //duyet danh sach plank tìm dc trên sence
        foreach (var plank in listPlank)
        {

            /*
     public class PlankData
{
    public string plankId;
    public Vector3 position;
    public Vector3 rotation;
    public string hexColor;
    public List<HoleData> listPlankHole = new List<HoleData>();
    public string sortingLayerName;
    public PlankType plankType;
}

     
     */

            // khoi tao 1 plank
            var plankData = new PlankData();

            // gán id cho plank
            plankData.plankId = plank.plankId;

            //gán position cho plnak
            plankData.position = plank.transform.position;

            //gán rotation cho plank
            plankData.rotation = plank.transform.eulerAngles.z;

            //màu sắc

            plankData.hexColor = plank.ColorToString();
            //sử dụng sorting group  : để cho sprie mask của hole chỉ có hiệu lực trong plank này chứ ko đi ra chỗ 

            //sorting goup layer
            //var plankObj = plank.gameObject;
            var sortingGroupRef = plank.GetComponent<SortingGroup>();
            plankData.sortingLayerName = sortingGroupRef.sortingLayerName;

            //type cua plank
            plankData.plankType = plank.plankType;

            // tìm kiếm  danh sách các componenet hole trong danh sách các con của plank
            var listHole_Plank = plank.GetComponentsInChildren<Hole>();

            //duyệt danh sách các hole trên
            foreach (var holePlank in listHole_Plank)
            {
                //taoj 1 cái holeData
                var holeData = new HoleData();

                // xet id cho chole
                holeData.holeId = holePlank.holeId;

                //plank pảent


                //xet possition cho hole
                holeData.positionLocal = holePlank.transform.localPosition;

                //them hoel vao danh sách các hole
                plankData.listPlankHole.Add(holeData);
            }

            //thêm nó vào dnah sách
            listPlankData.Add(plankData);
        }
        return listPlankData;
    }

    private BackgroundData ExportBackground()
    {

        GameObject bgObject = GameObject.Find("background(Clone)");


        var bg = bgObject.GetComponent<BackgroundPlane>();

        BackgroundData bgData = new BackgroundData();






        //possition
        bgData.position = bg.transform.position;

        // sorting group
        bgData.sortingGroupLayer = bg.GetComponent<SortingGroup>().sortingLayerName;


        var listHole = bg.GetComponentsInChildren<Hole>();

        //  Debug.Log("danh sách lustHole trong bg là " + listHole.Count());

        //duyet danh sac bolt
        foreach (var hole in listHole)
        {

            var holeData = new HoleData();

            //bolt id
            holeData.holeId = hole.holeId;

            //position
            holeData.positionLocal = hole.transform.localPosition;

            // thêm vào danh sách
            bgData.listHoleBg.Add(holeData);





        }
        return bgData;


    }

    private List<BoltData> ExportBolt()
    {
        //timf kieems cacs instance componenet bolt
        var listBolt = FindObjectsOfType<Bolt>();

        //danh sahcs botl data
        var listBoltData = new List<BoltData>();

        //duyet danh sac bolt component
        foreach (var bolt in listBolt)
        {

            // khoi tao 1 boltdata
            var boltData = new BoltData();

            //bolt id
            boltData.boltId = bolt.boltId;

            //position
            boltData.position = bolt.transform.position;


            // -> sorting group  ;Cho nay thua ko can thiet , dung sirting layer la đủ rồi nhưng mà kệ ko sửa
            var boltObj = bolt.gameObject;
            boltData.sortingGroup = bolt.GetComponent<SortingGroup>().sortingLayerName;

            // -> danh sách các hole plank
            foreach (var holePlank in bolt.plankHoles)
            {
                boltData.listHoleId.Add(holePlank.holeId);


            }

            //-> hole background
            boltData.holeBackgroundId = bolt.backgroundHole.holeId;

            //them vao danh sacsh
            listBoltData.Add(boltData);

        }
        return listBoltData;

    }
}