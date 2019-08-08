using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class WeaponSystem : MonoBehaviour, IUpdateable
    {       
        private Dictionary<int, BaseWeapon> Weapons =
            new Dictionary<int, BaseWeapon>();

        private BaseWeapon CurWeapon = null;

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
        
        public int[] WeaponIDs;

        public void LoadWeapons(int id)
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
            WeaponIDs = new int[MaxWeaponCount];

            for (int i = 0; i < MaxWeaponCount; i++)
            {
                LoadWeapons(WeaponIDs[i]);
            }            
        }

        // Update is called once per frame
        public virtual void OnUpdate(float dt)
        {

        }
    }
}