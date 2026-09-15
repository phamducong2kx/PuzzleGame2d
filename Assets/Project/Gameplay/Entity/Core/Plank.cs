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
        spriteRender = GetComponent<SpriteRenderer>();

    }

    public void SetupPlank(PlankData plankData)
    {
        //id
        plankId = plankData.plankId;

        //type
        plankType = plankData.plankType;


        spriteRender.size = new Vector2(plankData.sizeWidth, plankData.sizeHeight);


        //rigibody
        SetDynamicRigibody();

        //scale
        transform.localScale = new Vector3(plankData.scaleX, plankData.scaleY, 1);

        // Sorting Group
        sortingGroup.sortingLayerName = plankData.sortingLayerName;
        sortingGroup.sortingOrder = plankData.sortingOrder;

        //mafu sawc
        StringToClour(plankData.hexColor);


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
            AnimationManager.Instance.gamePlayAnimation.cointBurst
                .PlayAnimationPlankFell(transform.position, 3, 0.3f, UIManager.Instance.gameplayPannel.coinview.transform, () =>
                {
                    UIManager.Instance.gameplayPannel.coinview.UpdateTextCoin();
                });


            //check xem da thoa man hay chua
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

    public float GetHight()
    {
        if (spriteRender.size != null)
            return spriteRender.size.y;
        return 0;
    }
    public float GetWeight()
    {
        if (spriteRender.size != null)
            return spriteRender.size.x;
        return 0;
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
