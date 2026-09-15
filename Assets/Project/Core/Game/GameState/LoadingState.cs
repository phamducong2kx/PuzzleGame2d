using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingState : IGameState
{
    public async void EnterSate()
    {

        //tỉa dữ liều về máy.
        await UIManager.Instance.loadingManager.SetupLoading();

        //tỉa xong chuyển hướng sang home
        GameStateManager.Instance.ChangeSate(GameStateCache.homeState);
    }

    public void Existstate()
    {
        UIManager.Instance.loadingManager.gameObject.SetActive(false);
        UIManager.Instance.topBarZone.gameObject.SetActive(true);
        UIManager.Instance.homeManager.gameObject.SetActive(true);
    }
}
