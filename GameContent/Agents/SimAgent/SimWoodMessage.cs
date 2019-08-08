using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.OfficerMessage;
using SimpleAI.Messaging;

namespace GameContent.SimAgent
{

    // factory DP?
    public class SimWoodMessage : Telegram
    {
        public SimWoodMessage(float dispatchtime, int senderid, int receiverid, int msgtype) :
            base(dispatchtime, senderid, receiverid, msgtype)
        {

        }

        public SimWoodMessage() : base()
        { }
    }

    public class SimWoodBackMsg : SimWoodMessage
    {
        public Vector3 Pos = Vector3.zero;

        public SimWoodBackMsg(float dispatchtime, int senderid, int receiverid, int msgtype) :
            base(dispatchtime, senderid, receiverid, msgtype)
        {

        }

        public SimWoodBackMsg() : base()
        { 

        }
    }
}