using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.OfficerMessage
{
    public class MyCustomMessage : OfficerBaseMessage
    {
        public readonly int _intValue;

        public readonly float _floatValue;

        public MyCustomMessage(int intval, float floatVal)
        {
            _intValue = intval;
            _floatValue = floatVal;
        }
    }
}
