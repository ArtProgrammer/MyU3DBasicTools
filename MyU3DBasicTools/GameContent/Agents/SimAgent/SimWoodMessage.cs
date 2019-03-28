using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.OfficerMessage;

namespace GameContent.SimAgent
{

    // factory DP?
    public class SimWoodMessage : OfficerBaseMessage
    {
        public enum MsgType
        {
            Back,
            None
        }

        public MsgType TheType = MsgType.None;

        public SimWoodMessage()
        {

        }
    }

    public class SimWoodBackMsg : SimWoodMessage
    {
        public readonly Vector3 Pos = Vector3.zero;

        public SimWoodBackMsg(Vector3 pos)
        {
            Pos = pos;
        }
    }
}