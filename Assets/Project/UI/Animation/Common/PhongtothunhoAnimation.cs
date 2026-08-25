using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhongtothunhoAnimation
{

    public static void PlayEffectSmallToBig(Transform button, int delay, float scale)
    {

        //  Debug.Log("no co chay vao day ko ");
        button.transform.DOScale(scale, 0.5f).SetEase(Ease.InQuad).SetLoops(-1, LoopType.Yoyo)
            .SetLink(button.gameObject, LinkBehaviour.KillOnDisable);
    }

    public static void KillAnimation(Transform transform)
    {
        if (transform != null)
        {
            transform.DOKill(false);
            transform.localScale = Vector3.one;
        }
    }
}
