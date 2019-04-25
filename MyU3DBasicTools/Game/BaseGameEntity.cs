using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using SimpleAI.Messaging;
using SimpleAI.Utils;
using SimpleAI.Logger;
using SimpleAI.Spatial;
using GameContent.Defence;
using GameContent.Skill;

namespace SimpleAI.Game
{
    public class BaseGameEntity : SpatialFruitNode, ITelegramReceiver, IUpdateable
    {
        [SerializeField]
        private int TheID = 0;

        public int ID
        {
            get { return TheID; }
        }

        [SerializeField]
        private int TheXue = 100;

        public int Xue
        { 
            get
            {
                return TheXue;
            }
            set
            {
                TheXue = value;
                if (!System.Object.ReferenceEquals(null, OnXueChanged))
                {
                    OnXueChanged(TheXue);
                }
            }
        }

        Action<int> OnXueChanged;


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

        private List<BaseBuff<BaseGameEntity>> BuffList = 
            new List<BaseBuff<BaseGameEntity>>();

        public void AddBuff(BaseBuff<BaseGameEntity> buff)
        {
            BuffList.Add(buff);
        }

        public void RemoveBuff(BaseBuff<BaseGameEntity> buff)
        {
            BuffList.Remove(buff);
        }

        protected void ProcessBuffs(ref float dt)
        { 
            for (int i = 0; i < BuffList.Count; i++)
            {
                BuffList[i].OnUpdate(dt);
            }
        }

        public void ClearBuffList()
        {
            for (int i = 0; i < BuffList.Count; i++)
            {
                BuffList[i].Despawned();
            }

            BuffList.Clear();
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

        public virtual void Process(float dt) 
        {

        }

        public NavMeshAgent NMAgent = null;

        public virtual void UseSkill(BaseSkill skill, ref Vector3 position)
        {
            skill.SetOwner(this);
            SKillMananger.Instance.TryUseSkill(skill, ref position);
        }

        public virtual void UseSkill(BaseSkill skill, BaseGameEntity target)
        {
            skill.SetOwner(this);
            SKillMananger.Instance.TryUseSkill(skill, target);
        }

        public virtual void UseSkill(int skillid, ref Vector3 pos)
        {
            //skill.SetOwner(this);
        }

        public virtual void UseSkill(int skillid, BaseGameEntity target)
        {
            //skill.SetOwner(this);
        }

        void Awake()
        {
            TheID = IDAllocator.Instance.GetID();
            TinyLogger.Instance.DebugLog(string.Format("$ BaseGameEnity got id: {0}", TheID));

            NMAgent = GetComponent<NavMeshAgent>();
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
            ClearBuffList();

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

            ProcessBuffs(ref dt);

            Process(dt);
        }

        public virtual bool HandleMessage(Telegram msg) 
        {
            TinyLogger.Instance.DebugLog(string.Format("$ BaseGameEntity handle message {0}", ID));
            return false;
        }
    }
}