
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Timeline;
using UnityEngine.UI;
using static UnityEngine.Rendering.ReloadAttribute;





public class DailyRewardPopup : MonoBehaviour
{
    public RectTransform containerReward;
    [SerializeField] private DailyRewardConfig dailyRewardConfig;
    [SerializeField] private Transform listItemPanel;
    [SerializeField] private PannelDayItem panelDayItem;
    [SerializeField] private IconItemReward itemDailyReward;
    [SerializeField] private Button existButton;
    [SerializeField] private Button getRewardButton;
    [SerializeField] private TextMeshProUGUI countDownTime;
    // [SerializeField] private List<PannelDayItem> dayList;
    private bool isWatingTime = true;
    public bool isFirstOpen = false;





    private void Awake()
    {
        // Genertate();
        // SetUpExistButton();

    }

    public async void OnEnable()
    {
        if (isFirstOpen == false)
        {
            await Genertate();
            SetUpExistButton();
            SetUpGetRewardButton();
            isFirstOpen = true;
        }
        else
        {
            //ccnaf có hàm refresh lại
            RefreshPage();

        }
    }

    //danh sách các pannel
    private async Task Genertate()
    {
        //load prefab item rewward lên ram
        var handlePrefab = Addressables.LoadAssetAsync<GameObject>(AddressableLabels.ICONITEMREWARD);

        //đọi xong thì gán cho iconitemreward
        var x = await handlePrefab.Task;
        itemDailyReward = x.GetComponent<IconItemReward>();
        //lay danh sách daily reward
        var listReward = dailyRewardConfig.dailyRewards;

        //tao danh sacsh pannelDayitem tu lisdtPannel
        for (int i = 0; i < listReward.Count; ++i)
        {
            // khoi tao 1 cai pannelDay de chua danh scah các quà của 1 ngày
            var pannelDay = Instantiate(panelDayItem, listItemPanel);

            //set up cho pannel do : animation , text ngày 
            pannelDay.SetUpPannel(i + 1);

            //thêm vào list
            //  dayList.Add(pannelDay);

            //danh sách quà của ngày hôm đó
            var itemList_for_day = listReward[i].listRewardItem;

            //khởi tạo nó thôi 
            var itemInfo = new ItemInfo();
            for (int j = 0; j < itemList_for_day.Count; ++j)
            {

                //khoi tao object item bang pooler
                //  var item = ObjectPooler.Instance.Spawn(itemDailyReward.gameObject, new Vector2(0, 0), itemDailyReward.transform.rotation);
                var item = Instantiate(itemDailyReward, pannelDay.pannelItem);
                //cho lam con của pannelDay
                // item.transform.SetParent(panelDayItem.transform, false);

                //voi id dau tien ,can tim item infor tuong ung
                itemInfo = GameConfigManager.Instance.itemLogic.GetItemInfoById(itemList_for_day[j].idItem);

                //set up item
                //  var component = item.GetComponent<IconItemReward>();
                item.Setup(itemInfo.icon, itemList_for_day[j].amount);
                //item.amount.text = itemList_for_day[j].amount.ToString();
                // item.imageItem.sprite = itemInfo.icon;

            }

            foreach (RectTransform child in listItemPanel)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(child);
            }

            // 2. Ép Content cha xếp lại vị trí các Panel con từ trên xuống dưới
            LayoutRebuilder.ForceRebuildLayoutImmediate(listItemPanel as RectTransform);

        }
    }

    //refreh lại danh sách thôi
    private void RefreshPage()
    {


        var listPannel = listItemPanel.GetComponentsInChildren<PannelDayItem>();


        //tao danh sacsh pannelDayitem tu lisdtPannel
        for (int i = 0; i < listPannel.Length; ++i)
        {

            //set up cho pannel do : animation , text ngày 
            listPannel[i].RefreshPannel(i + 1);

        }
    }




    void Start()
    {

    }


    //void Update()
    //{
    //    if (isWatingTime == false) return;
    //    TimeSpan remainingTime = DailyRewardTimeLogic.GetRemainingTime();

    //    if (remainingTime == TimeSpan.Zero)
    //    {
    //        isWatingTime = false;
    //        getRewardButton.gameObject.SetActive(true);
    //        countDownTime.transform.parent.gameObject.SetActive(false);
    //        SetUpGetRewardButton();

    //    }
    //    else
    //    {

    //        //đếm ngược time
    //        countDownTime.text = DailyRewardTimeLogic.ConvertTimeSpantoString(remainingTime);
    //    }




    //}

    private void FixedUpdate()
    {
        if (isWatingTime == false) return;
        //tinfh khoảng thời gian còn lại
        var remainingTime = DailyRewardTimeLogic.GetRemainingTime();
        if (remainingTime != TimeSpan.Zero)
        {
            //getRewardButton.gameObject.SetActive(false);
            //countDownTime.gameObject.SetActive(true);
            countDownTime.text = DailyRewardTimeLogic.ConvertTimeSpantoString(remainingTime);

        }
        else
        {
            //kik hoat nut button
            getRewardButton.gameObject.SetActive(true);
            countDownTime.transform.parent.gameObject.SetActive(false);
            isWatingTime = false;

        }
    }

    private void SetUpExistButton()
    {
        existButton.onClick.AddListener(() =>
        {
            //disable cái popup này
            gameObject.SetActive(false);

            //refresh laij cais notice aniamtuon
            UIManager.Instance.homeManager.RefreshNoticeDailyReward();

        });
    }

    private void SetUpGetRewardButton()
    {
        getRewardButton.onClick.RemoveAllListeners();
        Debug.Log("ngay hien tai là " + SaveManager.Data.lastDayGetDailyReward);
        getRewardButton.onClick.AddListener(() =>
        {
            //vô hiệu hóa nút exist
            existButton.interactable = false;

            //active object animation
            AnimationManager.Instance.dailyRewardAnimation.Active();

            //lay danh sách quà của ngày nhận thưởng
            int reward = SaveManager.Data.lastDayGetDailyReward + 1;

            //laays danh sacsh qua cua ngay nhan thưởng
            var listReward = dailyRewardConfig.dailyRewards[reward - 1].listRewardItem;

            //tạo 1 cái list chứa danh sách các ảnh
            var listRewardObject = new List<RectTransform>();

            //cong vao database luon,sau do moi phat animation
            foreach (var x in listReward)
            {
                //tu id tim ra items
                var item = GameConfigManager.Instance.itemLogic.GetItemInfoById(x.idItem);


                //nếu item ko phải coint thì cộng như này
                if (item.type == ItemType.Skill)
                {
                    GameConfigManager.Instance.playerDataLogic.BuyResource(item.type, item.id, x.amount);
                }
                else
                {
                    //  còn  item là coin thfi cộng như này
                    GameConfigManager.Instance.playerDataLogic.AddCoint(x.amount);
                }



                //tao 1 object tu prefab;
                var itemReward = ObjectPooler.Instance.Spawn(itemDailyReward.gameObject, new Vector3(0, 0, 0), itemDailyReward.transform.rotation);

                //set up cho no
                var componentItem = itemReward.GetComponent<IconItemReward>();
                componentItem.Setup(item.icon, x.amount);


                //Đưa vào danh sách để quản lí 

                listRewardObject.Add(itemReward.GetComponent<RectTransform>());

            }


            //vị trí đến
            var target = UIManager.Instance.topBarZone.cointText;

            //chay aniamtion
            AnimationManager.Instance.gamePlayAnimation.cointBurst.PlayAnimationBuyItem(listRewardObject, containerReward, target.rectTransform,
                () =>
                {
                    //thay doi tetx trong tarrget
                    target.text = (SaveManager.Data.coint).ToString();
                },
                () =>
                {


                    //vo hieu hoa nut ẽisty
                    existButton.interactable = true;
                    //
                    //danh sach = true
                    isWatingTime = true;

                    //set active = true;
                    // countDownTime.gameObject.SetActive(true);
                    getRewardButton.gameObject.SetActive(false);
                    countDownTime.transform.parent.gameObject.SetActive(true);
                    //set up time 
                    SaveManager.Save_Get_dailyReward_Successfull();

                    //sau cung refresh lai pannel
                    RefreshPage();

                }

            );



        });
    }
}















