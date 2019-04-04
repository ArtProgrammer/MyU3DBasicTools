using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Messaging;
using SimpleAI.Utils;
using SimpleAI.Logger;
using SimpleAI.Spatial;
using GameContent.Defence;

namespace SimpleAI.Game
{
    public class BaseGameEntity : SpatialFruitNode, ITelegramReceiver, IUpdateable
    {
        private int TheID = 0;

        public int ID
        {
            get { return TheID; }
        }

        [SerializeField]
        protected int TheRaceSignal = 0;

        public int RaceSignal
        { 
            set
            {
                if (TheRaceSignal != value)
                {
                    TheRaceSignal = value;
                    RaceType = DefenceSystem.Instance.Int2RaceType(TheRaceSignal);
                }
            }
            get
            {
                return TheRaceSignal;
            }
        }

        [SerializeField]
        public RaceTypeEnum RaceType
        {
            set;get;
        }

        [SerializeField]
        protected int TheCampType = 0;

        public int CampType
        { 
            set
            {
                TheCampType = value;
            }
            get
            {
                return TheCampType;
            }
        }

        public BaseGameEntity()
        {
            //TheID = IDAllocator.Instance.GetID();
        }

        ~BaseGameEntity()
        {

        }

        public virtual void LoadData()
        {
            //TheRaceType = 0;
            //TheCampType = 0;
        }

        public virtual void SaveData()
        { 

        }

        public void GetPosition(ref Vector3 val)
        {
            val = transform.position;
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
            TheStart();
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

            HandleDestory();
        }

        public virtual void OnUpdate(float dt)
        {
            TheUpdate();

            Process(dt);
        }

        public virtual bool HandleMessage(Telegram msg) 
        {
            TinyLogger.Instance.DebugLog(string.Format("$ BaseGameEntity handle message {0}", ID));
            return false;
        }
    }
}