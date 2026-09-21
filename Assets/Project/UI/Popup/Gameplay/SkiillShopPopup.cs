using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkiillShopPopup : MonoBehaviour
{
    public Button shoppePannelButton;
    public Skill currentSkillRefrence;
    public Image imageSkill;
    public TextMeshProUGUI textDescribe;
    public TextMeshProUGUI textPrice;
    public Button buySkill;
    public TextMeshProUGUI totalCoint;
    public TextMeshProUGUI kocotien;


    // Start is called before the first frame update
    private void Awake()
    {
        SetupCoint();
        SetupShoppePannel();
        SetupBuyButton();
        kocotien.text = "Không đủ tiền , vòa shop mà mua !";
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        InputHandler.Instance.currentSkill = null;
        kocotien.gameObject.SetActive(false);
    }
    private void SetupCoint()
    {
        var coint = GameConfigManager.Instance.playerDataLogic.GetCoint(SaveManager.Data);
        totalCoint.text = coint.ToString();
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
            currentSkillRefrence = InputHandler.Instance.currentSkill;
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
            else
            {
                // mở ra của hàng 
                kocotien.gameObject.SetActive(true);
            }

        });

    }

    public void ResetCurrentSkillData(Skill x)
    {
        if (x == null)
        {
            Debug.Log("skill data dang co gia tri la null");
        }
        else
        {
            Debug.Log("skill data dang co gia tri khacs null");
        }
        var skillData = GameConfigManager.Instance.skillLogic.GetSkilLData(x.idSkill);
        imageSkill.sprite = GameConfigManager.Instance.itemLogic.GetItemInfoById(skillData.idItem).icon;
        textDescribe.text = skillData.desscribeSkill;
        textPrice.text = "Giá: " + skillData.price.ToString() + " xu";
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
