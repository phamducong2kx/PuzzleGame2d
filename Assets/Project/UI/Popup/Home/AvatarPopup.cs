using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarPopup : MonoBehaviour
{
    public RectTransform pannelAvatar;
    public AvatarIcon avartarIcon;
    public List<AvatarIcon> listAvatarIcon = new List<AvatarIcon>();
    public Button existButton;


    // Start is called before the first frame update
    private void Awake()
    {

        GenerateListAvatar();
        SetupButotnExxist();
    }

    public void GenerateListAvatar()
    {
        //list avar
        var list = GameConfigManager.Instance.playerDataLogic.avatarDatabase.listAvatarInfo;
        for (int i = 0; i < list.Count; i++)
        {
            var idAvatar = list[i].id;

            //tao ra avatar va cho lam con cua pannel
            var avatarObj = Instantiate(avartarIcon, pannelAvatar);
            //set up cho avatar
            avatarObj.SetUpAvatar(idAvatar, list[i].sprite);
            //them vao list
            listAvatarIcon.Add(avatarObj);
        }
    }

    public void RefreshAvatar()
    {
        var currentIDAvatar = GameConfigManager.Instance.playerDataLogic.GetIdCurrentAvatar(SaveManager.Data);
        for (int i = 0; i < listAvatarIcon.Count; i++)
        {
            listAvatarIcon[i].SetUpAvatar();
        }
    }

    public void SetupButotnExxist()
    {
        existButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
}
