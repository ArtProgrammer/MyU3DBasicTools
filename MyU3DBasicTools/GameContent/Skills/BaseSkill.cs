using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
using SimpleAI.Utils;
using GameContent.Item;
using GameContent.UsableItem;

namespace GameContent.Skill
{
    public enum SkillTargetType
    { 

    }

    public class BaseSkill : IBaseUsableItem
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

        private int TheKindID = 0;

        private int KindID
        {
            set
            {
                TheKindID = value;
            }
            get
            {
                return TheKindID;
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

        private float DurationTime = 0f;

        public float Duration
        { 
            set
            {
                DurationTime = value;
            }
            get
            {
                return DurationTime;
            }
        }

        public float Range
        {
            set;get;
        }

        public string Icon = null;

        private BaseGameEntity Owner = null;

        public List<BaseBuff<BaseGameEntity>> BuffList =
            new List<BaseBuff<BaseGameEntity>>();

        public List<int> BuffIDList = new List<int>();

        public void AddBuff(BaseBuff<BaseGameEntity> buff)
        {
            BuffList.Add(buff);
        }

        public void SetOwner(BaseGameEntity entity)
        {
            Owner = entity;
        }

        public void AddBufID(int id)
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
        }

        ~BaseSkill()
        {
            IDAllocator.Instance.RecycleID(TheUniqueID);
            TheUniqueID = IDAllocator.Instance.InvalidID;
        }

        public virtual void Process(float dt)
        { 

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