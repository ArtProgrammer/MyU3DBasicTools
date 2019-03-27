using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Logger;

namespace SimpleAI.OfficerMessage
{
    public class TestMessageListener : MonoBehaviour
    {
        bool HandleMyCustomMessage(OfficerBaseMessage msg)
        {
            MyCustomMessage castMsg = msg as MyCustomMessage;
            TinyLogger.Instance.DebugLog(string.Format("$ Got the message !" +
                "{0}, {1}", castMsg._intValue, castMsg._floatValue));

            return true;
        }

        // Start is called before the first frame update
        void Start()
        {
            MessagingSystem.Instance.AttachListener(typeof(MyCustomMessage),
                this.HandleMyCustomMessage);
        }

        private void OnDestroy()
        {
            if (MessagingSystem.IsAlive)
            {
                MessagingSystem.Instance.DetachListener(typeof(MyCustomMessage),
                    this.HandleMyCustomMessage);
            }
        }
    }
}
