using System;
using System.Collections.Generic;

using SimpleAI.Game;
using GameContent.SimAgent;

namespace GameContent
{
    class Trigger_WeaponGiver : Trigger_Respawning<SimWood>
    {
        private int WeaponCfgID;

        public override void Try(SimWood target)
        {
            if (IsActive &&
                IsTouchingTrigger(target.Position, target.CollideRadius))
            {
                target.WeaponSys.AddWeapon(WeaponCfgID);

                IsActive = false;
            }
        }
    }
}
