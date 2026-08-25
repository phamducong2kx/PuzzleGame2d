using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager : MonoBehaviour
{
    public static GameConfigManager Instance;

    public ItemLogic itemLogic;
    public LevelDatabaseLogic levelDatabaseLogic;
    public PlayerDataLogic playerDataLogic;
    public SkillLogic skillLogic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
