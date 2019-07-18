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
        //public NavMeshAgent NMAgent = null;
        public bool IsPlayerCtrl = false;

        public Transform Home = null;

        public Transform Food = null;

        private Regulator BrainReg = null;

        public SimWoodBrain Brain = null;

        [SerializeField]
        private SimSensor<BaseGameEntity> TheSensor = null;

        private Regulator SensorReg = null;

        public int FoodNeed = 5;

        public int FoodCount
        {
            get
            {
                return CurFoodCount;
            }
            set
            {                
                if (CurFoodCount != value)
                {
                    CurFoodCount = value;

                    if (CurFoodCount <= 0)
                    {
                        MessageDispatcher.Instance.DispatchMsg(0.01f, this.ID, this.ID, 101);
                    }
                }
            }
        }

        public int CurFoodCount = 0;

        protected Regulator FoodCostReg = null;

        private Transform SelfTrans = null;

        public void SetDestination(Vector3 pos)
        {
            if (NMAgent)
                NMAgent.destination = pos;
        }

        public void StopMove()
        {
            if (NMAgent)
                NMAgent.isStopped = true;
        }

        public void StartMove()
        {
            if (NMAgent)
                NMAgent.isStopped = false;
        }

        //public void GetPosition(ref Vector3 val)
        //{
        //    val = transform.position;
        //}

        public override void Initialize()
        {
            //if (IsPlayerCtrl)
            //{
            //    EntityManager.Instance.PlayerEntity = this;
            //}

            //TinyLogger.Instance.DebugLog("$$$ SimWood register to MessagingSystem");

            //NMAgent = GetComponent<NavMeshAgent>();

            SelfTrans = GetComponent<Transform>();

            BrainReg = new Regulator(15.0f);

            FoodCostReg = new Regulator(0.5f);

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
            if (!IsPlayerCtrl && BrainReg.IsReady())
            {
                Brain.Process();
            }

            if (SensorReg.IsReady())
            {
                TheSensor.Process(dt);

                Target = TheSensor.CurTarget;
            }

            if (FoodCostReg.IsReady())
            {
                if (FoodCount > 0)
                    FoodCount--;
            }
        }

        public override bool HandleMessage(Telegram msg)
        {
            if (Brain.HandleMessage(msg)) return true;

            //TinyLogger.Instance.DebugLog("$$$ sin wood back msg got!");

            //SimWoodBackMsg swbmsg = (SimWoodBackMsg)msg;

            //NMAgent.destination = swbmsg.Pos;

            return true;
        }

        //public void OnDrawGizmos()
        //{
        //    if (!System.Object.ReferenceEquals(TheSensor, null))
        //    {
        //        Gizmos.color = Color.blue;
        //        Gizmos.DrawWireCube(TheSensor.SearchBound.center,
        //            TheSensor.SearchBound.size);
        //    }
        //}
    }
}