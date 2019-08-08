using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class SimpleBullet : MonoBehaviour, IUpdateable
    {
        public int ID = 0;

        public float Speed = 0;

        public Vector3 Dir = Vector3.zero;

        private bool IsActive = true;

        public int OwnerID = 0;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void OnUpdate(float dt)
        {
            if (IsActive)
            {
                transform.position += Dir * Speed;
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.transform)
            {
                var trans = collision.transform;
                var target = trans.GetComponent<BaseGameEntity>();
                if (!target)
                {
                    trans = trans.parent;
                    if (trans)
                    {
                        target = trans.GetComponent<BaseGameEntity>();
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
    }
}