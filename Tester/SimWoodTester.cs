using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Logger;
using SimpleAI.Messaging;
using GameContent.SimAgent;

public class SimWoodTester : MonoBehaviour
{
    BaseGameEntity BGEntitu = null;

    public BaseGameEntity Target = null;

    // Start is called before the first frame update
    void Start()
    {
        BGEntitu = GetComponent<BaseGameEntity>();
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

            //SimWoodBackMsg sbmsg = new SimWoodBackMsg(0.0f, BGEntitu.ID, Target.ID, 1);
            var msg = new SimWoodBackMsg();
            msg.Pos = randPos;
            MessageDispatcher.Instance.DispatchMsg(0.0f, BGEntitu.ID, Target.ID, 1, msg);
        }
    }
}
