using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameContent.UsableItem;
using SimpleAI.Game;

namespace GameContent.Item
{
    public class NullItem : BaseItem
    {
        private static NullItem TheInstance = new NullItem();

        private NullItem() { }

        static NullItem() { }

        public static NullItem Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public override void Spawned()
        {

        }

        public override void Despawned()
        {

        }

        public override BaseItem Clone()
        {
            return Instance;
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public override void Initialize()
        {

        }

        public override void Process(float dt)
        {

        }

        public override void Destroy()
        {

        }

        public override void TakeEffect()
        {

        }

        public override void Use(Vector3 pos)
        {

        }

        public override void Use(BaseGameEntity target)
        {

        }

        public override void Use(List<BaseGameEntity> targets)
        {

        }

        public override void Use(IBaseUsableItem target)
        {

        }

        public override void Use(List<IBaseUsableItem> targets)
        {

        }
    }
}