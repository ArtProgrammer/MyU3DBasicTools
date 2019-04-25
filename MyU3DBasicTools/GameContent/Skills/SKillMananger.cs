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

        public Vector3 Position2Use = Vector3.zero;

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
            UpdateSkills(dt);
        }

        public void FindCurSkillTargets(ref Vector3 position, float range)
        { 
            if (!System.Object.ReferenceEquals(CurSkill2Use, null))
            {
                SkillBound.center = position;

                SkillRangeSize.x = range;
                SkillRangeSize.y = range;
                SkillRangeSize.z = range;
                SkillBound.size = SkillRangeSize;

                Targets.Clear();

                SpatialManager.Instance.QueryRange(ref SkillBound,
                    Targets);
            }
        }

        public bool TryUseSkill(int uniqueID)
        {
            TinyLogger.Instance.DebugLog(
                string.Format("$ try use skill with uniqueID {0}",
                uniqueID)
            );

            if (CanBeUsed(uniqueID))
            {
                CurSkill2Use = new RiseupSkill();
                FindCurSkillTargets(ref Position2Use, CurSkill2Use.Range);

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

        public bool TryUseSkill(BaseSkill skill, BaseGameEntity target)
        {
            if (!System.Object.ReferenceEquals(null, skill) &&
                !System.Object.ReferenceEquals(null, target))
            {
                skill.Use(target);
                return true;
            }

            return false;
        }

        public bool TryUseSkill(BaseSkill skill, ref Vector3 position)
        {
            if (!System.Object.ReferenceEquals(null, skill))
            {
                FindCurSkillTargets(ref position, skill.Range);

                TinyLogger.Instance.DebugLog("$$$ skill targets count: " + Targets.Count.ToString());

                for (int i = 0; i < Targets.Count; ++i)
                {
                    //if (!System.Object.ReferenceEquals(skill.GetOwner(), Targets[i]))
                        skill.Use((BaseGameEntity)Targets[i]);
                }

                return true;
            }

            return false;
        }

        public bool TryUseSkill(BaseSkill skill)
        { 
            if (!System.Object.ReferenceEquals(null, skill))
            {
                CurSkill2Use = skill;
                FindCurSkillTargets(ref Position2Use, skill.Range);

                for (int i = 0; i < Targets.Count; ++i)
                {
                    //CurSkill2Use.Use((BaseGameEntity)Targets[i]);
                    EntityManager.Instance.PlayerEntity.UseSkill(CurSkill2Use, 
                        (BaseGameEntity)Targets[i]);
                    CurSkill2Use.Use((BaseGameEntity)Targets[i]);
                }

                return true;
            }

            return false;
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

        public void UpdateSkills(float dt)
        { 
            for (int i = 0; i < Skills.Count; i++)
            {
                Skills[i].Process(dt);
            }
        }

        public bool CanBeUsed(int uniqueID)
        {
            return true;
        }
    }
}