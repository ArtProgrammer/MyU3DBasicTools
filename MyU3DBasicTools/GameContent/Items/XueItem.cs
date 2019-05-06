﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;

namespace GameContent.Item
{
    public class XueItem : BaseItem
    {
        private int Num = 30;

        public override void Initialize()
        {

        }

        public override void Process(float dt)
        {

        }

        public override void Destroy()
        {

        }

        public override void Use(BaseGameEntity target)
        {
            if (!System.Object.ReferenceEquals(target, null))
            {
                target.Xue += Num;
            }
        }
    }
}