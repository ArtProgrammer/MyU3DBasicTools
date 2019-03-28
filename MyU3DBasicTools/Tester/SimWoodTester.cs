using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Logger;
using SimpleAI.OfficerMessage;
using GameContent.SimAgent;

public class SimWoodTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var randPos = new Vector3(Random.Range(1, 10),
                    1,
                    Random.Range(1, 10)
                );

            var result = MessagingSystem.Instance.QueueMessage(
                new SimWoodBackMsg(randPos));

            TinyLogger.Instance.DebugLog(string.Format("$$$ sim wood msg tester {0}",
                result));
        }
    }
}
