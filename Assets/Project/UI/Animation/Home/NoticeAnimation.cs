using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[Serializable]
public class NoticeAnimation
{
    public float shakeAngle = 15f;    // Góc lắc (độ)
    public float shakeDuration = 0.5f; //thowif gian lawcs
    public int shakeVibrato = 5; //so lan lac qua lai

    private float dropDistance = 50f;   // Khoảng cách từ trên rớt xuống
    private float dropDuration = 0.3f;  // Thời gian rớt xuống
    private float delayTime = 0.4f;     // Thời gian chờ trước khi lặp lại



    private Sequence noticeSequence;

    public void PlayNoticeAnimation(UnityEngine.Transform transform)
    {
        var originalPosition = transform.localPosition;
        var originalRotation = transform.localEulerAngles;


        // Xóa sequence cũ nếu có để tránh lỗi tràn bộ nhớ


        noticeSequence?.Kill();
        noticeSequence = DOTween.Sequence();

        noticeSequence.Append(transform.DOPunchRotation(new Vector3(0, 0, shakeAngle), shakeDuration, shakeVibrato, 1));


        noticeSequence.Append(transform.DOScale(Vector3.zero, 0.1f));

        // 3. Chờ 0.4 giây
        noticeSequence.AppendInterval(delayTime);

        // 4. Đưa icon lên phía trên một đoạn và reset scale/rotation chuẩn bị xuất hiện lại
        noticeSequence.AppendCallback(() =>
        {
            transform.localPosition = originalPosition + new Vector3(0, dropDistance, 0);
            transform.localEulerAngles = originalRotation;
            transform.localScale = Vector3.one; // Hiện lại icon
        });

        // 5. Đi xuống vị trí ban đầu
        noticeSequence.Append(transform.DOLocalMoveY(originalPosition.y, dropDuration).SetEase(Ease.OutQuad));

        // 6. Cho toàn bộ chuỗi này lặp lại vô hạn
        noticeSequence.SetLoops(-1, LoopType.Restart);
    }

    public void KillAnimation()
    {
        if (noticeSequence != null && noticeSequence.IsActive())
        {
            noticeSequence.Kill();
        }
    }


}
