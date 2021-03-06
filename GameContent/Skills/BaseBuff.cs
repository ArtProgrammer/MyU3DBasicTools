﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
using SimpleAI.PoolSystem;

using GameContent.UsableItem;

namespace GameContent.Skill
{
    public class BaseBuff : IUpdateable, IPoolableComponent, IPrototype<BaseBuff>
    {
        public float DelayTime = 0.0f;

        public float LifeTime = 0.0f;

        public float CurTime = 0.0f;

        public BaseGameEntity Target;

        public BaseGameEntity Dst;

        public bool IsActive = true;

        public int SenderID = 0;

        public int ReceiverID = 0;

        private int TheUniqueID = 0;

        public string EffectName = null;

        public int TheValue = 0;

        public int UniqueID
        {
            set
            {
                TheUniqueID = value;
            }
            get
            {
                return TheUniqueID;
            }
        }

        private BuffKindType TheKindType = 0;

        public BuffKindType KindType
        {
            set
            {
                TheKindType = value;
            }
            get
            {
                return TheKindType;
            }
        }

        public BaseBuff()
        {
            KindType = BuffKindType.None;
        }

        public virtual BaseBuff Clone()
        {
            return NullBuff.Instance;
        }

        public virtual void Spawned()
        {
            IsActive = true;
        }

        public virtual void Despawned()
        {
            CurTime = 0.0f;
            SenderID = 0;
            ReceiverID = 0;
            Target = null;
            Dst = null;

            IsActive = false;
        }

        public virtual void LoadData()
        { 

        }

        public virtual void Attach(BaseGameEntity target)
        {
            Target = target;

            //GameLogicSupvisor.Instance.Register(this);
        }

        public virtual void OnEnter()
        {
            LoadData();
        }

        public virtual void OnExit()
        {
            //GameLogicSupvisor.Instance.Unregister(this);
            // respawn

            SKillMananger.Instance.DespawnBuff(this);
        }

        public virtual void OnProcess(ref float dt)
        {
            if (!IsActive) return;

            CurTime += dt;

            if (!(CurTime < DelayTime ))
            {
                TakeEffect(ref dt);
            }

            if (!(CurTime < LifeTime))
            {
                OnExit();
            }
        }

        public virtual void TakeEffect(ref float dt)
        { 
        }

        // Update is called once per frame
        public void OnUpdate(float dt)
        {
            OnProcess(ref dt);
        }

        public void OnFixedUpdate(float dt)
        {

        }
    }
}