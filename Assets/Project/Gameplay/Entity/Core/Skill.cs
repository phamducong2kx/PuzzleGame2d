using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [SerializeField] public SkillData skillData;
    public string idSkill;
    public float coolDown;
    public float coolDownRemaining;
    public TextMeshProUGUI amounttext;
    public TextMeshProUGUI coolDownText;
    public Image backgroundImage;
    public Image coolDownOverlay;

    public int amountSkill = 0;
    public float price;
    public Button buttonInteract;
    public Button buttonShoppe;
    public ISKillState skillState;
    public bool isCoolDownRunning;
    public TextMeshProUGUI notice;
    public Canvas canvas;
    public float selectTiming;
    public bool activeSkill = false;
    public bool isFirstTouch = false;


    private void Awake()
    {
        buttonInteract.onClick.AddListener(() =>
        {
            GetButton();
        });
    }

    private void GetButton()
    {

        if (isCoolDownRunning || amountSkill == 0) return;


        isFirstTouch = !isFirstTouch;
        if (isFirstTouch)
        {
            //  activeSkill = false;
            InputHandler.Instance.SetStrategy(skillState);
        }

        else
        {
            skillState.OnEnterState();

        }

    }


    public void ChangeUpdateUsingSkill(int numberChange)
    {
        //update so lunog skikk
        GameConfigManager.Instance.playerDataLogic.UpdateAmountSkill(idSkill, numberChange);
        //GameConfigManager.Instance.playerDataLogic.UpdateAmountSkill(idSkill);
        amountSkill = GameConfigManager.Instance.playerDataLogic.GetSkillProgress(idSkill).amount;
        amounttext.text = amountSkill.ToString();
        //update bieu tuong shoppe
        SetUpButtonShoppe();
    }

    public void SetUp()
    {
        //id
        idSkill = skillData.idItem;
        //skillsate
        skillState = skillData.GetSkillState(this);
        GameConfigManager.Instance.playerDataLogic.AddSkillToList(idSkill);

        //cooldown
        coolDown = skillData.Cooldown;
        coolDownText.text = "";
        coolDownOverlay.fillAmount = 0;

        //amount skill
        amountSkill = GameConfigManager.Instance.skillLogic.GetAmountSkillById(idSkill);
        amounttext.text = amountSkill.ToString();
        SetUpButtonShoppe();

        //price
        price = skillData.price;
        backgroundImage.sprite = GameConfigManager.Instance.itemLogic.GetItemInfoById(idSkill).icon;
        isCoolDownRunning = false;
        canvas.overrideSorting = false;
        selectTiming = skillData.selectTiming;

        coolDownRemaining = SetUpCoolDown(idSkill);


    }

    public void SetUpButtonShoppe()
    {
        //neu nhu amount =0 thi hien len casi nut shoppe
        if (amountSkill == 0)
        {
            buttonShoppe.gameObject.SetActive(true);
            buttonShoppe.onClick.AddListener(() =>
            {
                //bat object
                UIManager.Instance.gameplayPannel.skiillShopPopup.gameObject.SetActive(true);
                UIManager.Instance.gameplayPannel.skiillShopPopup.shoppePannelButton.gameObject.SetActive(true);
                // dong time lai
                // UIManager.Instance.gameplayPannel.timeSystem.isRunning = false;

                Time.timeScale = 0f;
                //set up curent Skill cho popup shoppe
                SkiillShopPopup.currentSkillRefrence = this;
                UIManager.Instance.gameplayPannel.skiillShopPopup.ResetCurrentSkillData();
                Debug.Log("curent skill dât hoen tai la " + SkiillShopPopup.currentSkillRefrence);
            });
        }
        else
        {
            buttonShoppe.gameObject.SetActive(false);
            buttonShoppe.onClick.RemoveAllListeners();
        }
    }

    private void OnEnable()
    {
        //tinh xem neu thoi gian hoi chiu van con thi gan cho no = cooldown roi chay tiep thoi gian con lai
    }
    private void OnDisable()
    {
        //updayte coooldown trong day
        if (coolDownRemaining < coolDown)
        {
            GameConfigManager.Instance.playerDataLogic.UpdateCoolDown_AfterUsing(idSkill, coolDownRemaining);
        }
        //reset isFirstTouch
        isFirstTouch = false;
    }
    void Start()
    {

    }
    void Update()
    {

        if (!isCoolDownRunning)
        {

            return;
        }
        coolDownRemaining -= Time.deltaTime;

        if (coolDownRemaining > 0)
        {
            coolDownText.text = Mathf.CeilToInt(coolDownRemaining).ToString();
            coolDownOverlay.fillAmount = coolDownRemaining / coolDown;  // int;
        }
        else
        {
            //sau khi cooldown chay xong thi update cooldown , trong trường hợp này coolDownRemaing sẽ gan bang là 0
            GameConfigManager.Instance.playerDataLogic.UpdateCoolDown_AfterUsing(idSkill, 0);
            coolDownText.text = "";
            coolDownOverlay.fillAmount = 0f;
            isCoolDownRunning = false;
            coolDownRemaining = coolDown;
        }

    }

    //chaneg canvas
    public void OpenCanvasSortingLayer()
    {
        canvas.overrideSorting = true;
        canvas.sortingLayerName = "top";
    }

    //dong layer
    public void CloseCanvasSortingLayer()
    {
        canvas.overrideSorting = false;

    }

    //setup cooldown
    private float SetUpCoolDown(string idSkill)
    {
        //tim kiem skillProgress thoe id
        var skillProgress = GameConfigManager.Instance.playerDataLogic.GetSkillProgress(idSkill);
        float coolDownPast = skillProgress.cooldownRemaining;
        if (coolDownPast > 0)
        {

            isCoolDownRunning = true;


            return coolDownPast;
        }
        else if (coolDownPast == 0)
        {
            return coolDown;
        }
        return -1;

    }
}
