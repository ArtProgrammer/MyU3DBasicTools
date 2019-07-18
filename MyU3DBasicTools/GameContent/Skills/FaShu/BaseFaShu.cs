using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.Skill;
using GameContent.UsableItem;

namespace GameContent.Skill
{
    public class BaseFaShu : BaseSkill
    {
        public override void Spawned()
        {

        }

        public override void Despawned()
        {

        }

        public BaseFaShu()
        {
            KindType = SkillKindType.None;
        }

        public override BaseSkill Clone()
        {
            return new BaseFaShu();
        }

        public override void Process(float dt)
        {

        }

        public override void Finish()
        {

        }

        //
        public override void TakeEffect()
        {

        }

        public override void Use(Vector3 pos)
        {

        }

        public override void Use(BaseGameEntity target)
        {
            for (int i = 0; i < BuffIDList.Count; i++)
            {
                var buff = SKillMananger.Instance.SpawnBuff(BuffIDList[i]);
                if (!System.Object.ReferenceEquals(null, buff))
                {
                    buff.Attach(target);
                }
            }
        }

        public override void Use(List<BaseGameEntity> targets)
        {

        }

        public override void Use(IBaseUsableItem target)
        {

        }

        public override void Use(List<IBaseUsableItem> targets)
        {

        }
    }
}