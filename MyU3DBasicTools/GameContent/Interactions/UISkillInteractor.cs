using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
using GameContent.Skill;
using SimpleAI.Logger;
using SimpleAI.Inputs;
using GameContent.Item;

namespace GameContent.Interaction
{
    public class UISkillInteractor : MonoBehaviour
    {
        UISkillData SkillDataUI = null;

        public Transform TestCircle = null;

        public BaseSkill CurSkill = null;

        public int CurSkillID = -1;

        public SkillData CurSkillData = null;

        public void UpdateCirclePos(Vector3 pos)
        {
            pos.y += 0.10f;
            TestCircle.position = pos;
        }

        // for aoe skill
        public void FireCurSkill(Vector3 pos)
        {
            //if (!System.Object.ReferenceEquals(null, CurSkill))
            if (-1 != CurSkillID)
            {
                if (!System.Object.ReferenceEquals(null, TestCircle))
                {
                    CurSkillData = SKillMananger.Instance.GetSkillData(CurSkillID);

                    TestCircle.localScale = new Vector3(CurSkillData.EffectRange / 10.0f,
                        1.0f,
                        CurSkillData.EffectRange / 10.0f);
                }

                EntityManager.Instance.PlayerEntity.UseSkill(CurSkillID,
                    ref pos);

                //CurSkill = null;
                //CurSkillData = null;
                CurSkillID = -1;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        public void LoadContent()
        {
            SkillDataUI = GetComponent<UISkillData>();
        }

        public void Initialize()
        {
            LoadContent();

            InputKeeper.Instance.OnLeftClickPos += UpdateCirclePos;
            InputKeeper.Instance.OnLeftClickPos += FireCurSkill;
        }

        public void TryItem(int index)
        {
            TinyLogger.Instance.DebugLog("$$$ try skill index" +
                index.ToString());
            //SkillData
            var data = SkillDataUI.GetItemByIndex(index);

            if (!System.Object.ReferenceEquals(data, null))
            {
                if (data.Catalog == UsableItem.UsableCatalog.Skill)
                {
                    //TinyLogger.Instance.DebugLog("$$$ try skill UniqueID" +
                    //data.SkillID.ToString());

                    //skill.SetOwner(EntityManager.Instance.PlayerEntity);
                    //EntityManager.Instance.PlayerEntity.UseSkill(skill, null);
                    //SKillMananger.Instance.TryUseSkill(skill.UniqueID);
                    //SKillMananger.Instance.TryUseSkill(skill);

                    //SKillMananger.Instance.CurSkill2Use = skill;
                    //SKillMananger.Instance.Position2Use = Vector3.zero;

                    //if (!System.Object.ReferenceEquals(null, TestCircle))
                    //{
                    //    TestCircle.localScale = 
                    //      new Vector3(skill.Range / 10.0f, 1.0f, skill.Range / 10.0f);
                    //}

                    //CurSkill = skill;
                    CurSkillID = data.ID;

                    //EntityManager.Instance.PlayerEntity.UseSkill(skill, 
                    //ref SKillMananger.Instance.Position2Use);

                    //EntityManager.Instance.PlayerEntity.UseSkill(data.ID, 
                    //ref SKillMananger.Instance.Position2Use);
                }
                else if (data.Catalog == UsableItem.UsableCatalog.Item)
                {
                    //QiItem qiItem = new QiItem();
                    //EntityManager.Instance.PlayerEntity.UseItem(qiItem,
                    //EntityManager.Instance.PlayerEntity);
                    EntityManager.Instance.PlayerEntity.UseItem(data.ID,
                        EntityManager.Instance.PlayerEntity);
                }
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