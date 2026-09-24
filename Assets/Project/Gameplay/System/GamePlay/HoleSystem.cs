using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HoleSystem : MonoBehaviour
{

    //tìm kiếm  xem có thể đóng đinh vòa hole này hay không
    public List<Hole> CheckConnectBoltToHole(Hole hole)
    {
        Collider2D[] listCollider = Physics2D.OverlapCircleAll(hole.transform.position, hole.screwRadius);
        List<Hole> list = new List<Hole>();

        //so luong cac colider ma no quet dc
        int plank = 0;
        int holePlank = 0;
        int holeBg = 0;

        foreach (var x in listCollider)
        {
            Hole a = x.GetComponent<Hole>();
            if (a != null)
            {
                // neu no co cha la bg thi + 1 
                if (a.isBackgroundHole) ++holeBg;
                else ++holePlank;
                list.Add(a);
            }
            if (x.GetComponent<Plank>()) ++plank;

        }
        //  Debug.Log("plank = " + plank);
        //  Debug.Log("holeBg  = " + holeBg);
        //   Debug.Log("holePlank = " + holePlank);
        if (holeBg == 1 && plank == holePlank) return list;
        return null;

    }


    public bool HandleHoleCollider(Hole bgHole, List<Hole> listHole, float distance)
    {


        for (int i = 0; i < listHole.Count; i++)
        {
            float x = Vector2.Distance(listHole[i].transform.position, bgHole.transform.position);
            if (x >= distance) return false;
        }
        return true;
    }

    //chek xem tát cả các lỗ đã bị lấp đầy hay chưa
    public bool AreAllHoleBackgroundCoverd()
    {
        //tim doi tuong background
        var background = LevelLoader.Instance.spawnedBackground;


        //duyet danh sacsh ta ca ca hole cua background
        foreach (var hole in background.backgroundHoles)
        {
            if (hole == null) continue;

            //danh sách các collider trong bán kính hole
            var listCollider = Physics2D.OverlapCircleAll(hole.transform.position, hole.screwRadius);

            //nếu như lisColider chỉ có đúng 1 phần từ -> nó chỉ có hole, cho return về false luôn
            if (listCollider.Count() == 1)
            {
                //  Debug.Log("no chi co 1 holenen chac chan la false");
                return false;
            }

            //nếu như có count thì nextx luôn
            var check = listCollider.Any(x => x.GetComponent<Bolt>() != null);
            if (check) continue;


            //danh sách chứa các hole
            var listHole = new List<Hole>();
            int plank = 0;
            int holePLank = 0;
            int holeBg = 0;


            foreach (var x in listCollider)
            {

                //tim kiếm compoent hole
                Hole a = x.GetComponent<Hole>();
                if (a != null)
                {
                    //nếu a là hole background
                    if (a.isBackgroundHole)
                    {
                        ++holeBg;
                    }
                    //nếu ko phải thì chắc chắn là hole của plank
                    else
                    {
                        ++holePLank;
                    }
                    //thêm vào danh sách để chút nữa xét xem các tâm hole có lệch nhau ko
                    listHole.Add(a);
                }
                if (x.GetComponent<Plank>()) ++plank;

            }
            //nếu số lượng plank không == số lượng hole thì tức là lỗ này đã bị lấp đầy , break khỏi for để check hoel khác
            if (plank != holePLank)
            {
                continue;
            }
            //bắt đầu check danh sách các hole
            else
            {
                //nếu các lỗ thông tức là lỗ này chưa dc lấp đầy , return false
                if (HandleHoleCollider(hole, listHole, 0.04f))
                {
                    return false;
                }
                //còn không thì tiếp tục sang hole mới

            }

        }
        //kết thúc check đày đủ mới trả về true:tức là tất cả các lỗ đều đã bị lấp đầy 
        return true;
    }

    void Start()
    {

    }
    void Update()
    {

    }
}
