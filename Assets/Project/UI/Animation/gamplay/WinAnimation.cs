using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;

public class WinAnimation : MonoBehaviour
{
    public GameObject plank1Prefab;
    public GameObject plank2Prefab;
    public GameObject textPrefab;
    public GameObject starPrefab;
    public GameObject p1;
    public GameObject p2;
    public GameObject text;
    public GameObject star;
    public RectTransform pannelStar;
    public List<StarICon> listStar = new List<StarICon>();




    public List<GameObject> listGameObject = new List<GameObject>();

    void Start()
    {

    }
    private void OnEnable()
    {
        EventManager.OnLevelComplete += HandleAnimationLevelComplete;
    }
    private void OnDisable()
    {
        EventManager.OnLevelComplete -= HandleAnimationLevelComplete;
        ClearAnimation();
    }



    private void HandleAnimationLevelComplete(int oldStar, int newStar)
    {
        //khởi tạo plank 1
        p1 = ObjectPooler.Instance.Spawn(plank1Prefab, new Vector2(0, 14), plank1Prefab.transform.rotation);

        // khởi tạo plank 2
        p2 = ObjectPooler.Instance.Spawn(plank2Prefab, new Vector2(0, 14), plank2Prefab.transform.rotation);

        listGameObject.Add(p1);
        listGameObject.Add(p2);


        //chuoi aniamtion;
        for (int i = 0; i < listGameObject.Count; ++i)
        {
            var plank = listGameObject[i];
            Sequence sequence = DOTween.Sequence()
                .SetLink(plank.gameObject, LinkBehaviour.KillOnDisable);
            sequence.AppendInterval(i * 0.6f);
            sequence.Append(plank.transform.DOMoveY(0f, 2f).SetEase(Ease.OutQuad));
            // sequence.AppendInterval(0.5f);

        }
        // sinh ra textPrefab;
        text = ObjectPooler.Instance.Spawn(textPrefab, new Vector2(0, 0), Quaternion.identity);

        listGameObject.Add(text);
        text.transform.DOScale(0.8f, 1f).SetLink(text.gameObject, LinkBehaviour.KillOnDisable);

        //ngoi sao
        //if newStar > oldStar thi khoi tao ngoi sao
        if (oldStar < newStar)
        {

            for (int i = 0; i < newStar; ++i)
            {
                var starObj = ObjectPooler.Instance.Spawn(starPrefab, new Vector2(0, 14), starPrefab.transform.rotation);
                var starIconComponent = starObj.GetComponent<StarICon>();
                if (starIconComponent != null)
                {
                    starIconComponent.SetYellowStar();
                }
                listGameObject.Add(starObj);
                listStar.Add(starIconComponent);
            }

            //animation
            for (int i = 0; i < listStar.Count; ++i)
            {
                var starx = listStar[i];
                starx.transform.DOMove(pannelStar.position, 1.5f)
                    .OnComplete(() =>
                    {
                        //lam con cua pannelStar
                        starx.transform.SetParent(pannelStar, false);

                        //cho ve chuong ga
                        //   ObjectPooler.Instance.Despawn(starPrefab, starx.gameObject);
                    });

            }
        }



    }


    void ClearAnimation()
    {
        //dua cac object vao pool
        if (p1 != null)
            ObjectPooler.Instance.Despawn(plank1Prefab, p1);
        if (p2 != null)
            ObjectPooler.Instance.Despawn(plank2Prefab, p2);
        if (text != null)
            ObjectPooler.Instance.Despawn(textPrefab, text);

        for (int i = 0; i < listStar.Count; ++i)
        {

            ObjectPooler.Instance.Despawn(starPrefab, listStar[i].gameObject);


        }
        listGameObject.Clear();
        listStar.Clear();
    }


    void Update()
    {

    }
}
