using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Timer;

namespace GameContent
{
    public class WeaponSystem// : MonoBehaviour, IUpdateable
    {
        public BaseGameEntity Owner = null;

        private List<BaseWeapon> Weapons = new List<BaseWeapon>();

        private Regulator SensorReg = null;

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
                if (!System.Object.ReferenceEquals(WeaponInUse, value))                             
                {
                    WeaponInUse = value;
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

        public WeaponSystem(BaseGameEntity owner)
        {
            Owner = owner;

            SensorReg = new Regulator(50.0f);
        }

        /// <summary>
        /// Now, load weapons from static config id and data.
        /// </summary>
        /// <param name="id"></param>
        public void LoadWeapons(int id)
        {
            var data = WeaponConfigMgr.Instance.GetDataByID(id);

            if (!System.Object.ReferenceEquals(null, data))
            {
                BaseWeapon bw = new BaseWeapon(data);
                AddWeapon(bw);
            }
        }

        public void AddWeapon(BaseWeapon weapon)
        {
            if (!System.Object.ReferenceEquals(null, weapon))
            {
                Weapons.Add(weapon);
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

            //if (GameLogicSupvisor.IsAlive)
            //    GameLogicSupvisor.Instance.Register(this);
        }

        // Update is called once per frame
        private Vector3 Dist2Target = Vector3.zero;
        public virtual void OnUpdate(float dt)
        {
            //if (SensorReg.IsReady())
            //{
                
            //}
        }

        public void SelectWeapon()
        {
            if (Owner.TargetSys.IsTargetPresent())
            {
                BaseWeapon temp = null;

                Dist2Target = Owner.TargetSys.CurTarget.Position - Owner.Position;
                float dist = Dist2Target.magnitude;

                float bestSoFar = float.MinValue;

                for (int i = 0; i < Weapons.Count; i++)
                {
                    if (!System.Object.ReferenceEquals(null, Weapons[i]))
                    {
                        float score = Weapons[i].GetDesirability(dist);

                        if (score > bestSoFar)
                        {
                            bestSoFar = score;
                            temp = Weapons[i];
                        }
                    }
                }

                if (!System.Object.ReferenceEquals(null, temp))
                {
                    ChangeWeapon(temp);
                }
            }
            else
            {
                // set the default weapon;
                if (!System.Object.ReferenceEquals(null, CurWeapon) &&
                    Weapons.Count > 0)
                {
                    CurWeapon = Weapons[0];
                }
            }
        }

        public void ChangeWeapon(int id)
        {
            if (CurWeaponID != id)
            {
                CurWeaponID = id;
                CurWeapon = GetWeaponByID(CurWeaponID);
                if (!System.Object.ReferenceEquals(null, OnWeaponChanged))
                {
                    OnWeaponChanged(CurWeaponID);
                }
            }
        }

        public void ChangeWeapon(BaseWeapon weapon)
        {
            if (!System.Object.ReferenceEquals(null, weapon) &&
                !System.Object.ReferenceEquals(CurWeapon, weapon))
            {
                CurWeapon = weapon;
                if (!System.Object.ReferenceEquals(null, OnWeaponChanged))
                {
                    OnWeaponChanged(CurWeaponID);
                }
            }
        }

        public void Use(BaseGameEntity target, BaseGameEntity origin)
        {
            if (!System.Object.ReferenceEquals(null, CurWeapon) &&
                CurWeapon.IsReady())
            {
                CurWeapon.Use(target, origin);
            }
        }

        public void Use(BaseGameEntity target, Transform origin)
        {
            if (!System.Object.ReferenceEquals(null, CurWeapon) &&
                CurWeapon.IsReady())
            {
                CurWeapon.Use(target, origin);
            }
        }

        public void OnDestroy()
        {
            //if (GameLogicSupvisor.IsAlive)
            //    GameLogicSupvisor.Instance.Unregister(this);
        }
    }
}