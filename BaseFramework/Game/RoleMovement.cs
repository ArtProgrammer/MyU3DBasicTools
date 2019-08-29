using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
using GameContent.SimAgent;

namespace SimpleAI.Game
{
    public class RoleMovement : MonoBehaviour, IUpdateable
    {
        //Transform Target;
        BaseGameEntity Target = null;

        SimWood Wood = null;

        private Vector3 Offset = Vector3.zero;

        private bool NeedMove = false;

        public float MoveSpeed = 10.0f;

        // Start is called before the first frame update
        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);

            Wood = GetComponent<SimWood>();
        }

        // Update is called once per frame
        public void OnUpdate(float dt)
        {
            HandleInputs(dt);
        }

        public Action OnSpaceClick;

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

            if (Input.GetKey(KeyCode.Space))
            {
                if (OnSpaceClick != null)
                {
                    OnSpaceClick();
                }
            }

            if (NeedMove)
            {
                transform.position += Offset;
                //Wood.SetDestination(Wood.GetDestination() + Offset);
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