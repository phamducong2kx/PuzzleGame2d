using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Button existPauseButton;
    public Button refreshButton;
    public Button goHomeButton;
    // Start is called before the first frame update
    void Start()
    {
        SetUpExistButton();
        SetUpRefreshLevel();
        SetUpGoHome();
    }


    void Update()
    {

    }


    public void ActiveSelf()
    {
        gameObject.SetActive(true);
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }

    public void SetUpExistButton()
    {
        existPauseButton.onClick.AddListener(() =>
        {
            //set
            GameManager.Instance.isResume = true;

            //ddonsg giao diene
            UIManager.Instance.gameplayPannel.HidePauseUI();

            //chuyeefn sang playing
            GameStateManager.Instance.ChangeSate(GameStateCache.playingState);


        });
    }

    public void SetUpRefreshLevel()
    {
        refreshButton.onClick.AddListener(() =>
        {
            //set isrefresh = true
            GameManager.Instance.isRefresh = true;

            //ddonsg giao diene
            UIManager.Instance.gameplayPannel.HidePauseUI();

            //chuyển skillstate sang default state;
            InputHandler.Instance.SetStrategy(new DefaultState());

            //chuyeefn sang playing
            GameStateManager.Instance.ChangeSate(GameStateCache.playingState);


        });
    }

    public void SetUpGoHome()
    {
        goHomeButton.onClick.AddListener(() =>
        {
            //clear level data
            LevelLoader.Instance.ClearLevel();

            //đóng cửa sổ pause
            gameObject.SetActive(false);

            //despawn danh sacj cac ngooi sao 
            UIManager.Instance.gameplayPannel.starView.Despawn();
            ParticleManager.Instance.HandleGotoHome();
            AnimationManager.Instance.gamePlayAnimation.cointBurst.HandleGotoHome();

            //phai cho inputhanlder pickupedBolt = null, neu no dang tham chieu toi 1 doi tuong 
            InputHandler.Instance.pickedBolt = null;

            //chuyển skillstate sang default state;
            InputHandler.Instance.SetStrategy(new DefaultState());


            //chuyen sang gameState moi
            GameStateManager.Instance.ChangeSate(GameStateCache.levelSelecState);
        });
    }





}
