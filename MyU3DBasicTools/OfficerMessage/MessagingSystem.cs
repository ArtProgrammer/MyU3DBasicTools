using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Logger;
using SimpleAI.Utils;

namespace SimpleAI.OfficerMessage
{
    public class MessagingSystem : SingletonAsComponent<MessagingSystem>
    {
        public static MessagingSystem Instance
        {
            get { return ((MessagingSystem)InsideInstance); }
            set { InsideInstance = value; }
        }

        private Dictionary<string, List<MessageHandlerDelegate>> ListenerDict =
            new Dictionary<string, List<MessageHandlerDelegate>>();

        public bool AttachListener(System.Type type, 
            MessageHandlerDelegate handler)
        {
            if (type == null)
            {
                TinyLogger.Instance.DebugLog("$ MessagingSystem: " + 
                    "AttachListener failed due to no message type specified");
                return false;
            }

            string msgName = type.Name;

            if (!ListenerDict.ContainsKey(msgName))
            {
                ListenerDict.Add(msgName, new List<MessageHandlerDelegate>());
            }

            List<MessageHandlerDelegate> listnerList = 
                ListenerDict[msgName];
            if (listnerList.Contains(handler))
            {
                return false;
            }

            listnerList.Add(handler);
            return true;
        }

        public bool DetachListener(System.Type type, 
            MessageHandlerDelegate handler)
        {
            if (type == null)
            {
                TinyLogger.Instance.DebugLog("$MessagingSystem: " + 
                    "DetachListener failed due to no message type specified");

                return false;
            }

            string msgName = type.Name;

            if (!ListenerDict.ContainsKey(type.Name))
            {
                return false;
            }

            List<MessageHandlerDelegate> listenerList = ListenerDict[msgName];
            if (!listenerList.Contains(handler))
            {
                return false;
            }

            listenerList.Remove(handler);
            return true;
        }

        private Queue<OfficerBaseMessage> MessageQueue = new
            Queue<OfficerBaseMessage>();

        public bool QueueMessage(OfficerBaseMessage msg)
        {
            if (!ListenerDict.ContainsKey(msg.Name))
            {
                return false;
            }

            MessageQueue.Enqueue(msg);
            return true;
        }

        private float MaxQueueProcessingTime = 0.16667f;

        // Update is called once per frame
        void Update()
        {
            float timer = 0.0f;

            while (MessageQueue.Count > 0)
            {
                if (MaxQueueProcessingTime > 0.0f)
                {
                    if (timer > MaxQueueProcessingTime)
                        return;
                }

                OfficerBaseMessage msg = MessageQueue.Dequeue();
                if (!TriggerMessage(msg))
                    TinyLogger.Instance.DebugLog("$ Error when processing " +
                        " message: " + msg.Name);

                if (MaxQueueProcessingTime > 0.0f)
                    timer += Time.deltaTime;
            }
        }

        public bool TriggerMessage(OfficerBaseMessage msg)
        {
            string msgName = msg.Name;
            if (!ListenerDict.ContainsKey(msgName))
            {
                TinyLogger.Instance.DebugLog("$MessagingSytem: Message \"" +
                    msgName + "\" has no listeners!");
                return false;
            }

            List<MessageHandlerDelegate> listenerList = ListenerDict[msgName];

            for (int i = 0; i < listenerList.Count; ++i)
            {
                if (listenerList[i](msg))
                    return true;
            }

            return true;
        }
    }
}
