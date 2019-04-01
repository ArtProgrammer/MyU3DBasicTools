using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using SimpleAI.Logger;
using SimpleAI.Messaging;
using SimpleAI.Game;
using GameContent.Agents;
using SimpleAI.Timer;

namespace GameContent.SimAgent
{
    public class SimWood : BaseGameEntity
    {
        private NavMeshAgent NMAgent = null;

        public Transform Home = null;

        public Transform Food = null;

        private Regulator BrainReg = null;

        public SimWoodBrain Brain = null;

        [SerializeField]
        private SimSensor<BaseGameEntity> TheSensor = null;

        private Regulator SensorReg = null;

        public int FoodNeed = 5;

        public int FoodCount = 0;

        private Transform SelfTrans = null;

        public void SetDestination(Vector3 pos)
        {
            if (NMAgent)
                NMAgent.destination = pos;
        }

        //public void GetPosition(ref Vector3 val)
        //{
        //    val = transform.position;
        //}

        public override void Initialize()
        {
            TinyLogger.Instance.DebugLog("$$$ SimWood register to MessagingSystem");

            NMAgent = GetComponent<NavMeshAgent>();

            SelfTrans = GetComponent<Transform>();

            BrainReg = new Regulator(15.0f);

            Brain = new SimWoodBrain(this, 0);

            SensorReg = new Regulator(10f);

            TheSensor = new SimSensor<BaseGameEntity>(this);

            TheSensor.Initialize();
        }

        /// <summary>
        /// Finish this instance.
        /// </summary>
        public override void Finish()
        {
        }

        public override void Process(float dt)
        {
            if (BrainReg.IsReady())
            {
                Brain.Process();
            }

            if (SensorReg.IsReady())
            {
                TheSensor.Process(dt);
            }

        }

        public override bool HandleMessage(Telegram msg)
        {
            if (Brain.HandleMessage(msg)) return true;

            TinyLogger.Instance.DebugLog("$$$ sin wood back msg got!");

            SimWoodBackMsg swbmsg = (SimWoodBackMsg)msg;

            NMAgent.destination = swbmsg.Pos;

            return true;
        }

        public void OnDrawGizmos()
        {
            if (!System.Object.ReferenceEquals(TheSensor, null))
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(TheSensor.SearchBound.center,
                    TheSensor.SearchBound.size);
            }
        }
    }
}