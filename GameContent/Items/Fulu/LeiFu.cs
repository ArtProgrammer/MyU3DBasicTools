using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.Skill;
using GameContent.UsableItem;
using SimpleAI.Utils;

namespace GameContent.Item
{
    public class LeiFu : BaseFulu
    {
        public int SkillID = 10003;

        // 
        public int Cost = 0;

        //
        public int Energy = 100;

        public LeiFu()
        {
            Kind = ItemKind.LeiFu;
            TargetType = ItemTargetType.TargetBody;
        }

        public override BaseItem Clone()
        {
            return new LeiFu();
        }

        public override void Use(BaseGameEntity target, BaseGameEntity dst = null)
        {
            GameObject gb = MineResource.Instance.LoadObjectFromAB<GameObject>(Application.dataPath + "/AssetBundles/fulu",
                "PingAnFu");

            GameObject ins = GameObject.Instantiate(gb) as GameObject;

            ins.transform.SetParent(target.transform);

            if (SkillID != 0)
            {
                SKillMananger.Instance.TryUseSkill(SkillID, target, null);
            }
        }
    }
}