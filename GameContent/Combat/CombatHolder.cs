﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class CombatHolder
    {
        private static CombatHolder TheInstance = new CombatHolder();

        public static CombatHolder Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public bool IsCloseEnough(BaseGameEntity src, BaseGameEntity dst)
        {
            Vector3 dis = src.Position - dst.Position;

            return dis.sqrMagnitude < src.CollideRadius * src.CollideRadius;
        }

        public bool IsCloseEnough(BaseGameEntity src, BaseGameEntity dst,
            float distance)
        {
            Vector3 dis = src.Position - dst.Position;
            return dis.sqrMagnitude < distance * distance;
        }

        public bool IsCloseEnough(BaseGameEntity src, Vector3 pos)
        {
            Vector3 dis = src.Position - pos;

            return dis.sqrMagnitude < src.CollideRadius * src.CollideRadius;
        }

        public bool IsInAttackRange(BaseGameEntity src, BaseGameEntity dst)
        {
            Vector3 dis = src.Position - dst.Position;

            return dis.sqrMagnitude < src.AttackRadius * src.AttackRadius;
        }

        public bool IsInAttackRange(BaseGameEntity src, Vector3 pos)
        {
            Vector3 dis = src.Position - pos;

            return dis.sqrMagnitude < src.AttackRadius * src.AttackRadius;
        }

        Vector3 Src2Dst = Vector3.zero;
        public bool IsSecondInFOVOfFirst(Vector3 srcPos,
            Vector3 facing, Vector3 dstPos, float fov)
        {
            Src2Dst = dstPos - srcPos;
            Src2Dst.Normalize();

            return Vector3.Dot(facing, Src2Dst) >= Mathf.Cos(fov * 0.5f);
        }

        public bool IsLOSOkay(Vector3 srcPos, Vector3 dstPos)
        {
            return true;
        }
    }
}
