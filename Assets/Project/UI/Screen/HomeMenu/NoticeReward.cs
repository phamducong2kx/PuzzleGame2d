using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class NoticeReward : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite notice;
    public Sprite success;
    public Image image;
    public Transform origin;
    private Vector3 originalLocalPosition;
    private Vector3 originalLocalRotation;
    private Vector3 originalLocalScale;
    private void Awake()
    {
        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localEulerAngles;
        originalLocalScale = transform.localScale;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleNoticeGetReward()
    {
        //transform.
        image.sprite = notice;

        //goi animation
        AnimationManager.Instance.noticeAnimation.PlayNoticeAnimation(transform);
    }

    public void HandleDoneGetReward()
    {
        image.sprite = success;
        ClearAnimation();
    }

    public void ClearAnimation()
    {
        AnimationManager.Instance.noticeAnimation.KillAnimation();
        transform.DOKill(false);

        // 3. Trả về đúng các giá trị ban đầu đã lưu
        transform.localPosition = originalLocalPosition;
        transform.localEulerAngles = originalLocalRotation;
        transform.localScale = originalLocalScale;
    }

    private void OnDisable()
    {

        ClearAnimation();



    }
}
