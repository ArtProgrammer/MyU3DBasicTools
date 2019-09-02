using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;

namespace GameContent.Fight
{
    public class FighterData : MonoBehaviour, IUpdateable
    {
        public int Jingli = 100;

        private int Health = 100;

        public int HP 
        {
            set
            {
                if (Health != value)
                {
                    Health = value;
                    if (HPChangedCallbacks != null)
                    {
                        HPChangedCallbacks(value);
                    }
                }
            }
            get
            {
                return Health;
            }
        }

        Action<int> HPChangedCallbacks;

        public int Hun = 100;

        private int FaliNum = 100;

        public int Fali
        { 
            set
            {
                if (FaliNum != value)
                {
                    FaliNum = value;
                    if (FaliChangedCallbacks != null)
                    {
                        FaliChangedCallbacks(value);
                    }
                }
            }
            get
            {
                return FaliNum;
            }
        }

        Action<int> FaliChangedCallbacks;



        void Awake()
        { 

        }

        public void LoadData()
        { 

        }

        public void OnUpdate(float dt)
        { 

        }

        public virtual void OnFixedUpdate(float dt)
        {

        }
    }
}