using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Utils;
using SimpleAI.Game;
using SimpleAI.Logger;
using SimpleAI.Spatial;

namespace GameContent.Skill
{
    public class SKillMananger : SingletonAsComponent<SKillMananger>, 
        IUpdateable
    {
        private Dictionary<int, BaseSkill> Skills =
            new Dictionary<int, BaseSkill>();

        private Vector3 Position2Use = Vector3.zero;

        private Vector3 SkillRangeSize = Vector3.zero;

        private Bounds SkillBound;

        List<SpatialFruitNode> Targets =
                new List<SpatialFruitNode>();

        private void LoadSkills()
        { 
        }

        public static SKillMananger Instance
        { 
            get
            {
                return (SKillMananger)InsideInstance;
            }
        }

        public BaseSkill CurSkill2Use = null;

        // Start is called before the first frame update
        void Start()
        {
            LoadSkills();

            GameLogicSupvisor.Instance.Register(this);
        }

        public void OnUpdate(float dt)
        {
            UpdateSkills();
        }

        public void FindCurSkillTargets(ref Vector3 position)
        { 
            if (!System.Object.ReferenceEquals(CurSkill2Use, null))
            {
                SkillBound.center = position;

                SkillRangeSize.x = CurSkill2Use.Range;
                SkillRangeSize.y = CurSkill2Use.Range;
                SkillRangeSize.z = CurSkill2Use.Range;
                SkillBound.size = SkillRangeSize;

                SpatialManager.Instance.QueryRange(ref SkillBound,
                    Targets);
            }
        }

        public bool TryUseSkill(int uniqueID)
        {
            TinyLogger.Instance.DebugLog(
                string.Format("$ try use skill with kind {0}",
                uniqueID)
            );

            if (CanBeUsed(uniqueID))
            {
                CurSkill2Use = new RiseupSkill();
                FindCurSkillTargets(ref Position2Use);

                for (int i = 0; i < Targets.Count; ++i)
                {
                    CurSkill2Use.Use((BaseGameEntity)Targets[i]);
                }

                //CurSkill2Use.Use(Targets);

                return true;
            }
            else {
                return false;
            }

        }

        public void TriggerSkillOnUI(int index)
        { 
        }

        public void AddSkill(BaseSkill skill)
        {
            //if (skill.Delay > float.Epsilon)
            //{ 
            //}
            Skills.Add(skill.UniqueID, skill);
        }

        public void RemoveSkill(BaseSkill skill)
        { 
        }

        public void RemoveSkill(int uniqueID)
        { 

        }

        public void UpdateSkills()
        { 
        }

        public bool CanBeUsed(int uniqueID)
        {
            return true;
        }
    }
}