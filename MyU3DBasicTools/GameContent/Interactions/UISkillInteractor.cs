using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
using GameContent.Skill;
using SimpleAI.Logger;
using SimpleAI.Inputs;
using GameContent.Item;
using GameContent.UsableItem;


namespace GameContent.Interaction
{
    public class UISkillInteractor : MonoBehaviour
    {
        UISkillData SkillDataUI = null;

        public Transform TestCircle = null;

        public BaseSkill CurSkill = null;

        public int CurSkillID = -1;

        public int CurItemID = -1;

        public SkillData CurSkillData = null;

        public void UpdateCirclePos(Vector3 pos)
        {
            pos.y += 0.10f;
            TestCircle.position = pos;
        }

        public void CancelItemUse(Vector3 pos)
        {
            if (CurSkillID != -1)
            {
                CurSkillID = -1;
            }

            if (CurItemID != -1)
            {
                CurItemID = -1;
            }
        }

        public void CancelItemUse(Transform trans)
        {
            if (CurSkillID != -1)
            {
                CurSkillID = -1;
            }

            if(CurItemID != -1)
            {
                CurItemID = -1;
            }
        }

        // for aoe skill
        public void FireCurSkill(Vector3 pos)
        {
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

                CurSkillID = -1;
            }

            if (-1 != CurItemID)
            {
                //EntityManager.Instance.PlayerEntity.UseItem(CurItemID, )
            }
        }

        public void FireCurSkillOnTarget(Transform trans)
        {
            if (-1 != CurSkillID)
            {
                if (!System.Object.ReferenceEquals(null, trans))
                {
                    var bge = trans.GetComponent<BaseGameEntity>();
                    if (!System.Object.ReferenceEquals(null, bge))
                    {
                        EntityManager.Instance.PlayerEntity.UseSkill(CurSkillID,
                            bge);
                    }
                }
            }

            if (-1 != CurItemID)
            {
                var bge = trans.GetComponent<BaseGameEntity>();
                if (!System.Object.ReferenceEquals(null, bge))
                {
                    EntityManager.Instance.PlayerEntity.UseItem(CurItemID, bge);
                }
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
            InputKeeper.Instance.OnLeftClickObject += FireCurSkillOnTarget;
            InputKeeper.Instance.OnRightClickPos += CancelItemUse;
            InputKeeper.Instance.OnRightClickObject += CancelItemUse;
        }

        protected void HandleSkillTry(BaseUsableData data)
        {
            SkillData sd = (SkillData)data;

            if (!System.Object.ReferenceEquals(null, sd))
            {
                if (sd.TargetType == SkillTargetType.PlayerSelf)
                {
                    CurSkillID = -1;
                    EntityManager.Instance.PlayerEntity.UseSkill(sd.ID,
                        EntityManager.Instance.PlayerEntity);
                }
                else
                {
                    CurSkillID = sd.ID;
                }
            }
        }

        protected void HandleItemTry(BaseUsableData data)
        {
            ItemData idata = (ItemData)data;

            if (!System.Object.ReferenceEquals(null, idata))
            {
                if (idata.TargetType == ItemTargetType.TargetBody)
                {
                    CurItemID = idata.ID;
                }
                else if (idata.TargetType == ItemTargetType.PlayerSelf)
                {
                    EntityManager.Instance.PlayerEntity.UseItem(data.ID,
                        EntityManager.Instance.PlayerEntity);
                }
            }            
        }

        public void TryItem(int index)
        {
            TinyLogger.Instance.DebugLog("$$$ try skill index" +
                index.ToString());

            var data = SkillDataUI.GetItemByIndex(index);

            if (!System.Object.ReferenceEquals(data, null))
            {
                if (data.Catalog == UsableItem.UsableCatalog.Skill)
                {
                    HandleSkillTry(data);
                }
                else if (data.Catalog == UsableItem.UsableCatalog.Item)
                {
                    HandleItemTry(data);
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