using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Utils;
using SimpleAI.PoolSystem;

namespace GameContent
{
    public class FruitSpawner : SingletonAsComponent<FruitSpawner>
    {
        public static FruitSpawner Instance
        {
            get
            {
                return (FruitSpawner)InsideInstance;
            }
        }

        public GameObject Apple;

        public void Intialize()
        {
            Apple = Resources.Load<GameObject>("Prefabs/Apple");
        }

        // Start is called before the first frame update
        //void Start()
        //{            
        //    //PrefabPoolingSystem.Instance.Spawn(Apple, new Vector3(0, 5, 0), Quaternion.identity);

        //    //RandSpawnApple();
        //    //RandSpawnApple();
        //    //RandSpawnApple();
        //}

        public void RandSpawnApple()
        {
            if (!System.Object.ReferenceEquals(null, Apple))
            {
                Vector3 pos = new Vector3(Random.Range(3.0f, 10.0f), 5.0f, Random.Range(3.0f, 10.0f));
                PrefabPoolingSystem.Instance.Spawn(Apple, pos, Quaternion.identity);
            }            
        }
    }
}