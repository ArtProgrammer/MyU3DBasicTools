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