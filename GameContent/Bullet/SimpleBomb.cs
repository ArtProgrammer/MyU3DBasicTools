using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.PoolSystem;
using SimpleAI.Spatial;
using GameContent.SimAgent;

namespace GameContent
{
    public class SimpleBomb : SimpleBullet
    {
        public float Radius = 5.0f;

        public override void Initialize()
        {
            base.Initialize();

            if (!Radius.Equals(0.0f))
            {
                transform.localScale = new Vector3(Radius, Radius, Radius);
            }
        }

        public override void Process(float dt)
        {

        }

        public override void TakeEffect(SimWood target)
        {            
            if (target)
            {
                target.Xue = target.Xue - 10;
            }
        }

        public override void HandleTargetEnter(SimWood target)
        {
            //base.HandleTargetEnter(target);
            TakeEffect(target);
        }

        public override void HandleTargetStay(SimWood target)
        {

        }

        public override void HandleTargetExit(SimWood target)
        {

        }
    }
}