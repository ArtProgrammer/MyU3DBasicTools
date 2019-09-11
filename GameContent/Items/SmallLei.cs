using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.PoolSystem;
using SimpleAI.Supervisors;

namespace GameContent.Item
{
    public class SmallLei : BaseItem
    {
        private int Num = 30;

        public SmallLei()
        {
            Kind = ItemKind.SmallLei;
            TargetType = ItemTargetType.Place;
        }

        public override BaseItem Clone()
        {
            return new SmallLei();
        }

        public override void Initialize()
        {

        }

        public override void Process(float dt)
        {

        }

        public override void Destroy()
        {

        }

        public override void TakeEffect()
        {

        }

        public override void Use(BaseGameEntity target, BaseGameEntity dst = null)
        {
            if (!System.Object.ReferenceEquals(target, null))
            {
                target.Xue -= Num;
            }
        }

        public override void Use(Vector3 pos)
        {
            
        }

        public override void Use(int id, Vector3 pos)
        {
            //var go = PrefabsAssetHolder.Instance.GetPrefabByID(id);
            //if (go)
            //{
            //    //PrefabPoolingSystem.Instance.Spawn(go, pos, Quaternion.identity);
            //    GlorySupervisor.Instance.SpawnItem(id, pos);
            //}

            GlorySupervisor.Instance.SpawnItem(id, pos);
        }
    }
}