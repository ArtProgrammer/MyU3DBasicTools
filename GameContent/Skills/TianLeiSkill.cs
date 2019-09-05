using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.UsableItem;

namespace GameContent.Skill
{
    public class TianLeiSkill : BaseSkill
    {
        public override void Spawned()
        {

        }

        public override void Despawned()
        {

        }

        public TianLeiSkill()
        {
            KindType = SkillKindType.TianLei;
        }

        public override BaseSkill Clone()
        {
            return new TianLeiSkill();
        }

        public override void AddBufID(int id)
        {
            BuffIDList.Add(id);
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
                    Debug.Log("$$$ use tian lei skill");
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