using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamplayAnimation : MonoBehaviour
{
    public CointBurst cointBurst;
    public WinAnimation winAnimation;
    public AddTimeSkill addTimeSkill;
    public Roatation roatationBolt;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
