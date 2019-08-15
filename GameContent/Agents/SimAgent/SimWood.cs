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

        //public Transform WeaponPoint = null;

        //public Transform Body = null;

        private Regulator BrainReg = null;

        public SimWoodBrain Brain = null;
        
        private WeaponSystem WeaponSys = null;

        public int[] SkillIDs = new int[3];

        private Regulator SensorReg = null;

        private Regulator TargetSysReg = null;

        private Regulator WeaponSelectionReg = new Regulator(100.0f);

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
            base.Initialize();
            //if (IsPlayerCtrl)
            //{
            //    EntityManager.Instance.PlayerEntity = this;
            //}

            SelfTrans = GetComponent<Transform>();

            BrainReg = new Regulator(15.0f);

            FoodCostReg = new Regulator(0.1f);

            Brain = new SimWoodBrain(this, 0);

            SensorReg = new Regulator(10f);

            TargetSysReg = new Regulator(100.0f);

            TargetSys = new TargetSystem(this);

            //WeaponSys = GetComponent<WeaponSystem>();
            WeaponSys = new WeaponSystem(this);

            if (!System.Object.ReferenceEquals(null, WeaponSys))
            {
                WeaponSys.OnWeaponChanged += OnWeaponChanged;
            }
        }

        public void OnWeaponChanged(float range)
        {
            AttackRadius = range;
        }

        /// <summary>
        /// Finish this instance.
        /// </summary>
        public override void Finish()
        {
            base.Finish();
        }

        public override void Process(float dt)
        {
            base.Process(dt);

            if (!IsPlayerCtrl)
            {
                Brain.Process();

                if (BrainReg.IsReady())
                {
                    Brain.Arbitrate();
                }

                if (WeaponSelectionReg.IsReady())
                {
                    WeaponSys.SelectWeapon();
                }
            }

            if (SensorReg.IsReady())
            {
                TheSensor.Process(dt);
            }

            if (TargetSysReg.IsReady())
            {
                TargetSys.Process(dt);
                Target = TargetSys.CurTarget;
            }

            if (FoodCostReg.IsReady())
            {
                if (FoodCount > 0)
                    FoodCount--;
            }

            // temp codes.
            if (IsPlayerCtrl)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    UseWeapon();
                }
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

        public void UseWeapon()
        {
            if (System.Object.ReferenceEquals(null, WeaponPoint))
            {
                WeaponPoint = transform;
            }

            if (!System.Object.ReferenceEquals(null, WeaponSys))
            {
                WeaponSys.Use(CurTarget, this);
            }
        }
    }
}