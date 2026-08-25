using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DailyRewardAnimation : MonoBehaviour
{
    public Transform cointView;
    public Transform pannel;
    public ItemDailyReward itemReward;
    public List<ItemDailyReward> listItem;
    private int coint;
    private bool isCoint = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        //dang ki
        EventManager.OnGetDailyReward += HandleAnimationgetDailyReward;
    }
    private void OnDisable()
    {
        // huy dang ki
        EventManager.OnGetDailyReward -= HandleAnimationgetDailyReward;
        ClearList();
    }

    public void HandleAnimationgetDailyReward(List<Item_DailyReward> list)
    {
        //duyet danh sách các item va tao ra game object tu item nay , lam con cua pannel;
        for (int i = 0; i < list.Count; ++i)
        {
            //tao 1 doi tuong
            var x = Instantiate(itemReward, pannel);

            //amount
            x.amount.text = list[i].amount.ToString();

            //sprite
            var item = GameConfigManager.Instance.itemLogic.GetItemInfoById(list[i].idItem);
            x.imageItem.sprite = item.icon;

            //add voa danh sacsh 
            listItem.Add(x);

            //check xem phan tu nay co type la coint hay ko 
            if (item.type == ItemType.Coint)
            {
                coint = list[i].amount;
                isCoint = true;
            }
        }

        //duyet danh sachs cac thamc hieu den compoenent
        for (int i = 0; i < listItem.Count; ++i)
        {

            //gan thma chieu cho x
            var x = listItem[i];


            //tạo 1 sequence
            var seq = DOTween.Sequence();

            seq.SetLink(x.gameObject);

            seq.AppendInterval(0.5f * i);

            //to nhỏ vìa vòng
            seq.Append(x.transform.DOScale(1.2f, 0.5f).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo));

            seq.AppendInterval(0.5f);

            seq.Append(x.transform.DOMove(cointView.transform.position, 0.7f).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    //cong coint cho view; 
                    if (isCoint)
                        UIManager.Instance.topBarZone.AddCoint_NotSave(coint);

                    //xóa object dc tạo
                    Destroy(x.gameObject, 2f);
                }));

        }


    }

    public void ClearList()
    {
        listItem.Clear();
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }
}

