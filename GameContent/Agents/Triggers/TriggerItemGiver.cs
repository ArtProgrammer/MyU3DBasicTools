using System;
using System.Collections.Generic;

using SimpleAI.Game;

namespace GameContent
{
    class TriggerItemGiver : TriggerLimitedLifetime<BaseGameEntity>
    {
        public int ItemID;

        public TriggerItemGiver(float lifeTime) :
            base(lifeTime)
        {

        }

        public override void Try(BaseGameEntity target)
        {
            if (IsActive &&
                IsTouchingTrigger(target.Position, target.CollideRadius))
            {

            }
        }
    }
}
