using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameContent.UsableItem;
using SimpleAI.Game;

namespace GameContent.Item
{
    public class BaseItem : IBaseUsableItem
    {
        private int TheID = 0;

        public int ID
        {
            set
            {
                TheID = value;
            }
            get
            {
                return TheID;
            }
        }

        private int TheCount = 1;

        public int Count
        {
            set
            {
                TheCount = value;
            }
            get
            {
                return TheCount;
            }
        }

        private int TheKind = 0;

        public int Kind
        {
            set
            {
                TheKind = value;
            }
            get
            {
                return TheKind;
            }
        }

        //void Start()
        //{
        //    Initialize();
        //}

        //public void OnUpdate(float dt)
        //{
        //    Process(dt);
        //}

        //void OnDestroy()
        //{
        //    Destroy();
        //}

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public virtual void Initialize()
        {

        }

        public virtual void Process(float dt)
        { 

        }

        public virtual void Destroy()
        { 

        }

        public virtual void TakeEffect()
        { 

        }

        public virtual void Use(Vector3 pos)
        { 

        }

        public virtual void Use(BaseGameEntity target)
        { 

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