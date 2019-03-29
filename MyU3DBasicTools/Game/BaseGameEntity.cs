using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Messaging;
using SimpleAI.Utils;
using SimpleAI.Logger;

namespace SimpleAI.Game
{
    public class BaseGameEntity : MonoBehaviour, ITelegramReceiver, IUpdateable
    {
        private int TheID = 0;

        public int ID
        {
            get { return TheID; }
        }

        public BaseGameEntity()
        {
            //TheID = IDAllocator.Instance.GetID();
        }

        ~BaseGameEntity()
        {

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

        public virtual void Process(float dt) { }

        void Awake()
        {
            TheID = IDAllocator.Instance.GetID();
            TinyLogger.Instance.DebugLog(string.Format("$ BaseGameEnity got id: {0}", TheID));
        }

        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);
            EntityManager.Instance.RegisterEntity(this);


            Initialize();
        }

        void OnDestroy()
        {
            if (IDAllocator.IsAlive)
            {
                IDAllocator.Instance.RecycleID(TheID);
            }

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
            Process(dt);
        }

        public virtual bool HandleMessage(Telegram msg) 
        {
            TinyLogger.Instance.DebugLog(string.Format("$ BaseGameEntity handle message {0}", ID));
            return false;
        }
    }
}