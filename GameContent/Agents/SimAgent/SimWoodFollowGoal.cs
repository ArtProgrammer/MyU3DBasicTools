﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;
using SimpleAI.Game;
using SimpleAI.Messaging;

using GameContent;

namespace GameContent.SimAgent
{
    public class SimWoodFollowGoal : Goal<SimWood>
    {
        private float Distance = 2.0f;

        private Vector3 TargetPos = Vector3.zero;

        public SimWoodFollowGoal(SimWood p, int type, BaseGameEntity target, float distance) :
            base(p, type)
        {
            Owner.Target = target;
            Distance = distance;
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
            if (Owner.Target)
            {
                Owner.Target.GetPosition(ref TargetPos);
                Owner.SetDestination(TargetPos);
                Owner.StartMove();
            }            
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            if (Status == GoalStatus.Active)
            {
                if (Owner.IsTargetLost ||
                    (CombatHolder.Instance.IsCloseEnough(Owner, Owner.Target, Distance)))
                {
                    Owner.StopMove();
                    Status = GoalStatus.Complete;
                }
                else
                {
                    Owner.Target.GetPosition(ref TargetPos);
                    Owner.SetDestination(TargetPos);
                    Status = GoalStatus.Active;
                }
            }            

            return Status;
        }

        public override void Terminate()
        {            
            Owner.StopMove();
        }

        public override bool HandleMessage(Telegram msg)
        {
            Debug.Log("$ msg arrived");
            if (msg.MsgType == 101)
            {
                Debug.Log("$Got msg 101");
                Status = GoalStatus.Failed;

                return true;
            }
            
            return false;
        }
    }
}
