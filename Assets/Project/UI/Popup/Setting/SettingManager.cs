using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{

    public Button exist;
    public Button changeSound;
    public Sprite muteSound;
    public Sprite normalSound;
    public Image imageBackground;
    private void Awake()
    {
        // SetMute();
        SetupButtonExist();
        SetupButtonChangeSound();
    }

    public void SetMute()
    {
        //nếu nod dang khong mute
        if (AudioListener.volume == 0f)
        {
            // doi ảnh
            imageBackground.sprite = normalSound;
            //bat am thanh
            AudioListener.volume = 1f;

        }
        else
        {
            //doi ảnh
            imageBackground.sprite = muteSound;
            //tat am thanh
            // AudioManagement.Instance.PlaySound(AddressableLabels.WIN, 1f, 1f);
            AudioListener.volume = 0f;
        }
        // muteSound
    }

    //kich hoaat
    public void OnActive()
    {
        gameObject.SetActive(true);
    }
    public void SetupButtonChangeSound()
    {
        changeSound.onClick.AddListener(() =>
        {
            SetMute();


        });
    }

    public void SetupButtonExist()
    {
        exist.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

    }

    void Start()
    {

    }


    void Update()
    {

    }
}
