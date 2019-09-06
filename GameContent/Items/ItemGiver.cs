using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Utils;
using SimpleAI.Game;
using SimpleAI.PoolSystem;
using GameContent.SimAgent;

namespace GameContent
{
    /// <summary>
    /// Attach to items' prefabs in the scene.
    /// </summary>
    public class ItemGiver : MonoBehaviour, IPoolableComponent, IUpdateable
    {
        public int UID = 0;

        public int ItemCfgID = 0;

        public string Name;

        public int Count = 1;

        public bool IsActive = true;

        private void Start()
        {
            UID = IDAllocator.Instance.GetID();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void Spawned()
        {
            gameObject.SetActive(true);
        }

        public virtual void Despawned()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnUpdate(float dt)
        {

        }

        public virtual void OnFixedUpdate(float dt)
        {

        }

        public virtual bool AddToTarget(SimWood target)
        {
            if (!System.Object.ReferenceEquals(null, target))
            {
                if (target.AddItem(ItemCfgID, Count))
                {                                        
                    return true;
                }                
            }

            return false;
        }

        private void OnDestroy()
        {
            if (IDAllocator.IsAlive)
                IDAllocator.Instance.RecycleID(UID);
        }

        public virtual void OnAddSuccess(SimWood target)
        {
            PrefabPoolingSystem.Instance.Despawn(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other && other.transform)
            {
                SimWood sw = other.transform.parent.GetComponent<SimWood>();
                if (!System.Object.ReferenceEquals(null, sw))
                {
                    // sw.Bag.AddItem(id);

                    if (AddToTarget(sw))
                    {
                        OnAddSuccess(sw);
                    }
                }
            }
        }
    }
}