using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.UsableItem;

namespace GameContent.Item
{
    public class BaseFulu : BaseItem
    {
        public int Fali2Stand = 100;

        public int MaxFali = 1000;

        public int CurFali = 0;

        public bool IsStart = false;

        public bool CanStop = false;

        protected bool IsFull = true;

        public override void Initialize()
        {

        }

        public override void Process(float dt)
        {
            if (IsStart)
            { 

            }
        }

        public bool IsPowerFull()
        {
            return IsFull;
        }

        public virtual void SupplyPower(int num)
        {
            if (IsFull)
            {
                return;
            }

            CurFali += num;

            if (CurFali >= MaxFali)
            {
                CurFali = MaxFali;
                IsFull = true;
            }
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

        public override void Use(BaseGameEntity target, BaseGameEntity dst = null)
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