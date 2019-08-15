using System;
using System.Collections.Generic;

using SimpleAI.Game;

namespace GameContent
{
    public class Trigger_Respawning : Trigger<BaseGameEntity>
    {
        private int NumUpdatesBetweenRespawns;

        private int NumUpdatesRemainingUntilRespawn;

        public void Deactivate()
        {
            IsActive = false;
            NumUpdatesRemainingUntilRespawn = NumUpdatesBetweenRespawns;
        }

        public Trigger_Respawning()
        {
            NumUpdatesBetweenRespawns = 0;
            NumUpdatesRemainingUntilRespawn = 0;
        }

        public override void Process(float dt)
        {
            if ((--NumUpdatesRemainingUntilRespawn <= 0) &&
                !IsActive)
            {
                IsActive = true;
            }
        }

        public void SetRespawnDelay(int numticks)
        {
            NumUpdatesBetweenRespawns = numticks;
        }
    }
}
