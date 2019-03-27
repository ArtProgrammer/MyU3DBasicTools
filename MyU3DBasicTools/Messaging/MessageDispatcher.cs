using System;
using System.Collections.Generic;

using SimpleAI.Game;

namespace SimpleAI.Messaging
{
    public sealed class MessageDispatcher
    {
        private static readonly MessageDispatcher TheInstance = new MessageDispatcher();

        private float ImmediatelyMsgTime = float.Epsilon;

        private SortedSet<Telegram> PriorityQ = new SortedSet<Telegram>(new TelegramCompare());

        static MessageDispatcher() { }

        private MessageDispatcher() { }

        ~MessageDispatcher()
        {
            PriorityQ.Clear();
        }

        public static MessageDispatcher Instance
        {
            get {
                return TheInstance;
            }
        }

        /// <summary>
        /// Send the message to the receiver.
        /// </summary>
        /// <param name="receiver">The reference of the entity receiving the message.</param>
        /// <param name="msg">The message reference to be sent.</param>
        private void Discharge(BaseGameEntity receiver, ref Telegram msg)
        {
            if (System.Object.ReferenceEquals(receiver, null) && 
                !receiver.HandleMessage(ref msg)) {
                // receiver can not handle this message.
            }
        }

        /// <summary>
        /// Uniform method adding the messages to be dispatched.
        /// Dispatch the no delay messages immediately,
        /// and record the delayed messages into a queue sorted
        /// by they delayed time with latest in the front.
        /// </summary>
        /// <param name="delay">The time to delay dispatching the message.</param>
        /// <param name="senderID">The id of sender entity.</param>
        /// <param name="receiverID">The id of the target entity to receive the message.</param>
        /// <param name="msgType">The type of the message for identifying.</param>
        public void DispatchMsg(float delay, int senderID, int receiverID, int msgType)
        {
            BaseGameEntity receiver = EntityManager.Instance.GetEntityByID(receiverID);

            if (System.Object.ReferenceEquals(receiver, null)) {
                // no receiver with id == receiverID
                return;
            }

            Telegram telegram = new Telegram(0, senderID, receiverID, msgType);

            if (delay < ImmediatelyMsgTime) {
                Discharge(receiver, ref telegram);
            }
            else {
                float currentTime = 0.0f;
                telegram.DispatchTime = currentTime + delay;

                // insert in the queue.
                PriorityQ.Add(telegram);
            }
        }

        /// <summary>
        /// Check and dispatch the delayed messages that dispatch time arrived.
        /// </summary>
        void DispatchDelayedMessages()
        {
            // TODO: get the current time.
            float currentTime = 0.0f;

            while (PriorityQ.Count > 0 && 
                PriorityQ.Min.DispatchTime > currentTime &&
                PriorityQ.Min.DispatchTime > 0.0) {

                Telegram telegram = PriorityQ.Min;

                BaseGameEntity receiver = EntityManager.Instance.GetEntityByID(telegram.ReceiverID);
                Discharge(receiver, ref telegram);

                PriorityQ.Remove(PriorityQ.Min);
            }

        }
    }
}
