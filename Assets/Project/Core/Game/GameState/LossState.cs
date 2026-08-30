using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossState : IGameState
{
    public void EnterSate()
    {
        //hient thi casi lossPOPup la xong
        UIManager.Instance.gameplayPannel.lossPopup.gameObject.SetActive(true);
        //
    }

    public void Existstate()
    {
        //hient thi casi lossPOPup la xong
        UIManager.Instance.gameplayPannel.lossPopup.gameObject.SetActive(false);
    }
}