using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
public class Plank : MonoBehaviour
{
    public string plankId;
    public List<Hole> holes = new List<Hole>();

    public float groundY = -30f;
    public bool hasFallen = false;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public string defaultSortingLayer;
    public string lightningSortingLayer;
    public SortingGroup sortingGroup;
    public SpriteRenderer spriteRender;

    public PlankType plankType;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sortingGroup = GetComponent<SortingGroup>();
        defaultSortingLayer = sortingGroup.sortingLayerName;
        lightningSortingLayer = "top";
        spriteRender = GetComponentInChildren<SpriteRenderer>();

    }

    //getColor
    public void StringToClour(string colorr)
    {
        //color hien tai 
        if (ColorUtility.TryParseHtmlString(colorr, out Color a))
        {
            spriteRender.color = a;

        }

    }

    public string ColorToString()
    {

        var color = spriteRender.color;
        var colorString = $"#{ColorUtility.ToHtmlStringRGBA(color)}";
        return colorString;
    }

    private void OnEnable()
    {
        hasFallen = false;
    }

    private void OnDisable()
    {


        var hingjoinedcomponent = GetComponentsInChildren<HingeJoint2D>();
        foreach (var x in hingjoinedcomponent)
        {
            Destroy(x);
        }

    }


    // rb.y là biến thiên theo frame khi nó rơi
    private void FixedUpdate()
    {
        if (hasFallen == true) return;

        if (transform.position.y <= groundY)
        {

            rb.MovePosition(new Vector2(rb.position.x, groundY));
            rb.bodyType = RigidbodyType2D.Static;
            hasFallen = true;

            //phat plank rơi
            EventManager.InvokePlankFallComplete(this);


            GameManager.Instance.winLoseSystem.Evaluate();
        }
    }

    public void SetupPlankHoles()
    {
        var childHoles = GetComponentsInChildren<Hole>(includeInactive: true);
        foreach (var hole in childHoles)
        {
            holes.Add(hole);
            hole.SetPlankParent(this);
        }
    }

    public void AddBoltConnection(Bolt bolt, Hole hole)
    {
        HingeJoint2D newJoint = gameObject.AddComponent<HingeJoint2D>();
        newJoint.connectedBody = bolt.rb;
        newJoint.anchor = transform.InverseTransformPoint(hole.transform.position);
        // newJoint.anchor = hole.transform.localPosition;
        newJoint.autoConfigureConnectedAnchor = false;
        newJoint.connectedAnchor = Vector3.zero;

    }

    public void RemoveBoltConnection(Bolt bolt)
    {
        HingeJoint2D[] allJoints = gameObject.GetComponents<HingeJoint2D>();
        foreach (var joint in allJoints)
        {
            if (joint.connectedBody == bolt.rb)
            {
                Destroy(joint);
                // --numberHingeJoined;
                // if (numberHingeJoined == 0) hasFallen = true;
                return;
            }
        }
    }

    public void SetDynamicRigibody()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetStaticRigibody()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void VisualDefaultSkill()
    {
        //day sorting group xuong ban dua
        sortingGroup.sortingLayerName = defaultSortingLayer;
    }

    public void VisualLightNingSkill()
    {
        //day ssorting group sorting layer len muc cao nhat
        sortingGroup.sortingLayerName = lightningSortingLayer;
    }
}
