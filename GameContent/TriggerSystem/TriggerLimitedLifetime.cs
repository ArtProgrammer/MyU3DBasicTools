﻿using System;
using System.Collections.Generic;

using SimpleAI.Game;

namespace GameContent
{
    class TriggerLimitedLifetime<T> : Trigger<T>
    {
        public float LifeTime;

        public TriggerLimitedLifetime(float lifeTime)
        {
            LifeTime = lifeTime;
        }

        public override void Process(float dt)
        {
            LifeTime -= dt;
            if (LifeTime < 0.0f)
            {
                RemoveFromGame = true;
            }
        }
    }
}
