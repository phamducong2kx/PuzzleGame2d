using System.Collections.Generic;
using UnityEngine;


public class Hole : MonoBehaviour
{



    public string holeId;
    public bool isBackgroundHole;
    public float screwRadius = 0.2f;


    public Bolt currentBolt;
    public Plank plankParent;



    private SpriteRenderer spriteRenderer;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

    }



    public void SetPlankParent(Plank plank)
    {
        plankParent = plank;
        isBackgroundHole = false;

    }

    public void SetAsBackgroundHole()
    {
        isBackgroundHole = true;
        plankParent = null;

    }



    //tìm kiếm  xem có thể đóng đinh vòa hole này hay không
    public static List<Hole> CheckBoltToHole(Hole hole)
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
                    //  Debug.Log("phat hien hole bg");
                }
                else
                {
                    ++holePLank;
                    //Debug.Log("phat hien hole plank");
                }
                list.Add(a);
            }
            if (x.GetComponent<Plank>())
            {
                ++plank;
                // Debug.Log("phat hien  plank");
            }
        }
        //  Debug.Log("plank = " + plank);
        //  Debug.Log("holeBg  = " + holeBg);
        // Debug.Log("holePlank = " + holePLank);
        if (holeBg == 1 && plank == holePLank) return list;
        return null;

    }
    //public static List<Hole> CheckBoltToHole(Vector2 positionHole, Hole hole)
    //{
    //    Collider2D[] listCollider = Physics2D.OverlapCircleAll(positionHole, hole.screwRadius);
    //    List<Hole> list = new List<Hole>();

    //    //phân loại thoe thứ tự, bg = 0 -> cook
    //    int plank = 0;
    //    int holePLank = 0;
    //    int holeBg = 0;

    //    for (int i = 0; i < listCollider.Length; ++i)
    //    {

    //    }
    //    foreach (var x in listCollider)
    //    {
    //        if (x.GetComponent<Hole>())
    //        {
    //            Hole a = x.GetComponent<Hole>();
    //            if (a.isBackgroundHole)
    //            {

    //                ++holeBg;
    //                //  Debug.Log("phat hien hole bg");
    //            }

    //            else
    //            {
    //                ++holePLank;
    //                //Debug.Log("phat hien hole plank");
    //            }
    //            list.Add(a);


    //        }
    //        if (x.GetComponent<Plank>())
    //        {
    //            ++plank;
    //            // Debug.Log("phat hien  plank");
    //        }
    //    }
    //    //  Debug.Log("plank = " + plank);
    //    //  Debug.Log("holeBg  = " + holeBg);
    //    // Debug.Log("holePlank = " + holePLank);
    //    if (holeBg == 1 && plank == holePLank) return list;
    //    return null;

    //}

    public static bool HandleHoleCollider(Hole bgHole, List<Hole> listHole)
    {


        for (int i = 0; i < listHole.Count; i++)
        {
            float x = Vector2.Distance(listHole[i].transform.position, bgHole.transform.position);
            if (x >= 0.04f) return false;
        }
        return true;
    }




}