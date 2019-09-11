using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.PoolSystem;
using GameContent.UsableItem;
using SimpleAI.Game;


namespace GameContent.Item
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseItem : IBaseUsableItem, IPoolableComponent, IPrototype<BaseItem>
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

        private ItemKind TheKind = 0;

        public ItemKind Kind
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

        public ItemTargetType TargetType
        {
            get;set;
        }

        public GameObject TheObject = null;        

        public BaseItem()
        {
            TargetType = ItemTargetType.PlayerSelf;
        }

        public virtual void Spawned()
        {

        }

        public virtual void Despawned()
        {

        }

        public virtual BaseItem Clone()
        {
            return new BaseItem();
        }

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

        public virtual void Use(BaseGameEntity target, BaseGameEntity dst = null)
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

        public virtual void Use(int id, Vector3 pos)
        {
                       
        }
    }
}