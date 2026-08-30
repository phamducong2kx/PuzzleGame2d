using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSystem : MonoBehaviour
{

    //tìm kiếm  xem có thể đóng đinh vòa hole này hay không
    public List<Hole> CheckConnectBoltToHole(Hole hole)
    {
        Collider2D[] listCollider = Physics2D.OverlapCircleAll(hole.transform.position, hole.screwRadius);
        List<Hole> list = new List<Hole>();

        //so luong cac colider ma no quet dc
        int plank = 0;
        int holePLank = 0;
        int holeBg = 0;

        foreach (var x in listCollider)
        {
            Hole a = x.GetComponent<Hole>();
            if (a != null)
            {
                if (a.isBackgroundHole)
                {
                    ++holeBg;

                }
                else
                {
                    ++holePLank;

                }
                list.Add(a);
            }
            if (x.GetComponent<Plank>())
            {
                ++plank;
                Debug.Log("phat hien  plank");
            }
        }
        //  Debug.Log("plank = " + plank);
        //  Debug.Log("holeBg  = " + holeBg);
        // Debug.Log("holePlank = " + holePLank);
        if (holeBg == 1 && plank == holePLank) return list;
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

    public bool AreAllHoleBackgroundCoverd()
    {
        //doi tuong background
        var background = LevelLoader.Instance.spawnedBackground;
        //bien de danh dau 

        //duyet danh sacsh ta ca ca hole cua background
        foreach (var hole in background.backgroundHoles)
        {
            if (hole == null) continue;
            var listCollider = Physics2D.OverlapCircleAll(hole.transform.position, hole.screwRadius);
            var listHole = new List<Hole>();
            int plank = 0;
            int holePLank = 0;
            int holeBg = 0;
            bool hasBolt = false;

            foreach (var x in listCollider)
            {
                Hole a = x.GetComponent<Hole>();
                if (a != null)
                {
                    if (a == hole)
                    {
                        ++holeBg;
                    }

                    else
                    {
                        ++holePLank;
                    }
                    listHole.Add(a);
                }
                if (x.GetComponent<Plank>()) ++plank;
                if (x.GetComponent<Bolt>()) hasBolt = true;
            }
            if (plank != holePLank) return true;
            else
            {
                if (!HandleHoleCollider(hole, listHole, 0.04f))
                {
                    return true;
                }
                else if (hasBolt == false) return false;
            }

        }
        return true;
    }

    void Start()
    {

    }
    void Update()
    {

    }
}
