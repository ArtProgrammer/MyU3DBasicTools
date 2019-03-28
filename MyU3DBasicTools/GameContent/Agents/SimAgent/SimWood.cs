using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using SimpleAI.Logger;
using SimpleAI.OfficerMessage;
using SimpleAI.Messaging;
using SimpleAI.Game;

namespace GameContent.SimAgent
{
    public class SimWood : BaseGameEntity
    {
        private NavMeshAgent NMAgent = null;

        public override void Initialize()
        {
            TinyLogger.Instance.DebugLog("$$$ SimWood register to MessagingSystem");
            MessagingSystem.Instance.AttachListener(typeof(SimWoodBackMsg),
                this.HandleOfficerMessage);

            NMAgent = GetComponent<NavMeshAgent>();
        }

        /// <summary>
        /// Finish this instance.
        /// </summary>
        public override void Finish()
        {
            if (MessagingSystem.IsAlive)
            {
                MessagingSystem.Instance.DetachListener(typeof(SimWoodBackMsg),
                    this.HandleOfficerMessage);
            }
        }

        public override void Process(float dt)
        {
            ;
        }

        public override bool HandleMessage(ref Telegram msg)
        {
            return false;
        }

        public bool HandleOfficerMessage(OfficerBaseMessage msg)
        {
            TinyLogger.Instance.DebugLog("$$$ sin wood back msg got!");

            {
                SimWoodBackMsg bmsg = (SimWoodBackMsg)msg;
                NMAgent.destination = bmsg.Pos;

                TinyLogger.Instance.DebugLog(
                    string.Format("$$$ des pos: {0}, {1}", bmsg.Pos.x, bmsg.Pos.z)
                    );
            }

            return true;
        }
    }
}