using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent
{
    public class WeaponConfigMgr
    {
        private static WeaponConfigMgr TheInstance = new WeaponConfigMgr();

        public static WeaponConfigMgr Instance
        {
            get
            {
                return TheInstance;
            }
        }

        private Dictionary<int, WeaponConfig> WeaponConfigs =
            new Dictionary<int, WeaponConfig>();

        public void LoadConfigs()
        {
            {
                WeaponConfig wc = new WeaponConfig();
                wc.ID = 101;
                wc.Prefab = "";
                wc.Range = 10.0f;
                wc.Rate = 3f;
                wc.Type = 1;
                wc.BulletCfgID = 1001;

                WeaponConfigs.Add(wc.ID, wc);
            }

            {
                WeaponConfig wc = new WeaponConfig();
                wc.ID = 102;
                wc.Prefab = "";
                wc.Range = 5.0f;
                wc.Rate = 2.0f;
                wc.Type = 2;
                wc.BulletCfgID = 1002;

                WeaponConfigs.Add(wc.ID, wc);
            }
        }

        public WeaponConfig GetDataByID(int id)
        {
            if (WeaponConfigs.ContainsKey(id))
            {
                return WeaponConfigs[id];
            }

            return null;
        }
    }
}