using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
using SimpleAI.Utils;
using SimpleAI.PoolSystem;
using GameContent.Item;
using GameContent.UsableItem;


namespace GameContent.Skill
{
    public class BaseSkill : IBaseUsableItem, IPoolableComponent, IPrototype<BaseSkill>
    {
        private int TheUniqueID = 0;

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

        private SkillKindType TheKindType = 0;

        public SkillKindType KindType
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

        public int SenderID = 0;

        public int ReceiverID = 0;

        private float DelayTime = 0f;

        public float Delay
        {
            set
            {
                DelayTime = value;
            }
            get
            {
                return DelayTime;
            }
        }

        private float CurTime = 0f;

        private bool IsAlive = true;

        private float LifeTime = 0f;

        public float Life
        { 
            set
            {
                LifeTime = value;
            }
            get
            {
                return LifeTime;
            }
        }

        public float Range
        {
            set;get;
        }

        public string Icon = null;

        public string EffectObject = null;

        private BaseGameEntity Owner = null;

        public List<int> BuffIDList = new List<int>();

        public virtual void Spawned()
        { 

        }

        public virtual void Despawned()
        { 

        }

        public virtual BaseSkill Clone()
        {
            return NullSkill.Instance;
        }

        public virtual void SetOwner(BaseGameEntity entity)
        {
            Owner = entity;
        }

        public virtual void AddBufID(int id)
        {
            BuffIDList.Add(id);
        }

        public BaseGameEntity GetOwner()
        {
            return Owner;
        }

        public BaseSkill()
        {
            TheUniqueID = IDAllocator.Instance.GetID();
            KindType = SkillKindType.None;
        }

        ~BaseSkill()
        {
            IDAllocator.Instance.RecycleID(TheUniqueID);
            TheUniqueID = IDAllocator.Instance.InvalidID;
        }

        public virtual void Process(float dt)
        {
            if (!IsAlive) return;

            CurTime += dt;

            if (CurTime > LifeTime)
            {
                IsAlive = false;
                Despawned();
            }
        }

        public virtual void Finish()
        { 

        }

        //
        public virtual void TakeEffect()
        {

        }

        public virtual void Use(Vector3 pos)
        {

        }

        public virtual void Use(BaseGameEntity target)
        {
            for (int i = 0; i < BuffIDList.Count; i++)
            {
                var buff = SKillMananger.Instance.SpawnBuff(BuffIDList[i]);
                if (!System.Object.ReferenceEquals(null, buff))
                {
                    buff.Attach(target);
                }
            }
        }

        public virtual void Use(List<BaseGameEntity> targets)
        {

        }

        public virtual void Use(IBaseUsableItem target)
        {

        }

        public virtual void Use(List<IBaseUsableItem> targets)
        {

        }
    }
}