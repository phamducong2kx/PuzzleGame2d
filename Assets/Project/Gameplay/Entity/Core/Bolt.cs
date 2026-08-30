using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


public class Bolt : MonoBehaviour
{
    public string boltId;
    public bool isPickedUp = false;
    public Hole backgroundHole;
    public List<Hole> plankHoles = new List<Hole>();
    public Rigidbody2D rb;
    [SerializeField] private CircleCollider2D col;
    public SpriteRenderer spriteRenderer;
    public Sprite boltIdle;
    public Sprite boltPickUp;
    [SerializeField] private Material materialDefaultSkill;
    [SerializeField] private Material materialDrillSkill;
    public string defaultSortingLayer;
    public string drillSortingLayer;
    public SortingGroup sortingGroup;



    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        col = GetComponent<CircleCollider2D>();

        //set up sprite ban đầu của bolt
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // view ban dau
        spriteRenderer.sprite = boltIdle;
        sortingGroup = GetComponent<SortingGroup>();
        defaultSortingLayer = sortingGroup.sortingLayerName;
        drillSortingLayer = "top";



    }
    private void OnEnable()
    {
        //dang ki skill drillSkill : khong nen dang ki su kien
        col.enabled = true;
    }

    private void HandleDrillSkill()
    {

    }

    private void Start()
    {

    }
    private void Update()
    {

    }
    public void AttachConnectToHole_OfBolt(Hole bgHole, List<Hole> pHoles)
    {
        backgroundHole = bgHole;
        plankHoles = pHoles;

        //vị trí bolt = hole background
        transform.position = new Vector3(
            bgHole.transform.position.x,
            bgHole.transform.position.y, -2);

        backgroundHole.currentBolt = this;


        if (pHoles.Count == 0) return;


        if (pHoles.Count > 0)
        {
            foreach (var hole in pHoles)
                hole.currentBolt = this;
        }

        transform.localScale = Vector3.one;

        foreach (var hole in plankHoles)
        {
            hole.currentBolt = this;

            var plankParent = hole.plankParent;


            plankParent.AddBoltConnection(this, hole);


        }

    }
    public void RemoveConnectToHole_OfBolt()
    {
        backgroundHole = null;

        if (plankHoles.Count == 0) return;

        //duyet mang danh sách hole
        foreach (var hole in plankHoles)
        {

            //moi hole se tim plank cha
            var plankParent = hole.plankParent;

            //xoa hingJoined2d 
            plankParent.RemoveBoltConnection(this);

            hole.currentBolt = null;
        }

        plankHoles.Clear();
    }

    public void VisualDrillSkill()
    {
        //cho no co layer cao nhat
        sortingGroup.sortingLayerName = drillSortingLayer;
        //material skill
        spriteRenderer.material = materialDrillSkill;
    }
    public void VisualDefaultSkill()
    {
        //dua layer mac dinh
        sortingGroup.sortingLayerName = defaultSortingLayer;
        //material binh thuong
        spriteRenderer.material = materialDefaultSkill;
    }

    public void SetDynamicRigibody()
    {
        if (transform.position.y <= 30f)
            rb.bodyType = RigidbodyType2D.Dynamic;

    }
    public void SetStaticRigibody()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void SetCollider()
    {
        col.enabled = false;
    }

}
