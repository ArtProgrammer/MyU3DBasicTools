using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Utils;
using SimpleAI.Game;
using SimpleAI.Logger;
using SimpleAI.Spatial;
using GameContent.UsableItem;

namespace GameContent.Skill
{
    public class SkillData : BaseUsableData
    {
        public bool AttackEnemy;
        public bool IsItemSkill;
        public bool IsRoleSkill;
        public bool IsLinked;
        public int SkillID;
        public int Level;
        public int CostType;
        public int Cost;
        public int EffectID;
        public int BuffID;
        public float UseRange;
        public float EffectRange;
        public float DelayTime;
        public float LastTime;
        public float CooldownTime;
        public float Speed;

        public SkillData()
        {
            Catalog = UsableCatalog.Skill;
        }
    }

    public class BuffData
    {
        public float Delay;
        public float LifeTime;
        public float Duration;
        public float MaxTimes;
    }

    /// <summary>
    /// SKill mananger.
    /// Handle skills data load and clean.
    /// Handle runtime skill instances process.
    /// </summary>
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

        private Dictionary<int, SkillData> SkillDataPool =
            new Dictionary<int, SkillData>();

        /// <summary>
        /// Loads the skills' config data.
        /// </summary>
        public void LoadSkills()
        {
            {
                SkillData sd = new SkillData();
                sd.ID = 10001; // IDAllocator.Instance.GetID();
                sd.SkillID = 1;
                sd.BuffID = 1;
                sd.Level = 1;
                sd.Cost = 10;
                sd.CostType = 1;

                sd.AttackEnemy = true;

                sd.EffectID = 100001;
                sd.EffectRange = 20.0f;
                sd.UseRange = 10.0f;
                sd.IsItemSkill = false;
                sd.IsRoleSkill = true;
                sd.Speed = 0.0f;
                sd.DelayTime = 0.0f;
                sd.LastTime = 0.0f;
                sd.CooldownTime = 1;

                sd.Icon = Application.dataPath + "/Images/PureImages/Board-Games.png";

                SkillDataPool.Add(sd.ID, sd);
            }

            {
                SkillData sd = new SkillData();
                sd.ID = 10002; // IDAllocator.Instance.GetID();
                sd.SkillID = 2;
                sd.BuffID = 2;
                sd.Level = 1;
                sd.Cost = 10;
                sd.CostType = 1;

                sd.AttackEnemy = true;

                sd.EffectID = 100001;
                sd.EffectRange = 20.0f;
                sd.UseRange = 10.0f;
                sd.IsItemSkill = false;
                sd.IsRoleSkill = true;
                sd.Speed = 0.0f;
                sd.DelayTime = 0.0f;
                sd.LastTime = 0.0f;
                sd.CooldownTime = 1;

                sd.Icon = Application.dataPath + "/Images/PureImages/Board-Games.png";

                SkillDataPool.Add(sd.ID, sd);
            }
        }

        public SkillData GetSkillData(int id)
        { 
            if (SkillDataPool.ContainsKey(id))
            {
                return SkillDataPool[id];
            }

            return null;
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
            GameLogicSupvisor.Instance.Register(this);
        }

        public void OnUpdate(float dt)
        {
            UpdateSkills(dt);
        }

        public void FindCurSkillTargets(ref Vector3 position, float range)
        { 
            SkillBound.center = position;

            SkillRangeSize.x = range;
            SkillRangeSize.y = range;
            SkillRangeSize.z = range;
            SkillBound.size = SkillRangeSize;

            Targets.Clear();

            SpatialManager.Instance.QueryRange(ref SkillBound,
                Targets);

            for (int i = 0; i < Targets.Count; i++)
            { 
                if (!SkillBound.Contains(Targets[i].Position))
                {
                    Targets.RemoveAt(i);
                    i--;
                }
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

        public BaseSkill SpawnSkill(int id)
        { 
            if (id == 1)
            {
                return new RiseupSkill();
            }
            else
            {
                return new SuckBloodSkill();
            }
        }

        public BaseBuff<BaseGameEntity> SpawnBuff(int id)
        {
            //return new BaseBuff<T>();
            if (id == 1)
            {
                return new RotBuff();
            }
            else if (id == 2)
            {
                return new SuckBloodBuff();
            }

            return null;
        }

        public bool TryUseSkill(int id, BaseGameEntity target, BaseGameEntity src)
        {
            if (!System.Object.ReferenceEquals(null, target))
            {
                var data = GetSkillData(id);
                var skill = SpawnSkill(data.SkillID);
                skill.AddBuff(SpawnBuff(data.BuffID));
                skill.Range = data.EffectRange;
                skill.Delay = data.DelayTime;
                skill.Duration = data.LastTime;

                skill.SetOwner(src);
                skill.Use(target);
                return true;
            }

            return false;
        }

        public bool TryUseSkill(int id, ref Vector3 position, BaseGameEntity src)
        {
            var data = GetSkillData(id);
            var skill = SpawnSkill(data.SkillID);

            if (!System.Object.ReferenceEquals(null, skill))
            {
                //var buff = SpawnBuff(data.BuffID);
                //skill.AddBuff(buff);
                skill.AddBufID(data.BuffID);
                skill.Range = data.EffectRange;
                skill.Delay = data.DelayTime;
                skill.Duration = data.LastTime;

                skill.SetOwner(src);
                FindCurSkillTargets(ref position, skill.Range);

                TinyLogger.Instance.DebugLog("$$$ skill targets count: " + Targets.Count.ToString());

                for (int i = 0; i < Targets.Count; ++i)
                {
                    if (data.AttackEnemy && 
                        System.Object.ReferenceEquals(skill.GetOwner(), Targets[i]))
                    {
                        continue;
                    }
                    
                    skill.Use((BaseGameEntity)Targets[i]);
                }

                return true;
            }

            return false;
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