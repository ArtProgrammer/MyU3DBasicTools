using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent.Item
{
    public interface FuluMakerProcess
    {
        void OnProcess(float dt);
    }

    public class FuluMaker : MonoBehaviour, IUpdateable
    {
        public BaseFulu CurFulu = null;

        public bool IsStarted = false;

        public bool IsSuccess = false;

        public float Time2Make = .0f;

        public float CurTimeLast = .0f;

        public int MaxPower2Supply = 0;

        public int CurPowerSupplied = 0;

        FuluMakerProcess MakerProcessCallback = null;

        public bool SetFulu2Make(int fuluKindID)
        {
            return false;
        }

        public bool StartMake()
        {
            IsStarted = true;
            IsSuccess = false;
            CurTimeLast = .0f;

            return true;
        }

        public void StopMake()
        {
            //if (CurTimeLast < Time2Make)
            //{
            //    IsSuccess = false;
            //}

            IsSuccess = IsTimeEnough();
        }

        public bool IsTimeEnough()
        {
            return !(CurTimeLast < Time2Make);
        }

        public bool IsPowerEnough()
        {
            return !(CurPowerSupplied < MaxPower2Supply);
        }

        public bool SupplyPower(int num)
        {
            if (IsPowerEnough())
            {
                return false;
            }

            CurPowerSupplied += num;
            return true;
        }

        public void Making(float dt)
        {
            if (!System.Object.ReferenceEquals(null, MakerProcessCallback))
            {
                MakerProcessCallback.OnProcess(1.0f * CurPowerSupplied / MaxPower2Supply);
            }

            if (IsPowerEnough())//(IsTimeEnough())
            {
                StopMake();
                return;
            }

            CurTimeLast += dt;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnUpdate(float dt)
        {
            if (IsStarted)
            {
                Making(dt);
            }
        }

        public virtual void OnFixedUpdate(float dt)
        {

        }
    }
}
