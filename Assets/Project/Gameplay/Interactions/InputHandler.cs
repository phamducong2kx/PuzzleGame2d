using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;

    private Camera mainCamera;

    public Bolt pickedBolt;

    public LayerMask layerMask;

    public ISKillState currentSkillState = new DefaultState();
    public Skill currentSkill = null;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

        }
        Instance = this;
        mainCamera = Camera.main;
        pickedBolt = null;
    }
    private void Start()
    {
        SetStrategy(currentSkillState, null);
    }
    public void SetStrategy(ISKillState newStrategy, Skill skill)
    {
        currentSkillState.OnExitState();
        currentSkillState = newStrategy;
        if (skill != null)
        {
            currentSkill = skill;
        }
        else
        {
            // Debug.Log("cuernt skill dang co gia tri la null");
        }

        currentSkillState.OnEnterState();
    }

    //public void SetStrategy_2(ISKillState newStrategy, Skill skill)
    //{
    //    currentSkillState.OnExitState();
    //    currentSkillState = newStrategy;
    //    if (currentSkill != null)
    //    {
    //        currentSkill = skill;
    //    }

    //    currentSkillState.OnEnterState();
    //}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            TryTap();
        }
    }
    private void TryTap()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);


        //tìm kiếm collider trong phạm vi 0,02f,tim kiếm 
        var hitCollider = Physics2D.OverlapCircle(mousePos2D, 0.15f);



        if (hitCollider == null)
        {
            return;
        }

        Bolt hitBolt = hitCollider.GetComponent<Bolt>();
        if (hitBolt != null)
        {
            currentSkillState.OntapBolt(hitBolt);
            return;
        }


        //var hitHole = hitCollider.GetComponent<Hole>();
        //if (hitHole != null)
        //{
        //    currentSkillState.OntapHole(hitHole);
        //    //  Debug.Log("check day la hole");
        //    return;
        //}



        var hitPlank = hitCollider.GetComponent<Plank>();
        if (hitPlank != null)
        {
            currentSkillState.OntapPlank(hitPlank);
            Debug.Log("check day la plank");
            return;
        }


        var hitHole = hitCollider.GetComponent<Hole>();
        if (hitHole != null)
        {
            currentSkillState.OntapHole(hitHole);
            //  Debug.Log("check day la hole");
            return;
        }


    }
































}
