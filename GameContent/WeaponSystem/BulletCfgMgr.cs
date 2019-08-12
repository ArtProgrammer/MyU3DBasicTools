using System;
using System.Collections.Generic;

using UnityEngine;

namespace GameContent
{
    class BulletCfgMgr
    {
        private static BulletCfgMgr TheInstance = new BulletCfgMgr();

        public static BulletCfgMgr Instance
        {
            get
            {
                return TheInstance;
            }
        }

        private Dictionary<int, BulletCfg> Cfgs = 
            new Dictionary<int, BulletCfg>();

        private Dictionary<int, GameObject> Prefabs =
            new Dictionary<int, GameObject>();

        public void LoadCfgs()
        {
            {
                BulletCfg bc = new BulletCfg();
                bc.ID = 1001;
                bc.EffectID = 10001;
                bc.Speed = 5.0f;
                bc.Damage = 5;
                bc.LifeTime = 10.0f;
                bc.AssetPath = "Prefabs/SimpleBullet";
                
                Cfgs.Add(bc.ID, bc);

                var pref = Resources.Load<GameObject>(bc.AssetPath);
                Prefabs.Add(bc.ID, pref);
            }

            {
                BulletCfg bc = new BulletCfg();
                bc.ID = 1002;
                bc.EffectID = 10002;
                bc.Speed = 5.0f;
                bc.Damage = 10;
                bc.LifeTime = 10.0f;
                bc.AssetPath = "Prefabs/SimpleBullet";

                Cfgs.Add(bc.ID, bc);
                var pref = Resources.Load<GameObject>(bc.AssetPath);
                Prefabs.Add(bc.ID, pref);
            }
        }

        public BulletCfg GetDataByID(int id)
        {
            if (Cfgs.ContainsKey(id))
            {
                return Cfgs[id];
            }

            return null;
        }

        public GameObject GetPrefabByID(int id)
        {
            if (Prefabs.ContainsKey(id))
            {
                return Prefabs[id];
            }

            return null;
        }
    }
}
