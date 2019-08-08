using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.OfficerMessage
{
    public class TestMessageSender : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MessagingSystem.Instance.QueueMessage(
                    new MyCustomMessage(6, 6.666f));
            }
        }
    }
}
