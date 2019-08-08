using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.UsableItem;


namespace GameContent.Skill
{
    public class NullBuff : BaseBuff
    {
        private static NullBuff TheInstance = new NullBuff();

        public static NullBuff Instance
        {
            get
            {
                return TheInstance;
            }
        }

        private NullBuff() { }
        static NullBuff() { }

        public override void Spawned() { }

        public override void Despawned() { }

        public override void LoadData()
        {

        }

        public override void Attach(BaseGameEntity target)
        {
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void OnProcess(ref float dt)
        {
        }

        public override void TakeEffect(ref float dt)
        {
        }
    }
}