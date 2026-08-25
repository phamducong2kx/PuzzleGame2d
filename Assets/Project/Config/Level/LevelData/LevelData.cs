using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScrewPuzzle/Level Data")]



//thêm menu để tạo file aset từ  class này
public class LevelData : ScriptableObject
{
    public int levelIndex = 1;

    public float timerLimit = 180f;

    public float timerWanr = 10f;

    public int coint = 0;

    public float thresh_time_star3 = 150;
    public float thresh_time_star2 = 100;
    public float thresh_time_star1 = 40;

    public List<PlankData> listPlankData = new List<PlankData>();

    public List<BoltData> listBoltData = new List<BoltData>();

    public BackgroundData bgData;
}

[System.Serializable]
public class PlankData
{
    public string plankId;
    public Vector3 position;
    public float rotation;
    public List<HoleData> listPlankHole = new List<HoleData>();
    public string sortingLayerName;

}

[System.Serializable]
public class HoleData
{
    public string holeId;
    public Vector3 positionLocal;

}

[System.Serializable]
public class BoltData
{
    public string boltId;
    public Vector3 position;
    public List<string> listHoleId = new List<string>();
    public string holeBackgroundId;

    public string sortingGroup;
}

[System.Serializable]
public class BackgroundData
{
    public List<HoleData> listHoleBg = new List<HoleData>();
    public Vector3 position;
    public string sortingGroupLayer;

}