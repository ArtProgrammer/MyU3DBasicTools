using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;

namespace SimpleAI.Game
{
    public class RoleMovement : MonoBehaviour, IUpdateable
    {
        //Transform Target;
        BaseGameEntity Target = null;

        private Vector3 Offset = Vector3.zero;

        private bool NeedMove = false;

        public float MoveSpeed = 10.0f;

        // Start is called before the first frame update
        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);
        }

        // Update is called once per frame
        public void OnUpdate(float dt)
        {
            HandleInputs(dt);
        }

        void HandleInputs(float dt)
        { 
            if (Input.GetKey(KeyCode.W))
            {
                Offset.z += dt * MoveSpeed;
                NeedMove = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                Offset.z -= dt * MoveSpeed;
                NeedMove = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                Offset.x -= dt * MoveSpeed;
                NeedMove = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                Offset.x += dt * MoveSpeed;
                NeedMove = true;
            }

            if (NeedMove)
            {
                transform.position += Offset;
                Offset.x = Offset.y = Offset.z = 0.0f;
                NeedMove = false;
            }
        }

        void OnDestroy()
        {
            if (GameLogicSupvisor.IsAlive)
                GameLogicSupvisor.Instance.Unregister(this);
        }
    }
}