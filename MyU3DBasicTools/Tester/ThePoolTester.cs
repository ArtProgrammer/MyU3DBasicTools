using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.PoolSystem;

public class ThePoolTester : MonoBehaviour
{
    [SerializeField] GameObject Prefab1;

    [SerializeField] GameObject Prefab2;

    [SerializeField] GameObject Prefab3;

    List<GameObject> Go1 = new List<GameObject>();
    List<GameObject> Go2 = new List<GameObject>();
    List<GameObject> Go3 = new List<GameObject>();

    public ThePoolTester()
    {
        Debug.Log("$ the pool tester ctor");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("$ The Pool tester start");
        PrefabPoolingSystem.Instance.Prespawn(Prefab1, 5);
        PrefabPoolingSystem.Instance.Prespawn(Prefab2, 5);
        PrefabPoolingSystem.Instance.Prespawn(Prefab3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnObject(Prefab1, Go1);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnObject(Prefab2, Go2);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnObject(Prefab2, Go2);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DespawnRandomObject(Go1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            DespawnRandomObject(Go2);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DespawnRandomObject(Go3);
        }
    }

    void SpawnObject(GameObject prefab, List<GameObject> list)
    {
        GameObject obj = PrefabPoolingSystem.Instance.Spawn(prefab,
            Random.insideUnitSphere * 8f, Quaternion.identity);
        list.Add(obj);
    }

    void DespawnRandomObject(List<GameObject> list)
    {
        if (list.Count == 0)
        {
            return;
        }

        int i = Random.Range(0, list.Count);
        PrefabPoolingSystem.Instance.Despawn(list[i]);
        list.RemoveAt(i);
    }
}
