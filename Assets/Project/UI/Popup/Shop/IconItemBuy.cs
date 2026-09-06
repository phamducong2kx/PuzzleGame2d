using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconItemBuy : MonoBehaviour
{
    public Image imageBg;
    public TextMeshProUGUI textAmount;

    private void Awake()
    {
        imageBg = GetComponent<Image>();
    }
    public void Setup(Sprite iamge, int amount)
    {
        imageBg.sprite = iamge;
        textAmount.text = amount.ToString();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
