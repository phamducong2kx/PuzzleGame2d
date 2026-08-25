using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum StarState
{
    Normal,
    Warning,
    Loss
}

public class StarICon : MonoBehaviour
{
    [SerializeField] private int idStar;
    [SerializeField] private Sprite star_yellow;
    [SerializeField] private Sprite star_white;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textTime;
    // Start is called before the first frame update
    private void Awake()
    {
        SetYellowStar();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetYellowStar()
    {
        image.sprite = star_yellow;
        textTime.text = "";
    }
    public void ResetIcon()
    {
        transform.localScale = Vector3.one;
        transform.DOKill();
        textTime.text = "";
    }
    public void SetWhiteStar()
    {
        image.sprite = star_white;
    }

    public void SetUpStarIcon(int index)
    {
        idStar = index;
        SetYellowStar();
    }

    public void HandleStar(int id, StarState starState)
    {
        if (id != idStar) return;
        //voi id nay tim xem trnog danh sacsh casc sao , tim object theo id


        //swaith case starState de chay animation thoe no
        switch (starState)
        {
            case StarState.Warning:
                HandleAnimationWarning(id);
                break;
            case StarState.Loss:
                HandleAnimationLoss();
                break;
        }

    }
    private void HandleAnimationWarning(int id)
    {


        //phong to thu nho 
        PhongtothunhoAnimation.PlayEffectSmallToBig(transform, -1, 1.2f);

        //hien thi  time tren textPrefab, can lay so giay
        float seconds = 0;
        if (id == 3) seconds = LevelLoader.Instance.CurrentLevelData.thresh_time_star3;
        else if (id == 2) seconds = LevelLoader.Instance.CurrentLevelData.thresh_time_star2;
        else if (id == 1) seconds = LevelLoader.Instance.CurrentLevelData.thresh_time_star1;

        //convert sang string time
        string time = UIManager.Instance.gameplayPannel.timeView.FormatString(seconds);
        textTime.text = time;



    }

    private void HandleAnimationLoss()
    {
        //xoa het dotkill
        PhongtothunhoAnimation.KillAnimation(transform);


        //xoa hien thi time tren textPrefab
        textTime.text = "";

        //chuyen mau cua ngoi sao
        SetWhiteStar();
    }


    private void OnDisable()
    {
        //xoa het dotkill
        transform.DOKill();

        //xoa hien thi time tren textPrefab
        textTime.text = "";
    }



}
