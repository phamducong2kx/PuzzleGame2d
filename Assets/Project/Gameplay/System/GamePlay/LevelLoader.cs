using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


//class nay de sinh ra cac prefab
public class LevelLoader : MonoBehaviour
{

    public static LevelLoader Instance;


    [Header("Prefabs")]
    public GameObject boltPrefab;
    public GameObject backgroundPrefab;
    public GameObject holePrefab;



    // Runtime state
    public BackgroundPlane spawnedBackground;
    public List<Plank> spawnedPlanks = new List<Plank>();
    public List<Bolt> spawnedBolts = new List<Bolt>();
    private LevelData currentLevelData;
    private Dictionary<string, Hole> map = new Dictionary<string, Hole>();
    private Dictionary<PlankType, GameObject> dic = new Dictionary<PlankType, GameObject>();
    private List<AsyncOperationHandle> listHandle = new List<AsyncOperationHandle>();

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

    private void OnDestroy()
    {
        foreach (var x in listHandle)
        {
            Addressables.Release(x);
        }
    }
    private void Start()
    {

    }

    //tải prefab lên màn hình
    public async Task LoadLevel(int levelIndex)
    {
        //tìm kiếm levelData từ levelIndex trong levelDatabase
        currentLevelData = GameConfigManager.Instance.levelDatabaseLogic.GetLevelDataByLevelID(levelIndex);

        //sau khi tìm xong thì xem trong level này có những loại prefab nào để chỉ tải lên 
        //những prefab đó từ ổ đĩa thay vì tải hết
        //await UpLoadPrefabPlank();
        await UploadPrefabPlank2();

        //spawn các object trong game
        SpawnBackground(currentLevelData.bgData);
        SpawnPlanks(currentLevelData.listPlankData);
        SpawnBolts(currentLevelData.listBoltData);



    }
    //lan 2 truy cap lay anh thi ssao ta
    private async Task UpLoadPrefabPlank()
    {
        // var list = new List<string>();
        //xet dnah sacsh plank data
        foreach (var a in currentLevelData.listPlankData)
        {
            //  check neu da co prefab cua type nay thi return luon
            if (dic.ContainsKey(a.plankType))
            {
                //ve luon khong can tai len ram
                continue;
            }
            // tim kiem key address tu type
            var keyAddress = GameConfigManager.Instance.plankTypeLogic.GetKeyAddressFromType(a.plankType);
            // tải prefab tương ứng lên ram
            var handle = Addressables.LoadAssetAsync<GameObject>(keyAddress);
            listHandle.Add(handle);
            var obj = await handle.Task;
            //sau khi tari xong thì đưa obj và keydaress vòa 1 cái map
            if (!dic.ContainsKey(a.plankType))
            {
                dic[a.plankType] = obj;
            }

        }
    }

    //2 kiểu truy cập ,mở hết tất cả các dahnh sách prefab
    private async Task UploadPrefabPlank2()
    {
        //duyêt dictionary
        var map = GameConfigManager.Instance.plankTypeLogic.dictionary;
        foreach (var entry in map)
        {
            if (dic.ContainsKey(entry.Key)) continue;
            //tim key addressable            
            var keyAddress = map[entry.Key];
            var handle = Addressables.LoadAssetAsync<GameObject>(keyAddress);
            listHandle.Add(handle);
            var obj = await handle.Task;
            dic[entry.Key] = obj;
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

            // var x = holeData.scaleX;
            // var y = holeData.scaleY;
            hole.transform.localScale = new UnityEngine.Vector3(1, 1, 0);

            hole.holeId = holeData.holeId;
            hole.SetAsBackgroundHole();

            spawnedBackground.backgroundHoles.Add(hole);

            //duea vao map
            map[hole.holeId] = hole;

        }
    }
    private void SpawnPlanks(List<PlankData> plankDatas)
    {
        //danh sach plankdata ko có gì return
        if (plankDatas == null) return;

        foreach (var plankData in plankDatas)
        {

            if (string.IsNullOrEmpty(plankData.plankId)) continue;

            //tim kiem prefab dựa vào planktype 
            var plankPrefab = dic[plankData.plankType];

            //tim kiem plank trong pooler hoac span ra nếu chưa có plank
            var plankObj = ObjectPooler.Instance.Spawn(plankPrefab, plankData.position, UnityEngine.Quaternion.Euler(0, 0, plankData.rotation));

            //tìm kiếm componenet plank
            Plank plank = plankObj.GetComponent<Plank>();

            //setup cơ bản 
            plank.SetupPlank(plankData);

            //thêm nó vòa danh sách quản lí các plank
            spawnedPlanks.Add(plank);

            // Spawn holes trong plank
            if (plankData.listPlankHole == null) continue;

            //duyệt danh sách các hole, cho các hole là con của plank
            foreach (var holeData in plankData.listPlankHole)
            {
                if (string.IsNullOrEmpty(holeData.holeId)) continue;


                // lay hole ra tu pool
                var holeObj = ObjectPooler.Instance.Spawn(holePrefab, UnityEngine.Vector3.zero, holePrefab.transform.rotation);

                // GameObject holeObj = Instantiate(holePrefab, spawnedBackground.transform);
                holeObj.transform.SetParent(plankObj.transform);
                holeObj.transform.localPosition = holeData.positionLocal;

                //lấy compoenet hole
                Hole hole = holeObj.GetComponent<Hole>();
                if (hole == null) hole = holeObj.AddComponent<Hole>();

                //xet id do hole nay
                hole.holeId = holeData.holeId;

                //them vao map
                map[hole.holeId] = hole;

                var x = 1 / plank.transform.localScale.x;
                var y = 1 / plank.transform.localScale.y;
                hole.transform.localScale = new UnityEngine.Vector3(x, y, 0);
                hole.transform.localRotation = UnityEngine.Quaternion.Euler(0, 0, 0);
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
            //cho thang bolt no ko phai oiispuck nua
            //   bolt.isPickedUp = false;

            spawnedBolts.Add(bolt);
        }
    }
    public void ClearLevel()
    {
        //đưa nó về pool

        foreach (var plank in spawnedPlanks)
        {

            var prefab = dic[plank.plankType];
            ObjectPooler.Instance.Despawn(prefab, plank.gameObject);
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
