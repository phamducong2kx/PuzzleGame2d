using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


public class CointBurst : MonoBehaviour
{

    public GameObject cointPrefab;
    public List<Transform> listCoint;




    private void Awake()
    {

    }


    private void OnEnable()
    {
        //EventManager.OnPlankFallComplete += HandlePlankFall;
        EventManager.OnRefreshLevel += HandleRefresh;
        EventManager.OnGetItem += HandleGetItem;

    }

    private void HandleGetItem()
    {

    }

    private void OnDisable()
    {
        // EventManager.OnPlankFallComplete -= HandlePlankFall;
        EventManager.OnRefreshLevel -= HandleRefresh;
        ClearAnimation();
    }

    //lay ra cac coint tu pool
    //private List<Transform> SpawnCoint(Transform plank)
    //{
    //    List<Transform> list = new List<Transform>();
    //    for (int i = 0; i < 3; ++i)
    //    {

    //        //lay ra (chua co thi sinh ra ) trong pooler , luc nay no se lam con cua game object ten "
    //        var coint = ObjectPooler.Instance.Spawn(cointPrefab, plank.position, cointPrefab.transform.rotation);
    //        //Debug.Log("scale cua coint la x = " + coint.transform.localScale.x);
    //        listCoint.Add(coint.transform);
    //        list.Add(coint.transform);
    //    }
    //    return list;
    //}

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



    ////animation
    //public void PlayAnimationPlankFell(List<Transform> coint, Transform uiTarget, Action eventBurst)
    //{

    //    for (int i = 0; i < coint.Count; i++)
    //    {
    //        Transform cointx = coint[i];

    //        //khoi tao 1 sequence va set vong doi cho no
    //        var sequence = DOTween.Sequence().SetLink(cointx.gameObject, LinkBehaviour.KillOnDisable);

    //        //dung lai 0.5s
    //        sequence.AppendInterval(0.2f * i);

    //        //coint di toi trans
    //        sequence.Append(cointx.DOMove(pannelList.position, 0.8f).SetEase(Ease.OutQuad));

    //        //cho may dong xu lam con cua pannellist nay
    //        sequence.AppendCallback(() =>
    //        {
    //            cointx.SetParent(pannelList, false);
    //        });

    //        //dung lai 0.2s
    //        sequence.AppendInterval(0.2f);



    //        //nhun nhay 2 lan
    //        sequence.Append(cointx.DOLocalMoveY(50f, 0.6f)
    //            .SetLoops(3, LoopType.Yoyo)
    //            .SetEase(Ease.InOutSine));

    //        //dung lai 0.1s
    //        sequence.AppendInterval(0.3f);

    //        //tien toi vi tri uiTarget
    //        sequence.Append(cointx.DOMove(uiTarget.position, 0.6f)
    //            .SetEase(Ease.InBack)
    //            .OnComplete(() =>
    //            {
    //                //  Debug.Log("gia tri cua uitartget.position x va y lan luot  là " + uiTarget.position.x + " va " + uiTarget.position.y);
    //                eventBurst?.Invoke();
    //                //cho gameObject nay deactive ,= despawn object nay , cat vao trong pool
    //                ObjectPooler.Instance.Despawn(cointPrefab, cointx.gameObject);

    //                //xoa ngay phan tu do khoi mang (xoa thma chieu ) 
    //                listCoint.Remove(cointx);


    //            }));

    //    }

    //}


    public void PlayAnimationPlankFell(UnityEngine.Vector2 startPosition, int pointCoint, float distance, Transform target, Action eventBurst)
    {
        Camera camera = Camera.main;
        var a = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var b = camera.ViewportToWorldPoint(new Vector3(1, 0, 0));

        //lay toa do x cua starPositon va danh sach cac diem den cua coint
        float x = startPosition.x;
        var listPosition = new List<Vector3>();


        //dueyt danh sacsh , số lần lặp là số điểm coint của plank đó
        for (int i = 0; i < pointCoint; ++i)
        {
            Vector3 pos = new Vector3();
            if (x < a.x) pos = new Vector3(a.x + i * distance, a.y + 2, 0);
            else if (x > b.x) pos = new Vector3(a.x + i * distance, a.y + 2, 0);
            else pos = new Vector3(x + i * distance, a.y + 2, 0);
            listPosition.Add(pos);
        }

        for (int i = 0; i < pointCoint; i++)
        {
            int index = i;
            var pos = listPosition[i];
            //voi moi gia tri khoi tao 1 coint
            var coint = ObjectPooler.Instance.Spawn(cointPrefab, startPosition, cointPrefab.transform.rotation);

            //thme coint voa danh sách
            listCoint.Add(coint.transform);

            //khoi tao 1 sequence va set vong doi cho no
            var sequence = DOTween.Sequence().SetLink(coint, LinkBehaviour.KillOnDisable);

            //dung lai 0.5s
            sequence.AppendInterval(0.2f * i);

            //coint di toi trans
            sequence.Append(coint.transform.DOMove(listPosition[i], 0.3f).SetEase(Ease.OutQuad));

            sequence.AppendCallback(() =>
            {
                if (index == 0)
                {
                    ParticleManager.Instance.HandlePlankFall(new Vector2(pos.x, -13f));
                    Debug.Log("da chay ong nay chua");
                }
            });

            //dung lai 0.2s
            sequence.AppendInterval(0.2f);

            //nhun nhay 2 lan
            sequence.Append(coint.transform.DOLocalMoveY(10f, 0.6f)
                .SetRelative(true)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InOutSine));

            //dung lai 0.1s
            sequence.AppendInterval(0.3f);

            //tien toi vi tri uiTarget
            sequence.Append(coint.transform.DOMove(target.position, 0.6f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    //  Debug.Log("gia tri cua uitartget.position x va y lan luot  là " + uiTarget.position.x + " va " + uiTarget.position.y);
                    eventBurst?.Invoke();

                    //cho gameObject nay deactive ,= despawn object nay , cat vao trong pool
                    ObjectPooler.Instance.Despawn(cointPrefab, coint);

                    //xoa ngay phan tu do khoi mang (xoa thma chieu ) 
                    listCoint.Remove(coint.transform);


                }));

        }

    }



    private void ClearAnimation()
    {

        //clear cac phan tu trong listCoint
        listCoint.Clear();


    }
}
