using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [SerializeField] private RectTransform poolCanvasUI;

    public Dictionary<GameObject, Queue<GameObject>> poolDict = new Dictionary<GameObject, Queue<GameObject>>();

    public Dictionary<GameObject, Transform> parentPoolDict = new Dictionary<GameObject, Transform>();

    public Dictionary<GameObject, GameObject> instanceObject = new Dictionary<GameObject, GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {

        if (!poolDict.ContainsKey(prefab))
        {
            poolDict[prefab] = new Queue<GameObject>();

            GameObject obj = new GameObject();
            obj.name = prefab.name;

            //xem prefab nay co transform hay rect transform

            var rectComponent = prefab.GetComponent<RectTransform>();
            if (rectComponent != null)
            {

                obj.transform.SetParent(poolCanvasUI, false);
                //obj.transform.parent = poolCanvasUI;
                // obj.transform.localScale = Vector3.one;

            }
            else
            {
                obj.transform.parent = transform;
            }

            parentPoolDict[prefab] = obj.transform;
        }

        GameObject gameObj = null;
        if (poolDict[prefab].Count == 0)
        {
            gameObj = Instantiate(prefab, parentPoolDict[prefab]);
            instanceObject[gameObj] = prefab;
        }
        else
        {
            while (poolDict[prefab].Count > 0)
            {
                gameObj = poolDict[prefab].Dequeue();
                if (gameObj != null) break;
            }
        }


        gameObj.transform.position = position;
        gameObj.transform.rotation = rotation;
        gameObj.SetActive(true);
        return gameObj;
    }

    public GameObject Spawn_ver2(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        //neu trong pool chua chua key nay 
        if (!poolDict.ContainsKey(prefab))
        {
            //tao key cho prefab
            poolDict[prefab] = new Queue<GameObject>();

            //tao gameobject de chua danh sacsh cacs object cua prefab nay tren sence
            var gameParent = new GameObject();
            gameParent.name = prefab.name;
            //gameobject nay lam con cua objectpooler
            gameParent.transform.SetParent(transform);
            //dua gameobject nay vao trong poolParent de xac dinh vi tri
            parentPoolDict[prefab] = gameParent.transform;
        }

        //neu trong danh sach chua co 1 object nao  thi sinh ra 1 game object
        GameObject gameObj;
        if (poolDict[prefab].Count == 0)
        {
            gameObj = Instantiate(prefab);
        }
        else
        {
            //neu da co trong queu
            gameObj = poolDict[prefab].Dequeue();
        }

        //setup vi tri
        gameObj.transform.position = position;
        gameObj.transform.rotation = rotation;
        gameObj.SetActive(true);
        //   Debug.Log($"{gameObj.name} co toa do world x = {gameObj.transform.position.x},toa do y = {gameObj.transform.position.y},toa do z = {gameObj.transform.position.z}");
        //   Debug.Log($"{gameObj.name} co toa do local x = {gameObj.transform.localPosition.x},toa do y = {gameObj.transform.localPosition.y},toa do z = {gameObj.transform.localPosition.z}");
        return gameObj;

    }
    public void Despawn(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        if (poolDict.ContainsKey(prefab))
        {
            poolDict[prefab].Enqueue(obj);
        }

        if (parentPoolDict.ContainsKey(prefab))
        {
            Debug.Log("no da duoc dua voa pool du t ko co goi despawn ?? chăc chan la co gọi");
            obj.transform.SetParent(parentPoolDict[prefab], false);
            // obj.transform.localScale = Vector3.one;
        }
    }


    //tim kiem prefab cua 1 gameoject nhu nao nhi
    public GameObject GetPrefabObject(GameObject obj)
    {
        return instanceObject[obj];
    }
    private void Start()
    {

    }


    private void Update()
    {

    }
}
