using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUnlockLevel : MonoBehaviour
{

    public GameObject textUnlockLevel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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


    private void OnEnable()
    {
        EventManager.OnUnlockLevel += NotificationUnlockNewLevel;
    }

    private void OnDisable()
    {
        EventManager.OnUnlockLevel -= NotificationUnlockNewLevel;
    }

    public void NotificationUnlockNewLevel()
    {
        var gameOBj = Instantiate(textUnlockLevel, new Vector2(0, 0), Quaternion.identity);
        gameOBj.transform.DOScale(2f, 0.7f).SetEase(Ease.InQuad).SetLink(gameOBj);
        Destroy(gameOBj, 1f);

    }

    

}
