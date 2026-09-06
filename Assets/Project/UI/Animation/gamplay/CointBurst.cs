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

        EventManager.OnRefreshLevel += HandleRefresh;


    }



    private void OnDisable()
    {

        EventManager.OnRefreshLevel -= HandleRefresh;
        ClearAnimation();
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
            //    ObjectPooler.Instance.Despawn(ObjectPooler.Instance.instanceObject[x.gameObject], x.gameObject);
            ObjectPooler.Instance.Despawn(cointPrefab, x.gameObject);
        }
        //sau do clear danh sach coint
        listCoint.Clear();
    }





    public void PlayAnimationPlankFell(UnityEngine.Vector2 startPosition, int pointCoint, float distance, Transform target, Action onItemReachTarget)
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
                    onItemReachTarget?.Invoke();

                    //cho gameObject nay deactive ,= despawn object nay , cat vao trong pool
                    ObjectPooler.Instance.Despawn(cointPrefab, coint);

                    //xoa ngay phan tu do khoi mang (xoa thma chieu ) 
                    listCoint.Remove(coint.transform);


                }));

        }

    }


    public void PlayAnimationBuyItem(List<RectTransform> listRectTranform, RectTransform pannelList, RectTransform target, Action eventBurst, Action onAllComplete)
    {

        int count = 0;
        for (int i = 0; i < listRectTranform.Count; i++)
        {
            var obj = listRectTranform[i];

            //khoi tao 1 sequence va set vong doi cho no
            var sequence = DOTween.Sequence().SetLink(obj.gameObject, LinkBehaviour.KillOnDisable);

            //dung lai 0.5s
            sequence.AppendInterval(0.5f * (i + 1));

            //cho làm con của pannel
            obj.SetParent(pannelList, false);



            sequence.Append(obj.DOMove(target.transform.position, 1f).SetEase(Ease.OutQuad));

            sequence.Append(obj.DOMoveY(10f, 0.01f).SetRelative(true).SetEase(Ease.OutQuad));

            sequence.OnComplete(() =>
              {
                  //xu li event
                  eventBurst?.Invoke();
                  ++count;

                  if (count == listRectTranform.Count)
                  {
                      onAllComplete?.Invoke();

                      //dua tat ca danh sách trong l;ít vao pool
                      foreach (var x in listRectTranform)
                      {
                          ObjectPooler.Instance.Despawn(ObjectPooler.Instance.instanceObject[x.gameObject], x.gameObject);
                      }


                      //xoa di tham chieu trong listRectRanform
                      listRectTranform.Clear();
                  }

              });





        }

    }


    private void ClearAnimation()
    {

        //clear cac phan tu trong listCoint
        listCoint.Clear();


    }
}
