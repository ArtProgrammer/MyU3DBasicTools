using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;


namespace GameContent.Skill
{
    public class SpeedChanger : BaseSkill
    {
        public float StartTime = 0.0f;

        public float LastTime = 3.0f;

        private float TimeFlyed = 0.0f;

        List<BaseGameEntity> Targets = new List<BaseGameEntity>();

        private bool IsActive = true;

        public SpeedChanger()
        {
            Range = 100.0f;
        }

        public override void Use(List<BaseGameEntity> targets)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                Targets.Add(targets[i]);
                targets[i].NMAgent.speed *= 0.1f;
            }
        }

        public override void Process(float dt)
        {
            if (IsActive)
            {
                TimeFlyed += dt;

                if (TimeFlyed >= LastTime)
                {
                    for (int i = 0; i < Targets.Count; i++)
                    {
                        Targets[i].NMAgent.speed /= 0.1f;
                    }

                    IsActive = false;
                }
            }
        }

        public override void Finish()
        { 

        }
    }
}