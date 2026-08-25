using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopZone : MonoBehaviour
{
    public Button setting;
    public TextMeshProUGUI cointText;




    void Start()
    {
        cointText.text = SaveManager.Data.coint.ToString();
    }

    public void AddCoint(int coint)
    {
        SaveManager.AddCoint(coint);
        cointText.text = SaveManager.Data.coint.ToString();
    }

    public void AddCoint_NotSave(int coint)
    {

        cointText.text = SaveManager.Data.coint.ToString();
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



}
