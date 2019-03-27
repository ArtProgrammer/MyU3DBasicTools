using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Messaging;
using SimpleAI.Utils;

namespace SimpleAI.Game
{
    public class BaseGameEntity : MonoBehaviour, ITelegramReceiver, IUpdateable
    {
        private int TheID = IDAllocator.Instance.InvalidID;

        public int ID
        {
            get;
        }

        public BaseGameEntity()
        {
            TheID = IDAllocator.Instance.GetID();
        }

        ~BaseGameEntity()
        {
            IDAllocator.Instance.RecycleID(TheID);
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// Finish this instance.
        /// </summary>
        public virtual void Finish()
        {

        }

        public virtual void Process() { }

        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);
            EntityManager.Instance.RegisterEntity(this);
            Initialize();
        }

        void OnDestroy()
        {
            Finish();

            if (GameLogicSupvisor.IsAlive)
            {
                GameLogicSupvisor.Instance.Unregister(this);
            }

            if (EntityManager.IsAlive)
            {
                EntityManager.Instance.RemoveEntity(this);
            }
        }

        public virtual void OnUpdate(float dt)
        {
            Process();
        }

        public virtual bool HandleMessage(ref Telegram msg) 
        {
            return false;
        }
    }
}