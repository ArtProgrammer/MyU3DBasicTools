using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
using SimpleAI.Logger;

namespace GameContent.Skill
{
    public class RotBuff : BaseBuff<BaseGameEntity>
    {
        public float RotSpeed = 60.0f;

        public Vector3 Rotdir = Vector3.zero;

        public Vector3 OldScale = Vector3.zero;

        public override void Attach(BaseGameEntity target)
        {
            base.Attach(target);
            Target.AddBuff(this);
            OnEnter();

            OldScale = target.transform.localScale;
        }

        public override void LoadData()
        {
            LastTime = 6.0f;

            Rotdir.y = RotSpeed;

        }

        public override void OnExit()
        {
            base.OnExit();

            Target.RemoveBuff(this);

            Target.transform.localScale = OldScale;
        }

        public override void TakeEffect(ref float dt)
        {
            //Target.transform.Rotate(Rotdir * dt);
            Rotdir.x = 5.0f;
            Rotdir.y = 5.0f;
            Rotdir.z = 5.0f;
            Target.transform.localScale = Rotdir;
        }
    }
}