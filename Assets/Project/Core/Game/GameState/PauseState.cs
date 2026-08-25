using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseState : IGameState
{
    public void EnterSate()
    {

        //dung thoi gian trong gam update cua moi script lai ?
        Time.timeScale = 0f;

        // hiene thị ui 
        UIManager.Instance.gameplayPannel.ActivePauseUI();
    }

    public void Existstate()
    {
        Time.timeScale = 1f;

    }
}
