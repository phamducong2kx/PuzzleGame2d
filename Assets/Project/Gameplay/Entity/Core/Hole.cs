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







}