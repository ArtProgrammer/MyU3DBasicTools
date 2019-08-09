using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class WeaponSystem : MonoBehaviour, IUpdateable
    {
        //private Dictionary<int, BaseWeapon> Weapons =
        //    new Dictionary<int, BaseWeapon>();

        List<BaseWeapon> Weapons = new List<BaseWeapon>();

        private int WeaponIDInUse = 0;

        public int CurWeaponID
        {
            set
            {
                if (WeaponIDInUse != value)
                {
                    WeaponIDInUse = value;
                    WeaponConfig data = WeaponConfigMgr.Instance.GetDataByID(WeaponIDInUse);
                    if (!System.Object.ReferenceEquals(null, OnWeaponChanged) &&
                        !System.Object.ReferenceEquals(null, data))
                    {                        
                        OnWeaponChanged(data.Range);
                    }
                }
            }
            get
            {
                return WeaponIDInUse;
            }
        }

        public Action<float> OnWeaponChanged;

        private BaseWeapon WeaponInUse = null;

        public BaseWeapon CurWeapon
        {
            set
            {
                if (!System.Object.ReferenceEquals(CurWeapon, value))
                {
                    CurWeapon = value;
                }
            }
            get
            {
                return WeaponInUse;
            }
        }

        public int MaxWeaponCount = 3;
        
        public int MaxCount
        {
            set
            {
                if (MaxWeaponCount != value)
                {
                    MaxWeaponCount = value;
                }
            }
            get
            {
                return MaxWeaponCount;
            }
        }
        
        public int[] WeaponCfgIDs;

        public void LoadWeapons(int id)
        {
            var data = WeaponConfigMgr.Instance.GetDataByID(id);

            if (!System.Object.ReferenceEquals(null, data))
            {
                BaseWeapon bw = new BaseWeapon(data);
                //Weapons.Add(bw.ID, bw);
                Weapons.Add(bw);
            }
        }

        public BaseWeapon GetWeaponByID(int id)
        {
            for (int i = 0; i < Weapons.Count; i++)
            {
                if (Weapons[i].ID == id)
                {
                    return Weapons[i];
                }
            }

            return null;
        }

        // Start is called before the first frame update
        void Start()
        {
            WeaponCfgIDs = new int[MaxWeaponCount];

            // test default weapon config id 101.
            if (MaxWeaponCount > 0)
            {
                WeaponCfgIDs[0] = 101;
            }

            for (int i = 0; i < MaxWeaponCount; i++)
            {
                LoadWeapons(WeaponCfgIDs[i]);
            }

            if (MaxWeaponCount > 0)
            {
                ChangeWeapon(Weapons[0].ID);
            }

            if (GameLogicSupvisor.IsAlive)
                GameLogicSupvisor.Instance.Register(this);
        }

        // Update is called once per frame
        public virtual void OnUpdate(float dt)
        {

        }

        public void ChangeWeapon(int id)
        {
            if (CurWeaponID != id)
            {
                CurWeaponID = id;
                CurWeapon = GetWeaponByID(CurWeaponID);
                if (System.Object.ReferenceEquals(null, OnWeaponChanged))
                {
                    OnWeaponChanged(CurWeaponID);
                }
            }
        }

        public void OnDestroy()
        {
            if (GameLogicSupvisor.IsAlive)
                GameLogicSupvisor.Instance.Unregister(this);
        }
    }
}