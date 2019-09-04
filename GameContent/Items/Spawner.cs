using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Supervisors;
using SimpleAI.Utils;
using SimpleAI.PoolSystem;

using Config;

namespace GameContent
{
    public enum SpawnType
    {
        Role,
        NPC,
        Item,
        None
    }

    public class Spawner : MonoBehaviour, IUpdateable
    {
        public SpawnType TypeToSpawn = SpawnType.Item;

        public int UnitCfgID = 0;

        public int SpawnCountPer = 3;

        public float Interval = 10.0f;

        private float CurTimePass = 0.0f;

        public float SpawnRadius = 10.0f;        

        private Vector3 SpawnPos = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);
        }

        public virtual void OnUpdate(float dt)
        {
            CurTimePass += dt;
            if (CurTimePass > Interval)
            {
                for (int i = 0; i < SpawnCountPer; i++)
                {
                    SpawnPos.x = Random.Range(1, SpawnRadius);
                    SpawnPos.y = 1;
                    SpawnPos.z = Random.Range(1, SpawnRadius);
                    SpawnUnit(SpawnPos);
                }

                CurTimePass = 0.0f;
            }
        }

        public virtual void OnFixedUpdate(float dt)
        {

        }

        private void OnDestroy()
        {
            if (GameLogicSupvisor.IsAlive)
                GameLogicSupvisor.Instance.Unregister(this);
        }

        protected void SpawnUnit(Vector3 pos)
        {
            if (TypeToSpawn == SpawnType.Item)
            {
                ItemConfig itemCfg = ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(UnitCfgID);

                if (!System.Object.ReferenceEquals(null, itemCfg))
                {
                    GameObject go = PrefabsAssetHolder.Instance.GetPrefabByID(itemCfg.PrefabID);

                    if (!System.Object.ReferenceEquals(null, go))
                    {
                        GameObject inst =
                            PrefabPoolingSystem.Instance.Spawn(go,
                                pos, Quaternion.identity);

                        inst.transform.SetParent(GlorySupervisor.Instance.RoleContenter);

                        ItemGiver ig = inst.GetComponent<ItemGiver>();
                        if (!System.Object.ReferenceEquals(null, ig))
                        {
                            ig.ItemCfgID = UnitCfgID;
                        }
                    }
                }
            }
        }
    }
}
