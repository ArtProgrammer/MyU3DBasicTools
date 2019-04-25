using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
using GameContent.Skill;
using SimpleAI.Logger;
using SimpleAI.Inputs;

namespace GameContent.Interaction
{
    public class UISkillInteractor : MonoBehaviour
    {
        UISkillData SkillData = null;

        public Transform TestCircle = null;

        public BaseSkill CurSkill = null;

        public void UpdateCirclePos(Vector3 pos)
        {
            pos.y += 0.10f;
            TestCircle.position = pos;
        }

        // for aoe skill
        public void FireCurSkill(Vector3 pos)
        {
            if (!System.Object.ReferenceEquals(null, CurSkill))
            {
                if (!System.Object.ReferenceEquals(null, TestCircle))
                {
                    TestCircle.localScale = new Vector3(CurSkill.Range / 10.0f, 
                        1.0f, 
                        CurSkill.Range / 10.0f);
                }

                EntityManager.Instance.PlayerEntity.UseSkill(CurSkill,
                    ref pos);

                //CurSkill = null;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            LoadContent();

            InputKeeper.Instance.OnLeftClickPos += UpdateCirclePos;
            InputKeeper.Instance.OnLeftClickPos += FireCurSkill;
        }

        public void LoadContent()
        {
            SkillData = GetComponent<UISkillData>();
        }

        public void TryItem(int index)
        {
            TinyLogger.Instance.DebugLog("$$$ try skill index" +
                index.ToString());
            //SkillData
            BaseSkill skill = SkillData.GetItemByIndex(index);
            if (!System.Object.ReferenceEquals(skill, null))
            {
                TinyLogger.Instance.DebugLog("$$$ try skill UniqueID" +
                skill.UniqueID.ToString());

                //skill.SetOwner(EntityManager.Instance.PlayerEntity);
                //EntityManager.Instance.PlayerEntity.UseSkill(skill, null);
                //SKillMananger.Instance.TryUseSkill(skill.UniqueID);
                //SKillMananger.Instance.TryUseSkill(skill);

                SKillMananger.Instance.CurSkill2Use = skill;
                //SKillMananger.Instance.Position2Use = Vector3.zero;

                //if (!System.Object.ReferenceEquals(null, TestCircle))
                //{
                //    TestCircle.localScale = new Vector3(skill.Range / 10.0f, 1.0f, skill.Range / 10.0f);
                //}

                CurSkill = skill;

                //EntityManager.Instance.PlayerEntity.UseSkill(skill, 
                    //ref SKillMananger.Instance.Position2Use);
            }
        }

        public void AddItem(int index)
        {

        }

        public void RemoveItem(int index)
        {
        }

        public void SweepItems(int srcIndex, int dstIndex)
        {
        }
    }
}