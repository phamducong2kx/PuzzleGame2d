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
        SetStrategy(currentSkillState);
    }
    public void SetStrategy(ISKillState newStrategy)
    {
        currentSkillState.OnExitState();
        currentSkillState = newStrategy;
        currentSkillState.OnEnterState();
    }

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


        //tìm kiếm collider trong phạm vi 0,02f, nó sẽ đi theo hướng z
        Collider2D hitCollider = Physics2D.OverlapCircle(mousePos2D, 0.02f);

        if (hitCollider == null)
        {
            return;
        }

        Bolt hitBolt = hitCollider.GetComponent<Bolt>();
        if (hitBolt != null)
        {
            currentSkillState.OntapBolt(hitBolt);
        }


        var hitHole = hitCollider.GetComponent<Hole>();
        if (hitHole != null)
        {
            currentSkillState.OntapHole(hitHole);
        }



        var hitPlank = hitCollider.GetComponent<Plank>();
        if (hitPlank != null)
        {
            currentSkillState.OntapPlank(hitPlank);
        }
    }
































}
