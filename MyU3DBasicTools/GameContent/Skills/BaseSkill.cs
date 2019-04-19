using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;


namespace GameContent.Skill
{
    public class BaseSkill
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

        public float Range
        {
            set;get;
        }

        public string Icon = null;

        public virtual void Use(BaseGameEntity target)
        {

        }

        public virtual void Use(BaseItem target)
        {

        }

        public virtual void Use(BaseSkill target)
        {

        }

        public virtual void Use(List<BaseGameEntity> targets)
        { 

        }

        public virtual void Use(List<BaseItem> targets)
        {

        }

        public virtual void Use(List<BaseSkill> targets)
        {

        }
    }
}