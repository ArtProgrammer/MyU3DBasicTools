using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using GameContent.Skill;
using SimpleAI.Logger;

namespace GameContent.Interaction
{
    public class UIIneteractor : MonoBehaviour
    {
        StringBuilder TextBuf = new StringBuilder();

        // Start is called before the first frame update
        void Start()
        {

        }

        public void UseSkill(int index)
        {
            // get the skill info: uniqueid on the given index.
            int uniqueid = 10001;
            bool result = SKillMananger.Instance.TryUseSkill(uniqueid);

            if (!result)
            {
                TextBuf.Clear();
                TextBuf.Append("$ failed to use skill with uniqueid: ");
                TextBuf.Append(uniqueid);
                TinyLogger.Instance.DebugLog(TextBuf.ToString());
            }
        }

        public void UseItem(int index)
        {

        }
    }
}