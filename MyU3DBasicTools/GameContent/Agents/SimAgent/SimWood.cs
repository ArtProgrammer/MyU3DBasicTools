using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using SimpleAI.Logger;
using SimpleAI.Messaging;
using SimpleAI.Game;

namespace GameContent.SimAgent
{
    public class SimWood : BaseGameEntity
    {
        private NavMeshAgent NMAgent = null;

        public Transform Home = null;

        public Transform Food = null;

        public SimWoodBrain Brain = null;

        public int FoodNeed = 5;

        public int FoodCount = 0;

        public void SetDestination(Vector3 pos)
        {
            if (NMAgent)
                NMAgent.destination = pos;
        }

        public override void Initialize()
        {
            TinyLogger.Instance.DebugLog("$$$ SimWood register to MessagingSystem");

            NMAgent = GetComponent<NavMeshAgent>();

            Brain = new SimWoodBrain(this, 0);
        }

        /// <summary>
        /// Finish this instance.
        /// </summary>
        public override void Finish()
        {
        }

        public override void Process(float dt)
        {
            if (!System.Object.ReferenceEquals(Brain, null)) 
                Brain.Process();
        }

        public override bool HandleMessage(Telegram msg)
        {
            TinyLogger.Instance.DebugLog("$$$ sin wood back msg got!");

            SimWoodBackMsg swbmsg = (SimWoodBackMsg)msg;

            NMAgent.destination = swbmsg.Pos;

            return true;
        }
    }
}