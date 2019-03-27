using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Messaging;
using SimpleAI.Utils;

namespace SimpleAI.Game
{
    public class BaseGameEntity : ITelegramReceiver
    {
        private int TheID;

        public int ID
        {
            get;
        }

        public BaseGameEntity()
        {
            TheID = IDAllocator.Instance.GetID();
        }

        ~BaseGameEntity()
        {
            IDAllocator.Instance.RecycleID(TheID);
        }

        public virtual void Process() { }

        public virtual bool HandleMessage(ref Telegram msg) 
        {
            return false;
        }
    }
}