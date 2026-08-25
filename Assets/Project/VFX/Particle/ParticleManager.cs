using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    public ParticleSystem plankFellPrefab;
    public ParticleSystem pickUpBoltPrefab;
    public ParticleSystem pickDownBoltPrefab;
    public GameObject winEffectPrefab;
    public ParticleSystem pickDownDrillPrefab;
    public ParticleSystem lightningPrefab;
    private List<ParticleSystem> listParticlePlankFell = new List<ParticleSystem>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnEnable()
    {

        EventManager.OnPlankFallComplete += HandlePlankFall;
        EventManager.OnBoltPickedUp += HandlePickUpBolt;
        EventManager.OnBoltPlaced += HandlePickDownBolt;
        EventManager.OnLevelComplete += HandleWinEffect;
        EventManager.OnRefreshLevel += HandleRefreshLevel;


        GameConfigManager.Instance.skillLogic.DrillSkillEvent += HandleEventDrillSkill;
        GameConfigManager.Instance.skillLogic.LightNingSKillEvent += HandleLightNingSkill;
    }

    private void HandleLightNingSkill(Plank plank)
    {
        var obj = ObjectPooler.Instance.Spawn(lightningPrefab.gameObject, plank.transform.position, lightningPrefab.transform.rotation);

        //  listParticlePlankFell.Add(obj.GetComponent<>);
        StartCoroutine(DespawnRoutine(lightningPrefab.gameObject, obj, 1f));
    }


    //dua particle vao poooler ngay lap tuc 
    public void HandleGotoHome()
    {
        foreach (var x in listParticlePlankFell)
        {
            ObjectPooler.Instance.Despawn(plankFellPrefab.gameObject, x.gameObject);
        }
    }

    private void HandleEventDrillSkill(Bolt bolt)
    {

        var obj = ObjectPooler.Instance.Spawn(pickDownDrillPrefab.gameObject, bolt.transform.position, pickDownDrillPrefab.transform.rotation);

        listParticlePlankFell.Add(obj.GetComponent<ParticleSystem>());
        StartCoroutine(DespawnRoutine(pickDownDrillPrefab.gameObject, obj, 0.5f));

    }

    private void HandleRefreshLevel()
    {

        //desstroy parrticle
        HandleGotoHome();
    }

    private void OnDisable()
    {
        EventManager.OnPlankFallComplete -= HandlePlankFall;
        EventManager.OnBoltPickedUp -= HandlePickUpBolt;
        EventManager.OnBoltPlaced -= HandlePickDownBolt;
        EventManager.OnLevelComplete -= HandleWinEffect;
        EventManager.OnRefreshLevel -= HandleRefreshLevel;
        GameConfigManager.Instance.skillLogic.DrillSkillEvent -= HandleEventDrillSkill;
        GameConfigManager.Instance.skillLogic.LightNingSKillEvent -= HandleLightNingSkill;

    }

    //particle pick down bolt
    private void HandlePickDownBolt(Bolt bolt)
    {
        var obj = ObjectPooler.Instance.Spawn(pickDownBoltPrefab.gameObject, bolt.transform.position, pickDownBoltPrefab.transform.rotation);

        StartCoroutine(DespawnRoutine(pickDownBoltPrefab.gameObject, obj, 0.5f));
    }

    //particel pick up bolt
    private void HandlePickUpBolt(Bolt bolt)
    {

        var obj = ObjectPooler.Instance.Spawn(pickUpBoltPrefab.gameObject, bolt.transform.position, pickUpBoltPrefab.transform.rotation);
        StartCoroutine(DespawnRoutine(pickUpBoltPrefab.gameObject, obj, 0.5f));
    }

    private void HandlePlankFall(Plank plank)
    {
        Vector2 target = new Vector2(-2f, -13f);
        var obj = ObjectPooler.Instance.Spawn(plankFellPrefab.gameObject, target, plankFellPrefab.transform.rotation);
        //đua object này vào danh sách;
        var refer = obj.GetComponent<ParticleSystem>();
        listParticlePlankFell.Add(refer);
        StartCoroutine(DespawnRoutine(plankFellPrefab.gameObject, obj, 2f));
    }

    private IEnumerator DespawnRoutine(GameObject prefab, GameObject obj, float timeDelay)
    {

        yield return new WaitForSeconds(timeDelay);
        ObjectPooler.Instance.Despawn(prefab, obj);
    }
    private void HandleWinEffect(int oldStar, int newStar)
    {
        var winEffect = ObjectPooler.Instance.Spawn(winEffectPrefab, new Vector2(-18.89f, -5.056089f), Quaternion.identity);
        StartCoroutine(DespawnRoutine(winEffectPrefab, winEffect, 2f));

    }




}
