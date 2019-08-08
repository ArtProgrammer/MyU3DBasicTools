using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.UsableItem;


namespace GameContent.Skill
{
    public class NullSkill : BaseSkill
    {
        private static NullSkill TheInstance = new NullSkill();

        public static NullSkill Instance
        {
            get
            {
                return TheInstance;
            }
        }

        private NullSkill() { }
        static NullSkill() { }

        public override void Spawned() { }

        public override void Despawned() { }

        public override void SetOwner(BaseGameEntity entity) { }

        public override void AddBufID(int id) { }

        public override BaseSkill Clone()
        {
            return this;
        }

        public override void Process(float dt) { }

        public override void Finish() { }

        //
        public override void TakeEffect() { }

        public override void Use(Vector3 pos) { }

        public override void Use(BaseGameEntity target) {}

        public override void Use(List<BaseGameEntity> targets) { }

        public override void Use(IBaseUsableItem target) { }

        public override void Use(List<IBaseUsableItem> targets) { }
    }
}