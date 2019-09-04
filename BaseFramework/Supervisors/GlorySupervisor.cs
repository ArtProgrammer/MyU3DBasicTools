using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Spatial;
using SimpleAI.PoolSystem;
using GameContent;
using GameContent.Skill;
using GameContent.Item;
using GameContent.SimAgent;

using Config;

namespace SimpleAI.Supervisors
{
    public class GlorySupervisor : MonoBehaviour
    {
        public void Initialize()
        {
            Reload();
            DontDestroyOnLoad(gameObject);
        }

        public void Reload()
        {
            SKillMananger.Instance.LoadSkills();

            ItemManager.Instance.LoadDatas();

            BulletCfgMgr.Instance.LoadCfgs();

            WeaponConfigMgr.Instance.LoadConfigs();

            FruitSpawner.Instance.Intialize();
            FruitSpawner.Instance.RandSpawnApple();
            FruitSpawner.Instance.RandSpawnApple();
            FruitSpawner.Instance.RandSpawnApple();

            // load from config
            SpatialManager.Instance.Init(0, 0, 0, 100, 100, 100);

            ConfigDataMgr.Instance.Initialize();

            //Test_SpawnNPC();
        }

        [SerializeField]
        public Transform RoleContenter = null;

        public Transform NPC_Home = null;

        public Transform NPC_Food = null;

        public void Test_SpawnNPC()
        {
            GameObject npc = Resources.Load<GameObject>("Prefabs/Roles/NPC");
            if (npc)
            {
                GameObject npcInstance = PrefabPoolingSystem.Instance.Spawn(npc);

                npcInstance.transform.SetParent(RoleContenter);

                SimWood sw = npcInstance.GetComponent<SimWood>();

                if (sw)
                {
                    sw.Home = NPC_Home;
                    sw.Food = NPC_Food;
                }
            }
        }

        public void Test_SpawnItems(int cfgid, Vector3 pos)
        {
            ItemConfig itemCfg = ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(cfgid);

            if (!System.Object.ReferenceEquals(null, itemCfg))
            {
                GameObject go = PrefabsAssetHolder.Instance.GetPrefabByID(itemCfg.PrefabID);

                if (!System.Object.ReferenceEquals(null, go))
                {
                    GameObject inst =
                        PrefabPoolingSystem.Instance.Spawn(go,
                            pos, Quaternion.identity);

                    inst.transform.SetParent(RoleContenter);

                    ItemGiver ig = inst.GetComponent<ItemGiver>();
                    if (!System.Object.ReferenceEquals(null, ig))
                    {
                        ig.ItemCfgID = cfgid;
                    }
                }                
            }
        }

        void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            //Test_SpawnNPC();

            StartCoroutine("SpawnNPCs");

            StartCoroutine("SpawnItems");
        }

        IEnumerator SpawnNPCs()
        {
            for (int i = 0; i < 30; i++)
            {
                Test_SpawnNPC();
                yield return new WaitForSeconds(6.0f);
            }
        }

        IEnumerator SpawnItems()
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 pos = new Vector3(Random.Range(1, 10),
                    1, Random.Range(1, 10));
                Test_SpawnItems(10002, pos);
                yield return new WaitForSeconds(3.0f);
            }
        }
    }
}