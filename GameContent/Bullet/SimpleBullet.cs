using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.PoolSystem;
using GameContent.SimAgent;

using DG.Tweening;

namespace GameContent
{
    public class SimpleBullet : MonoBehaviour, IUpdateable, IPoolableComponent
    {
        public int ID = 0;

        private static int IDRecorder = 0;

        public float Speed = 0;

        public float LifeTime = 0;

        public float CurTime = 0;

        public Vector3 Dir = Vector3.zero;

        private bool IsActive = false;

        public int OwnerID = 0;

        public virtual void Go()
        {
            IsActive = true;

            //transform.do
        }

        // Start is called before the first frame update
        void Start()
        {
			Initialize();
		}

        public virtual void Initialize()
		{
			ID = IDRecorder++;
			GameLogicSupvisor.Instance.Register(this);
		}

        public virtual void Process(float dt)
        {
            transform.position += Dir * Speed * dt;

            CurTime += dt;

            if (CurTime >= LifeTime)
            {
                IsActive = false;
                PrefabPoolingSystem.Instance.Despawn(gameObject);
            }            
        }

        public virtual void Spawned()
        {
            IsActive = true;
            CurTime = 0.0f;
        }

        public virtual void Despawned()
        {
            IsActive = false;
            CurTime = 0.0f;
        }

        public virtual void OnUpdate(float dt)
        {
            if (IsActive)
            {
                Process(dt);
            }
        }

        public virtual void OnFixedUpdate(float dt)
        {

        }

        public virtual SimWood GetAffectTarget(Transform trans)
        {
            SimWood target = trans.GetComponent<SimWood>();

            if (!target)
            {
                trans = trans.parent;
                if (trans)
                {
                    target = trans.GetComponent<SimWood>();
                }
            }

            if (target)
            {
                if (target.ID != OwnerID)
                {
                    Debug.Log("$$$ bullet collider" + ID.ToString());

                    return target;
                }
            }

            return null;
        }

        public virtual void HandleTargetEnter(SimWood target)
		{
			TakeEffect(target);
			AfterEffect(target);
		}

        public virtual void HandleTargetStay(SimWood target)
        {

        }

        public virtual void HandleTargetExit(SimWood target)
        {

        }

        public virtual void TakeEffect(SimWood target)
		{
			target.Xue = target.Xue - 10;
		}

        public virtual void AfterEffect(SimWood target)
		{

		}

        //public void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.transform)
        //    {
        //        var trans = collision.transform;
        //        var target = trans.GetComponent<SimWood>();
        //        if (!target)
        //        {
        //            trans = trans.parent;
        //            if (trans)
        //            {
        //                target = trans.GetComponent<SimWood>();
        //            }
        //        }

        //        if (target)
        //        {
        //            if (target.ID != OwnerID)
        //            {
        //                Debug.Log("$$$ OnCollisionEnter bullet collider" + ID.ToString());
        //                //IsActive = false;
        //                //gameObject.SetActive(false);

        //                //PrefabPoolingSystem.Instance.Despawn(gameObject);
        //            }
        //        }
        //    }
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other)
            {
                if (other.transform)
                {
                    var trans = other.transform;
                    var target = trans.GetComponent<SimWood>();
                    if (!target)
                    {
                        trans = trans.parent;
                        if (trans)
                        {
                            target = trans.GetComponent<SimWood>();
                        }
                    }

                    if (target)
                    {
                        if (target.ID != OwnerID)
                        {
                            Debug.Log("$$$ OnTriggerEnter bullet collider" + ID.ToString());

                            HandleTargetEnter(target);

							//PrefabPoolingSystem.Instance.Despawn(gameObject);
                        }
                    }
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other)
            {
                if (other.transform)
                {
                    var trans = other.transform;
                    var target = trans.GetComponent<SimWood>();
                    if (!target)
                    {
                        trans = trans.parent;
                        if (trans)
                        {
                            target = trans.GetComponent<SimWood>();
                        }
                    }

                    if (target)
                    {
                        if (target.ID != OwnerID)
                        {
                            Debug.Log("$$$ OnTriggerEnter bullet collider" + ID.ToString());

                            HandleTargetStay(target);

                            //PrefabPoolingSystem.Instance.Despawn(gameObject);
                        }
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other)
            {
                if (other.transform)
                {
                    var trans = other.transform;
                    var target = trans.GetComponent<SimWood>();
                    if (!target)
                    {
                        trans = trans.parent;
                        if (trans)
                        {
                            target = trans.GetComponent<SimWood>();
                        }
                    }

                    if (target)
                    {
                        if (target.ID != OwnerID)
                        {
                            Debug.Log("$$$ OnTriggerEnter bullet collider" + ID.ToString());

                            HandleTargetExit(target);

                            //PrefabPoolingSystem.Instance.Despawn(gameObject);
                        }
                    }
                }
            }
        }

        public void OnDestroy()
        {
            if (GameLogicSupvisor.IsAlive)
                GameLogicSupvisor.Instance.Unregister(this);
        }
    }
}
