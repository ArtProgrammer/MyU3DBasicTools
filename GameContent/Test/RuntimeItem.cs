using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.PoolSystem;

namespace GameContent
{
    public class RuntimeItem : MonoBehaviour, IPoolableComponent
    {
        // Start is called before the first frame update
        void Start()
        {             
            
        }

        public void Spawned()
        {
            gameObject.SetActive(true);
        }

        public void Despawned()
        {
            gameObject.SetActive(false);            
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("$$$ Apple trigger.");
            if (PrefabPoolingSystem.IsAlive)
            {
                PrefabPoolingSystem.Instance.Despawn(gameObject);
            }

            if (FruitSpawner.IsAlive)
            {
                FruitSpawner.Instance.RandSpawnApple();
            }
            else
            {
                Debug.Log("$$$ Fruit Spawner is not alive.");
            }
        }
    }
}
