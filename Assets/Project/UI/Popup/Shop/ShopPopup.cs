using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;


public class ShopPopup : MonoBehaviour
{
    public static ShopPopup Instance;
    public Button exist;
    public TextMeshProUGUI cointText;

    public List<TypePackageShoppe> listPannel;
    public RectTransform viewport;

    public RectTransform prefabPannel;
    public SnapBanner prefabBannerPannel;
    public RectTransform ContentPannel;
    public PackageShopInfor packageShopInfoPrefab;
    public GameObject positionCungCapXuPackage;
    public PopupConfirm popupConfirm;
    private AsyncOperationHandle<IList<Sprite>> preloadHandle;
    public IList<Sprite> listSprite = new List<Sprite>();
    public bool isKhoitao = false;
    public Dictionary<string, Sprite> dictionary = new Dictionary<string, Sprite>();
    public List<PackageShopInfor> listPackageInfo;
    private bool isLoadImage = false;
    private void Awake()
    {
        //khoi tao instance
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else Instance = this;

        //lưu ảnh trong đây


    }

    private async void OnEnable()
    {
        if (isLoadImage) return;
        //loading tát cả ảnh vào đây 
        isLoadImage = true;
        //load a?nh tu thu muc ung dung hoacj cache, khong co thi load tren server
        preloadHandle = Addressables.LoadAssetsAsync<Sprite>(AddressableLabels.PRELOAD, null);
        listSprite = await preloadHandle.Task;
        if (preloadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("tải ảnh lên ram thanh cong");
            //đua anh vao dictionary
            foreach (var x in listSprite)
            {
                dictionary[x.name] = x;
                Debug.Log("x.name co gia tri la " + x.name);
            }

        }

        if (isKhoitao == false)
        {
            SetupButtonExist();
            GenerateListItem();
            isKhoitao = true;
        }


    }
    private void OnDisable()
    {
       
    }




    void Start()
    {

    }


    private void SetupButtonExist()
    {
        exist.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.topBarZone.gameObject.SetActive(true);
        });

    }
    public void SetupCoint()
    {
        //so tien hien tai la

        var coint = SaveManager.Data.coint;
        // Debug.Log("coint hien tai la " + coint);
        cointText.text = GameConfigManager.Instance.playerDataLogic.GetCoint(SaveManager.Data).ToString();
    }

    private void GenerateListItem()
    {

        GenPackageBanner();
        GenPackageAlone();

    }

    private void GenPackageBanner()
    {
        //tao 1 cai pannel de chua object banner
        var snapBanner = Instantiate(prefabBannerPannel, ContentPannel);

        //tao object banner trong banner to va set up ca thong so
        snapBanner.SetupFirst();

    }

    // tao cac package don le
    private void GenPackageAlone()
    {
        // can duet danh sacsh cacs the laoi pacckage de tao danh sacsh gom voa pannle
        //cần tạo danh sách chứa các packahe có type là coint và vật pohamar tăng cươg
        GameObject gameObjectPannel = null;
        foreach (var type in listPannel)
        {

            //lay danh sach cac package co tupe la x
            var listPackage = GameConfigManager.Instance.itemLogic.GetPackageByType(type);
            if (listPackage.Count == 0) return;



            //đến Cuối của vòng for , tham chiếu đến vị trí của type cungcapxu
            if (type == TypePackageShoppe.CungCapXu)
            {
                positionCungCapXuPackage = Instantiate(prefabPannel.gameObject, ContentPannel);
                gameObjectPannel = positionCungCapXuPackage;
            }
            else
            {
                //tao pannel de chua cac package co type la x
                gameObjectPannel = Instantiate(prefabPannel.gameObject, ContentPannel);
            }

            //set up tẽtmespro
            var textMesh = gameObjectPannel.GetComponentInChildren<TextMeshProUGUI>();
            //sau khi tim dc thi viet chu cho no

            textMesh.text = listPackage[0].namePackageType;

            //tim kiem contentPanel trong gameobjectpannel
            var contentMain = gameObjectPannel.GetComponentInChildren<GridLayoutGroup>();

            //dueyt danh scah listPackage nay
            foreach (var x in listPackage)
            {
                //tao 1 object tu packageShopInfoPrefab
                var itemPackage = Instantiate(packageShopInfoPrefab, contentMain.transform);

                //thme vao lisst
                listPackageInfo.Add(itemPackage);

                //tim image
                var sprite = dictionary[x.iconAdressKey];


                itemPackage.Setup(x, () =>
                 {
                     HandleButtonBuyItem(x.idPackage);

                 });

            }

            foreach (RectTransform child in ContentPannel)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(child);
            }

            // 2. Ép Content cha xếp lại vị trí các Panel con từ trên xuống dưới
            LayoutRebuilder.ForceRebuildLayoutImmediate(ContentPannel as RectTransform);


        }
    }

    public void HandleButtonBuyItem(string idPackage)
    {
        //nếu gia cua packahe nay tinh theo dolar, thì return
        var package = GameConfigManager.Instance.itemLogic.GetPackageByID(idPackage);

        //neu giao dịch tiền mặt xử lí sau
        if (package.priceDolar != 0) return;

        else
        {
            //tinmh gia coinm hien tai dang co
            var currentCoint = GameConfigManager.Instance.playerDataLogic.GetCoint(SaveManager.Data);

            //so sansh xem coint hien tai co lon hon gia cua goi packahe hay khong
            var pricePackage = package.priceCoint;

            if (currentCoint >= pricePackage)
            {
                popupConfirm.OpenPopup(package);
            }
            else
            {
                ScrollVerticle();
            }
        }
    }

    private void ScrollVerticle()
    {
        Debug.Log("Khong du tien ,chay vao day di ");
        //lay vi tri hien tai thoew world cua pannel chua package nay
        var positonPannel = positionCungCapXuPackage.transform.position;

        //tạovector the hien toa do cua dioem nay so voi viewPort content 
        Vector2 distance = viewport.InverseTransformPoint(positonPannel);

        //di chuyen thoe phuonh thang udng nen chi can lay truc y, tim toa do moi
        Vector2 targetPosition = new Vector2(ContentPannel.anchoredPosition.x, ContentPannel.anchoredPosition.y - distance.y);

        //di chuyen contetn den vij tri tareget , dung dottwin
        ContentPannel.DOAnchorPos(targetPosition, 1f);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
