using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinState : IGameState
{
    private Coroutine winCoroutine;
    private bool unlockLevel;
    private int newStar = 0;
    public void EnterSate()
    {

        winCoroutine = GameStateManager.Instance.StartCoroutine(WinRoutine());

    }

    private IEnumerator WinRoutine()
    {



        //clear heest object
        LevelLoader.Instance.ClearLevel();

        // hien thi man pannel win
        UIManager.Instance.ActivePanelLevelComplate();

        //tăt logic time , tắt ui ko cần thiết
        GameManager.Instance.timerSystem.isRunning = false;
        GameManager.Instance.warningSystem.isPlaying = false;

        UIManager.Instance.gameplayPannel.WinUI();

        //đưa newStar trở lại pool
        UIManager.Instance.gameplayPannel.starView.Despawn();

        //xem so sao kime dc 
        //doi chiue voi so sao hien co cua level nay
        //neu so sao kiem dc > so sao hien co thi se co aniamtion so sao, se dua tham so int vao trong event
        //, sau do thi se luu so sao kiem dc cua level do trong ham animation


        //so sao hien tai kiem duoc;

        newStar = GameManager.Instance.timerSystem.mocsao;
        var data = SaveManager.Data;
        var oldStar = GameConfigManager.Instance.playerDataLogic.GetNumberStarOfLevel(data, data.currentLevel);
        var levelId = SaveManager.Data.currentLevel;


        //goi su kien win level
        EventManager.InvokeLevelComplete(oldStar, newStar);

        //sau do luu sao
        SaveManager.Save_Star(levelId, newStar);

        //doi trong 5s
        yield return new WaitForSeconds(5f);

        GameStateManager.Instance.ChangeSate(GameStateCache.levelSelecState);
    }
    public void Existstate()
    {
        //xóa courotine
        if (winCoroutine != null)
        {
            GameStateManager.Instance.StopCoroutine(winCoroutine);
        }

        //tro ve trang thai che do choi binh thường
        InputHandler.Instance.SetStrategy(new DefaultState());

        //disable cacs aniamtion
        AnimationManager.Instance.gamePlayAnimation.Hide();

        //aan man hinh level complete
        UIManager.Instance.HidePanelLevelComplate();

        //an mann hinh gamepley
        UIManager.Instance.gameplayPannel.HideSelf();

        //enable aniamtion
        AnimationManager.Instance.levelAnimationManager.Active();

        //mo khoa level moi
        unlockLevel = SaveManager.UnlockNewLevel(newStar);

        //neu mo khao level moi
        if (unlockLevel)
        {
            //phat su kien mo khoa level moi va cong tien vao tai khoan
            EventManager.InvokeUnlockLevel();
        }


        //refresh lai level
        UIManager.Instance.pannelLevelSelect.RefreshButtonIcon(SaveManager.Data.currentChapter);

    }
}