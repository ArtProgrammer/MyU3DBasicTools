using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;

namespace GameContent.Skill
{
    public class BaseBuff<T> : IUpdateable
    {
        public float DelayTime = 0.0f;

        public float LastTime = 0.0f;

        public float CurTime = 0.0f;

        public T Target;

        public T Dst;

        public bool IsActive = true;

        public int SenderID = 0;

        public int ReceiverID = 0;

        public BaseBuff()
        {
        }

        public void Spawned()
        {
            IsActive = true;
        }

        public void Despawned()
        {
            IsActive = false;
        }

        public virtual void LoadData()
        { 

        }

        public virtual void Attach(T target)
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
        }

        public virtual void OnProcess(ref float dt)
        {
            if (!IsActive) return;

            CurTime += dt;

            if (!(CurTime < DelayTime ))
            {
                TakeEffect(ref dt);
            }

            if (!(CurTime < LastTime))
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
    }
}