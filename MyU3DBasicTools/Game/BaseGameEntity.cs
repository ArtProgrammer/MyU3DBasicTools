using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Messaging;
using SimpleAI.Utils;

namespace SimpleAI.Game
{
    public class BaseGameEntity : MonoBehaviour, ITelegramReceiver, IUpdateable
    {
        private int TheID = 0;

        public int ID
        {
            get;
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
            TheID = IDAllocator.Instance.InvalidID;
        }

        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);
            EntityManager.Instance.RegisterEntity(this);


            Initialize();
        }

        void OnDestroy()
        {
            IDAllocator.Instance.RecycleID(TheID);

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

        public virtual bool HandleMessage(ref Telegram msg) 
        {
            return false;
        }
    }
}