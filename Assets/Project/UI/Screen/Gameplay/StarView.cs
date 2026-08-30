using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class StarView : MonoBehaviour
{
    [SerializeField] private StarICon prefabStarIcon;
    public List<StarICon> listStarICon;
    public RectTransform gg;

    private void Awake()
    {
        //khoi tao cac hinh ngoi sao
        //GenerateStarIcon();
    }
    private void OnEnable()
    {
        GameConfigManager.Instance.skillLogic.AddTimeSkill += HandleAddTime;
        // RegisterEventStarView();
    }

    private void HandleAddTime()
    {

        KillAllAnimationStar();
    }

    private void OnDisable()
    {

        KillAllAnimationStar();
        GameConfigManager.Instance.skillLogic.AddTimeSkill -= HandleAddTime;
        //khong despan trong day dc vi co loi Despawn();
    }

    public void KillAllAnimationStar()
    {

        for (int i = 0; i < listStarICon.Count; ++i)
        {
            listStarICon[i].transform.DOKill();
            listStarICon[i].ResetIcon();

            //   listStarICon[i].SetUpStarIcon();


        }
    }



    void Start()
    {

    }

    void Update()
    {

    }
    private void OnDestroy()
    {

    }

    // moi object se dang ki su kien , de o day thi thuan loi hon
    public void GenerateStarIcon()
    {
        // Debug.Log($"vi tri local cua starview la x = {transform.localPosition.x} , y ={transform.localPosition.y} , z la {transform.localPosition.z}");
        //  Debug.Log($"vi tri tren world cua starview la x = {transform.position.x} , y ={transform.position.y} , z la {transform.position.z}");
        //khoi tao cacs icon newStar
        for (int i = 0; i < 3; ++i)
        {
            //lay ra tu pool ,, gan position thi luc nay starobejct co vi tri la vi tri cua pannel tren world
            var starObj = ObjectPooler.Instance.Spawn(prefabStarIcon.gameObject, transform.position, prefabStarIcon.transform.rotation);
            //  Debug.Log($"toa do cua ngoi sao thu {i + 1} la x  = {starObj.transform.position.x} , y ={starObj.transform.position.y} , z la {starObj.transform.position.z}");


            starObj.transform.SetParent(transform, false);
            // Debug.Log($"sau khi gan pannel lam cha , toa do cua ngoi sao thu {i + 1} la x  = {starObj.transform.position.x} , y ={starObj.transform.position.y} , z la {starObj.transform.position.z}");
            var starIcon = starObj.GetComponent<StarICon>();
            starIcon.SetUpStarIcon(i + 1);
            listStarICon.Add(starIcon);

        }

        Canvas.ForceUpdateCanvases();
        // Hoặc: UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

        // 2. Bây giờ in ra mới thấy 3 tọa độ X Y Z KHÁC NHAU do GridLayoutGroup đã xếp xong
        for (int i = 0; i < listStarICon.Count; i++)
        {
            // Debug.Log($"Vị trí ĐÚNG của ngôi sao {i + 1} sau khi xếp Grid là: {listStarICon[i].transform.position}");
        }
    }

    public void Despawn()
    {
        for (int i = 0; i < listStarICon.Count; ++i)
        {
            ObjectPooler.Instance.Despawn(prefabStarIcon.gameObject, listStarICon[i].gameObject);
        }
        listStarICon.Clear();
    }


    //các object con trongdanh sách dang ki suej kien 
    public void RegisterEventStarView()
    {
        for (int i = 0; i < listStarICon.Count; ++i)
        {
            GameManager.Instance.timerSystem.OnStarView -= listStarICon[i].HandleStar;
            GameManager.Instance.timerSystem.OnStarView += listStarICon[i].HandleStar;
            listStarICon[i].SetYellowStar();
        }
    }

    //reset danh sacsh cac star dua tren moc sao
    public void ResetStar_TheoMocSao(int mocsao)
    {
        for (int i = 0; i < listStarICon.Count; ++i)
        {
            if (mocsao > 0)
            {
                listStarICon[i].SetUpStarIcon(i + 1);
                --mocsao;
            }
        }
    }




}
