using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarIcon : MonoBehaviour
{
    public string idAvatar;
    public Image imageBackground;
    public Button button;
    public Sprite dautichxanh;
    public Image imageComponent;

    // Start is called before the first frame update


    public void SetUpAvatar(string id, Sprite sprite)
    {
        idAvatar = id;
        imageBackground.sprite = sprite;
        //neu id hien tai trung voi currentAvarID thi se hien thi tich con khong thi khong hien th
        if (idAvatar == SaveManager.Data.currentIdAvatar)
        {
            imageComponent.gameObject.SetActive(true);
            imageComponent.sprite = dautichxanh;
        }
        else
        {
            imageComponent.gameObject.SetActive(false);
        }

    }

    public void SetUpAvatar()
    {



        if (idAvatar == SaveManager.Data.currentIdAvatar)
        {
            imageComponent.gameObject.SetActive(true);
            imageComponent.sprite = dautichxanh;
        }
        else
        {
            imageComponent.gameObject.SetActive(false);
        }

    }
    public void SetupButotn()
    {
        button.onClick.AddListener(() =>
        {
            //cho avatar nay lam avatar
            imageComponent.gameObject.SetActive(true);
            imageComponent.sprite = dautichxanh;
            //chanrg vao database
            GameConfigManager.Instance.playerDataLogic.ChangeAvatar(SaveManager.Data, idAvatar);
            //sau do rtefresh lai tat ca danh sach
            UIManager.Instance.homeManager.avatarPopup.RefreshAvatar();
            //thay doi o vatarhome luon
            var gameobjectAvatarHome = UIManager.Instance.homeManager.buttonAvatar.gameObject;
            //lay component image
            var imageCompoent = gameobjectAvatarHome.GetComponent<Image>();
            imageCompoent.sprite = GameConfigManager.Instance.playerDataLogic.GetSpriteFormIdAvatar(idAvatar);
        });
    }


    public void Awake()
    {
        SetupButotn();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
