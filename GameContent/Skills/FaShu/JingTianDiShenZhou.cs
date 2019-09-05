using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.UsableItem;
using SimpleAI.Timer;

namespace GameContent.Skill
{
    public class JingTianDiShenZhou : BaseSkill
    {
        //Regulator EffectReg = new Regulator(30.0f);

        public override void Process(float dt)
        {
            //if (EffectReg.IsReady())
            //{ 

            //}
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

        public override void Use(BaseGameEntity target, BaseGameEntity dst = null)
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