using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Spatial;
using SimpleAI.Logger;
using SimpleAI.Game;

namespace GameContent.Skill
{
    public class RiseupSkill : BaseSkill
    {
        public Vector3 Pos = Vector3.zero;

        private Vector3 VRange = Vector3.zero;

        private Vector3 Offset = Vector3.zero;

        //public float Range = 100f;

        public RiseupSkill()
        {
            Range = 100.0f;
        }

        public override void Use(List<BaseGameEntity> targets)
        {
            for (int i = 0; i < targets.Count; ++i)
            {
                if (!System.Object.ReferenceEquals(targets[i], null))
                {
                    Offset = targets[i].transform.position;
                    //target.GetPosition(ref Offset);
                    Offset.y += 5.0f;
                    //target.SetPosition(ref Offset);
                    targets[i].transform.Translate(Offset);

                    //TinyLogger.Instance.DebugLog(string.Format("$ new y {0}",
                    //Offset.y));
                }
            }
        }

        public override void Use(BaseGameEntity target)
        {
            //Bounds bound = new Bounds();
            //bound.center = Pos;
            //VRange.x = Range;
            //VRange.y = Range;
            //VRange.z = Range;
            //bound.size = VRange;

            //List<SpatialFruitNode> targets =
            //    new List<SpatialFruitNode>();
            //SpatialManager.Instance.QueryRange(ref bound, targets);

            //for (int i = 0; i < targets.Count; ++i)
            //{
            //    var trans = targets[i].transform;
            //    Offset.x = trans.position.x;
            //    Offset.z = trans.position.z;
            //    Offset.y = trans.position.y + 10.0f;

            //    targets[i].transform.Translate(Offset);
            //}

            //TinyLogger.Instance.DebugLog(string.Format("$found {0} targets",
                //targets.Count));

            if (!System.Object.ReferenceEquals(target, null))
            {
                Offset = target.transform.position;
                //target.GetPosition(ref Offset);
                Offset.y += 5.0f;
                //target.SetPosition(ref Offset);
                target.transform.Translate(Offset);

                //TinyLogger.Instance.DebugLog(string.Format("$ new y {0}",
                    //Offset.y));
            }
        }
    }
}