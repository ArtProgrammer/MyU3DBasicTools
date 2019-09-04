using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Utils;
using SimpleAI.Game;
using SimpleAI.PoolSystem;
using GameContent.SimAgent;

namespace GameContent
{
    public class ItemGiver : MonoBehaviour, IPoolableComponent, IUpdateable
    {
        public int UID = 0;

        public int ItemCfgID = 0;

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

        public virtual void OnAddSuccess(SimWood target)
        {
            //IDAllocator.Instance.RecycleID(UID);
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