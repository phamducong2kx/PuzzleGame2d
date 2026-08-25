using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class CointBurst : MonoBehaviour
{

    public GameObject cointPrefab;
    public List<Transform> listCoint;
    public Transform pannel;



    private void Awake()
    {

    }


    private void OnEnable()
    {
        EventManager.OnPlankFallComplete += HandlePlankFall;
        EventManager.OnRefreshLevel += HandleRefresh;

    }



    private void OnDisable()
    {
        EventManager.OnPlankFallComplete -= HandlePlankFall;
        EventManager.OnRefreshLevel -= HandleRefresh;
        ClearAnimation();
    }

    //lay ra cac coint tu pool
    private List<Transform> SpawnCoint(Transform plank)
    {
        List<Transform> list = new List<Transform>();
        for (int i = 0; i < 3; ++i)
        {

            //lay ra (chua co thi sinh ra ) trong pooler , luc nay no se lam con cua game object ten "
            var coint = ObjectPooler.Instance.Spawn(cointPrefab, plank.position, cointPrefab.transform.rotation);
            //Debug.Log("scale cua coint la x = " + coint.transform.localScale.x);
            listCoint.Add(coint.transform);
            list.Add(coint.transform);
        }
        return list;
    }

    private void HandleRefresh()
    {
        HandleGotoHome();
    }

    public void HandleGotoHome()
    {
        if (listCoint.Count == 0) return;
        foreach (var x in listCoint)
        {
            //se tat aniamtion cua no ngay lap tuc va dua no vao ppol ngay 
            x.DOKill(false);
            ObjectPooler.Instance.Despawn(cointPrefab, x.gameObject);
        }
        //sau do clear danh sach coint
        listCoint.Clear();
    }


    //animation
    public void PlayAnimationPlankFell(List<Transform> coint, Transform pannelList, Transform uiTarget, Action plusCoint)
    {

        for (int i = 0; i < coint.Count; i++)
        {
            Transform cointx = coint[i];

            //khoi tao 1 sequence va set vong doi cho no
            var sequence = DOTween.Sequence().SetLink(cointx.gameObject, LinkBehaviour.KillOnDisable);

            //dung lai 0.5s
            sequence.AppendInterval(0.2f * i);

            //coint di toi trans
            sequence.Append(cointx.DOMove(pannelList.position, 0.8f).SetEase(Ease.OutQuad));

            //cho may dong xu lam con cua pannellist nay
            sequence.AppendCallback(() =>
            {
                cointx.SetParent(pannelList, false);
            });

            //dung lai 0.2s
            sequence.AppendInterval(0.2f);



            //nhun nhay 2 lan
            sequence.Append(cointx.DOLocalMoveY(50f, 0.6f)
                .SetLoops(3, LoopType.Yoyo)
                .SetEase(Ease.InOutSine));

            //dung lai 0.1s
            sequence.AppendInterval(0.3f);

            //tien toi vi tri uiTarget
            sequence.Append(cointx.DOMove(uiTarget.position, 0.6f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    Debug.Log("gia tri cua uitartget.position x va y lan luot  là " + uiTarget.position.x + " va " + uiTarget.position.y);
                    plusCoint?.Invoke();
                    //cho gameObject nay deactive ,= despawn object nay , cat vao trong pool
                    ObjectPooler.Instance.Despawn(cointPrefab, cointx.gameObject);

                    //xoa ngay phan tu do khoi mang (xoa thma chieu ) 
                    listCoint.RemoveAt(0);


                }));

        }

    }

    //goi khi plank roi den vach
    private void HandlePlankFall(Plank plank)
    {


        var list = SpawnCoint(plank.transform);

        PlayAnimationPlankFell(list, pannel, UIManager.Instance.gameplayPannel.coinview.transform, () =>
                {
                    UIManager.Instance.gameplayPannel.coinview.UpdateTextCoin();
                });

    }

    private void ClearAnimation()
    {

        //clear cac phan tu trong listCoint
        listCoint.Clear();


    }
}
