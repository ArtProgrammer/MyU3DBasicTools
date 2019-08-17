using System;
using System.Collections.Generic;

using SimpleAI.Game;
using GameContent.SimAgent;

namespace GameContent
{
    public class TriggerHealthGiver : Trigger_Respawning<SimWood>
    {
        public int HealthGiven;

        public TriggerHealthGiver() { }

        public override void Try(SimWood target)
        {
            if (IsActive && 
                IsTouchingTrigger(target.Position, target.CollideRadius))
            {
                //
                target.Xue += HealthGiven;
                IsActive = false;
            }
        }
    }
}
