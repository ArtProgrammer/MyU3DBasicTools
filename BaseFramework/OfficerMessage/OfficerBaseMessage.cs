using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.OfficerMessage
{
    public delegate bool MessageHandlerDelegate(OfficerBaseMessage message);

    public class OfficerBaseMessage
    {
        public string Name;

        public OfficerBaseMessage()
        {
            Name = this.GetType().Name;
        }
    }
}
