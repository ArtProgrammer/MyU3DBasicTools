using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Spatial;
using GameContent;
using GameContent.Skill;
using GameContent.Item;

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

            TextAsset ta = Resources.Load("TextAssets/Icons") as TextAsset;

            IconsLoader iconLoader = new IconsLoader();
            Dictionary<int, Icons> icons = iconLoader.LoadConfigData(ta.text);

            int count = icons.Count;
        }

        void Awake()
        {
            Initialize();
        }
    }
}