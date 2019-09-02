using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using SimpleAI.Logger;
using SimpleAI.Messaging;
using SimpleAI.Game;
using SimpleAI.PoolSystem;
using GameContent.Agents;
using SimpleAI.Timer;
using GameContent.UI;
using GameContent.Interaction;

namespace GameContent.SimAgent
{
    public class SimWood : BaseGameEntity, IPoolableComponent
    {
        //public NavMeshAgent NMAgent = null;
        public bool IsPlayerCtrl = false;

        public Transform Home = null;

        public Transform Food = null;

        //public Transform WeaponPoint = null;

        //public Transform Body = null;

        private Regulator BrainReg = null;

        public SimWoodBrain Brain = null;

        public WeaponSystem WeaponSys = null;

        public int[] SkillIDs = new int[3];

        private Regulator SensorReg = null;

        private Regulator TargetSysReg = null;

        private Regulator WeaponSelectionReg = new Regulator(100.0f);

        public GameObject HealthbarPrefab = null;

        public GameObject SimHealthBag = null;

        public HealthBar Health = null;

        public Transform HeadupTrans = null;

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
                        //PrefabPoolingSystem.Instance.Despawn(gameObject);
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

        public Vector3 GetDestination()
        {
            if (NMAgent)
            {
                return NMAgent.destination;
            }

            return Vector3.zero;
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

            FoodCostReg = new Regulator(0.01f);

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

            if (HealthbarPrefab)
            {
                SimHealthBag = Instantiate(HealthbarPrefab) as GameObject;

                if (!SimHealthBag) return;

                SimHealthBag.transform.SetParent(UIManager.Instance.HealthBarPanel);

                Health = SimHealthBag.GetComponent<HealthBar>();
                if (!System.Object.ReferenceEquals(null, Health))
                {
                    Health.Target = HeadupTrans;
                    OnXueChanged += OnXueChange;
                }
            }
        }

        public void OnXueChange(int val)
        {
            if (val <= 0 && IsActive)
            {
                PrefabPoolingSystem.Instance.Despawn(gameObject);
            }

            Health.ChangePercent((float)val / MaxXue);
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
            if (IsActive)
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

                    if (!IsPlayerCtrl)
                    {
                        Target = TargetSys.CurTarget;
                    }                    
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
                WeaponSys.Use(CurTarget);
            }
        }

        public void UseWeapon(Vector3 pos)
        {
            if (System.Object.ReferenceEquals(null, WeaponPoint))
            {
                WeaponPoint = transform;
            }

            if (!System.Object.ReferenceEquals(null, WeaponSys))
            {
                WeaponSys.Use(pos);
            }
        }

        public virtual void Spawned()
        {
            Xue = 100;
            IsActive = true;
            if (Health)
            {
                Health.gameObject.SetActive(true);
            }            
        }

        public virtual void Despawned()
        {
            IsActive = false;

            if (Health)
            {
                Health.gameObject.SetActive(false);
            }            
        }
    }
}
