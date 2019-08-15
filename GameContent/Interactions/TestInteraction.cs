using System;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.Skill;
using SimpleAI.Inputs;

namespace GameContent
{
    class TestInteraction : MonoBehaviour
    {
        private SkillData CurSkillData = null;

        public int CurSkillID = 0;

        void Start()
        {
            InputKeeper.Instance.OnLeftClickPos += UpdateCirclePos;
            InputKeeper.Instance.OnLeftClickPos += FireCurSkill;
            InputKeeper.Instance.OnLeftClickObject += FireCurSkillOnTarget;
            InputKeeper.Instance.OnRightClickPos += CancelItemUse;
            InputKeeper.Instance.OnRightClickObject += CancelItemUse;
        }

        public void UpdateCirclePos(Vector3 pos)
        {

        }

        public void FireCurSkill(Vector3 pos)
        {
            CurSkillData = SKillMananger.Instance.GetSkillData(CurSkillID);

            EntityManager.Instance.PlayerEntity.UseSkill(CurSkillID,
                ref pos);

            CurSkillID = -1;
        }

        public void FireCurSkillOnTarget(Transform trans)
        {

        }

        public void CancelItemUse(Vector3 pos)
        {

        }

        public void CancelItemUse(Transform trans)
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                SKillMananger.Instance.TryUseSkill(10001);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                SKillMananger.Instance.TryUseSkill(10002);
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                SKillMananger.Instance.TryUseSkill(10003);
            }
        }
    }
}
