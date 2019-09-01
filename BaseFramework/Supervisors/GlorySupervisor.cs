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

            Test_SpawnNPC();
        }

        [SerializeField]
        private Transform RoleContenter = null;

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

        void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            StartCoroutine("SpawnNPCs");
        }

        IEnumerator SpawnNPCs()
        {
            for (int i = 0; i < 30; i++)
            {
                Test_SpawnNPC();
                yield return new WaitForSeconds(6.0f);
            }
        }
    }
}