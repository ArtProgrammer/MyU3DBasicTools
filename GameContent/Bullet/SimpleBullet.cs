using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.SimAgent;

namespace GameContent
{
    public class SimpleBullet : MonoBehaviour, IUpdateable
    {
        public int ID = 0;

        public float Speed = 0;

        public Vector3 Dir = Vector3.zero;

        private bool IsActive = false;

        public int OwnerID = 0;

        public void Go()
        {
            IsActive = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);
        }

        public void OnUpdate(float dt)
        {
            if (IsActive)
            {
                transform.position += Dir * Speed * dt;
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.transform)
            {
                var trans = collision.transform;
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
                        Debug.Log("$$$ bullet collider");
                        IsActive = false;
                        gameObject.SetActive(false);
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