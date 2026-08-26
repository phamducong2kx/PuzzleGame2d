using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelConWorld : MonoBehaviour
{
    [SerializeField] private int idWorld;
    // Start is called before the first frame update
    public Sprite lockWorld;
    public Button button;
    public Image imageBG;
    private void Awake()
    {
        SetupButton();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        PhongtothunhoAnimation.KillAnimation(transform);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupIcon()
    {
        //pha dotwtwen sau do set up lai phai duoi
        PhongtothunhoAnimation.KillAnimation(transform);
        //world cao nhat
        var worldUnlockHighest = SaveManager.Data.worldUnlockHighest;

        //neu world do chua mo khoa
        if (idWorld <= worldUnlockHighest)
        {
            button.interactable = true;
            //hoat a?nh
            PhongtothunhoAnimation.PlayEffectSmallToBig(transform, -1, 1.2f);

        }
        else
        {
            button.interactable = false;
            imageBG.sprite = lockWorld;
        }
    }

    private void SetupButton()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            //chuyen sang state levelselect , khoa man hinh maplevel lai
            UIManager.Instance.levelMapManager.HideSelf();

            //tim dc world -> set up curent world
            SaveManager.SetCurrenWorld(idWorld);

            //go to levelSelect
            GameStateManager.Instance.ChangeSate(GameStateCache.levelSelecState);


        });
    }


}
