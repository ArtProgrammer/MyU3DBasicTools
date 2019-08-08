using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace  GameContent
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
            return false;
        }

        public bool IsCloseEnough(BaseGameEntity src, Vector3 pos)
        {
            return false;
        }

        public bool IsInAttackRange(BaseGameEntity src, BaseGameEntity pos)
        {
            return false;
        }
    }
}
