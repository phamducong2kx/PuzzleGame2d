using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkiillShopPopup : MonoBehaviour
{
    public Button shoppePannelButton;
    public static Skill currentSkillRefrence;
    public Image imageSkill;
    public TextMeshProUGUI textDescribe;
    public TextMeshProUGUI textPrice;
    public Button buySkill;
    public TextMeshProUGUI totalCoint;

    // Start is called before the first frame update
    private void Awake()
    {
        SetupShoppePannel();
        SetupBuyButton();
    }
    private void SetupShoppePannel()
    {
        shoppePannelButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            shoppePannelButton.gameObject.SetActive(false);
        });


    }
    private void SetupBuyButton()
    {

        buySkill.onClick.AddListener(() =>
        {
            var coint = GameConfigManager.Instance.playerDataLogic.GetCoint(SaveManager.Data);
            if (coint >= currentSkillRefrence.price)
            {
                shoppePannelButton.gameObject.SetActive(false);
                gameObject.SetActive(false);
                //coong so luong skill
                currentSkillRefrence.ChangeUpdateUsingSkill(1);
                //refresh lai skill;
                Time.timeScale = 1f;
            }

            //logic + amount
            //aniamtion?
        });

    }

    public void ResetCurrentSkillData()
    {
        imageSkill.sprite = GameConfigManager.Instance.itemLogic.GetItemInfoById(currentSkillRefrence.skillData.idItem).icon;
        textDescribe.text = currentSkillRefrence.skillData.desscribeSkill;
        textPrice.text = "Giá: " + currentSkillRefrence.skillData.price.ToString() + " xu";
        totalCoint.text = GameConfigManager.Instance.playerDataLogic.GetCoint(SaveManager.Data).ToString();
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
}
