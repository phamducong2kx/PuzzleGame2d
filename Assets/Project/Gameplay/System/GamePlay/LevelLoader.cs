using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

//class nay de sinh ra cac prefab
public class LevelLoader : MonoBehaviour
{

    public static LevelLoader Instance;

    [Header("Prefabs")]
    public GameObject plankPrefab;
    public GameObject boltPrefab;
    public GameObject backgroundPrefab;
    public GameObject holePrefab;



    // Runtime state
    private BackgroundPlane spawnedBackground;
    public List<Plank> spawnedPlanks = new List<Plank>();
    public List<Bolt> spawnedBolts = new List<Bolt>();
    private LevelData currentLevelData;
    private Dictionary<string, Hole> map = new Dictionary<string, Hole>();

    public LevelData CurrentLevelData
    {
        get { return currentLevelData; }
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()

    {

    }
    private void Start()
    {

    }

    //tải prefab lên màn hình
    public void LoadLevel(int levelIndex)
    {
        //tìm kiếm levelData từ levelIndex trong levelDatabase
        currentLevelData = GameConfigManager.Instance.levelDatabaseLogic.GetLevelDataByLevelID(levelIndex);

        //spawn object
        SpawnBackground(currentLevelData.bgData);
        SpawnPlanks(currentLevelData.listPlankData);
        SpawnBolts(currentLevelData.listBoltData);

        var x = ObjectPooler.Instance.poolDict[plankPrefab];
        if (x != null)
        {
            Debug.Log("so luong plank trong queue la " + x.Count);
        }
        if (spawnedPlanks[0] == spawnedPlanks[1])
        {
            Debug.Log("ca 2 cung tham chieu toi 1 doi tuong");

        }
    }
    private void SpawnBackground(BackgroundData bgData)
    {
        if (bgData == null || backgroundPrefab == null) return;

        //tạo mới 1 cái backgroudn từ prefab , nếu có thfi lấy trong pool
        //  spawnedBackground = Instantiate(backgroundPrefab);
        var spawnBg = ObjectPooler.Instance.Spawn(backgroundPrefab, bgData.position, backgroundPrefab.transform.rotation);
        // spawnedBackground.transform.position = bgData.position;

        // Sorting Group
        var sortingGroup = spawnBg.GetComponent<SortingGroup>();
        if (sortingGroup == null)
        {
            sortingGroup = spawnBg.AddComponent<SortingGroup>();
        }
        else
        {
            sortingGroup.sortingLayerName = bgData.sortingGroupLayer;
        }


        //laasy compoent bg
        // var bgComponent = spawnBg.GetComponent<BackgroundPlane>();
        spawnedBackground = spawnBg.GetComponent<BackgroundPlane>();
        // Spawn holes trong background
        foreach (var holeData in bgData.listHoleBg)
        {
            if (string.IsNullOrEmpty(holeData.holeId)) continue;

            // lay hole ra tu pool
            var holeObj = ObjectPooler.Instance.Spawn(holePrefab, UnityEngine.Vector3.one, holePrefab.transform.rotation);
            // GameObject holeObj = Instantiate(holePrefab, spawnedBackground.transform);
            holeObj.transform.SetParent(spawnedBackground.transform);
            holeObj.transform.localPosition = holeData.positionLocal;

            Hole hole = holeObj.GetComponent<Hole>();
            if (hole == null) hole = holeObj.AddComponent<Hole>();

            hole.holeId = holeData.holeId;
            hole.SetAsBackgroundHole();

            spawnedBackground.backgroundHoles.Add(hole);

            //duea vao map
            map[hole.holeId] = hole;

        }
    }
    private void SpawnPlanks(List<PlankData> plankDatas)
    {

        if (plankDatas == null || plankPrefab == null) return;

        //dem so luong plank trong  quêu 

        //var x = ObjectPooler.Instance.poolDict[plankPrefab];
        //if (x != null)
        //{
        //    Debug.Log("so luong plank trong queue la " + x.Count);
        //}

        foreach (var plankData in plankDatas)
        {

            if (string.IsNullOrEmpty(plankData.plankId)) continue;

            //tạo object plank từ prefab , rồi đưa vòa pool
            // GameObject plankObj = Instantiate(plankPrefab);
            var plankObj = ObjectPooler.Instance.Spawn(plankPrefab, plankData.position, UnityEngine.Quaternion.Euler(0, 0, plankData.rotation));

            //setup no la static rigibody // -0.06 . 1.94 . -0.1

            //vị trí
            //plankObj.transform.position = plankData.position;

            //rotation
            //plankObj.transform.rotation = Quaternion.Euler(0, 0, plankData.rotation);

            //tìm kiếm componenet plank
            Plank plank = plankObj.GetComponent<Plank>();

            //set up rigibody
            plank.SetDynamicRigibody();
            //id plank
            plank.plankId = plankData.plankId;

            Debug.Log("id cua plank la " + plank.plankId);
            // Sorting Group
            var sortingGroup = plankObj.GetComponent<SortingGroup>();
            if (sortingGroup == null)
                sortingGroup = plankObj.AddComponent<SortingGroup>();
            sortingGroup.sortingLayerName = plankData.sortingLayerName;

            //thêm nó vòa danh sách quản lí các plank
            spawnedPlanks.Add(plank);

            // Spawn holes trong plank
            if (plankData.listPlankHole == null) continue;

            foreach (var holeData in plankData.listPlankHole)
            {
                if (string.IsNullOrEmpty(holeData.holeId)) continue;


                // lay hole ra tu pool
                var holeObj = ObjectPooler.Instance.Spawn(holePrefab, UnityEngine.Vector3.zero, holePrefab.transform.rotation);
                // GameObject holeObj = Instantiate(holePrefab, spawnedBackground.transform);
                holeObj.transform.SetParent(plankObj.transform);
                holeObj.transform.localPosition = holeData.positionLocal;


                //khởi tạo hole từ prefab
                //  GameObject holeObj = Instantiate(holePrefab, plankObj.transform);

                //vị tris local so với plank
                //  holeObj.transform.localPosition = holeData.positionLocal;

                //lấy compoenet hole
                Hole hole = holeObj.GetComponent<Hole>();
                if (hole == null) hole = holeObj.AddComponent<Hole>();

                //xet id do hole nay
                hole.holeId = holeData.holeId;

                //them vao map
                map[hole.holeId] = hole;

                //set làm con của plank
                hole.SetPlankParent(plank);

                //add vào danh sách plank
                plank.holes.Add(hole);
            }
        }

        //slosnah xem 2 plank co khac nhau ko

    }
    private void SpawnBolts(List<BoltData> boltDatas)
    {
        if (boltDatas == null || boltPrefab == null) return;

        foreach (var boltData in boltDatas)
        {
            var boltObj = ObjectPooler.Instance.Spawn(boltPrefab, boltData.position, boltPrefab.transform.rotation);
            //  GameObject boltObj = Instantiate(boltPrefab);
            //boltObj.transform.position = boltData.position;

            // Sorting Group khong can thiet , dùng sorting layer là đủ 
            var sortingGroup = boltObj.GetComponent<SortingGroup>();
            if (sortingGroup == null)
                sortingGroup = boltObj.AddComponent<SortingGroup>();
            sortingGroup.sortingLayerName = boltData.sortingGroup;

            Bolt bolt = boltObj.GetComponent<Bolt>();
            bolt.SetStaticRigibody();
            bolt.boltId = boltData.boltId;
            bolt.backgroundHole = map[boltData.holeBackgroundId];
            foreach (var x in boltData.listHoleId)
            {
                bolt.plankHoles.Add(map[x]);
            }

            //add comoponent hingjoined
            bolt.AttachConnectToHole_OfBolt(bolt.backgroundHole, bolt.plankHoles);

            spawnedBolts.Add(bolt);
        }
    }
    public void ClearLevel()
    {
        //đưa nó về pool

        foreach (var plank in spawnedPlanks)
        {
            //  if (plank != null) Destroy(plank.gameObject);
            //dua plank ve bool

            ObjectPooler.Instance.Despawn(plankPrefab, plank.gameObject);
            //dua hole trong plank ve pool
            foreach (var hole in plank.holes)
            {
                ObjectPooler.Instance.Despawn(holePrefab, hole.gameObject);
            }
            plank.holes.Clear();
        }

        spawnedPlanks.Clear();

        foreach (var bolt in spawnedBolts)
        {
            ObjectPooler.Instance.Despawn(boltPrefab, bolt.gameObject);



            bolt.plankHoles.Clear();
            bolt.backgroundHole = null;
            //   if (bolt != null) Destroy(bolt.gameObject);
        }

        spawnedBolts.Clear();


        ObjectPooler.Instance.Despawn(backgroundPrefab, spawnedBackground.gameObject);
        foreach (var hole in spawnedBackground.backgroundHoles)
        {
            ObjectPooler.Instance.Despawn(holePrefab, hole.gameObject);
        }
        spawnedBackground.backgroundHoles.Clear();
        //   Destroy(spawnedBackground);



        //xopas map
        map.Clear();


        //
    }
}
