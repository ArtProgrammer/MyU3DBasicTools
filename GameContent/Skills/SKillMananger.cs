using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Utils;
using SimpleAI.Game;
using SimpleAI.Logger;
using SimpleAI.Spatial;
using SimpleAI.PoolSystem;
using GameContent.UsableItem;

namespace GameContent.Skill
{
    public enum SkillKindType
    { 
        SuckXue,
        SuckFali,
        ZhaoShen,
        QuGui,
        ZhenYa,
        JuFu,
        TianLei,
        JingGuang,
        None
    }

    public enum BuffKindType
    { 
        SuckXue,
        ZhenYa,
        TianLei,
        LeiJi,
        JingGuang,
        None
    }
    
    public enum SkillTargetType
    {
        PlayerSelf,
        TargetBody,
        Place,
        Direction
    }

    public class SkillData : BaseUsableData
    {
        public bool AttackEnemy;
        public bool IsItemSkill;
        public bool IsRoleSkill;
        public bool IsLinked;
        public int KindType;
        public int Level;
        public int CostType;
        public int Cost;
        public int EffectID;
        public int BuffID;

        public int EffectObjectID;

        public SkillTargetType TargetType = SkillTargetType.PlayerSelf;

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

        public int EffectObjectID;

        public int KindType;

        public string EffectName;
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

        public SkillProducer SkillMaker = new SkillProducer();

        private SkillPoolMananger SKMgr = new SkillPoolMananger();

        private BuffPoolManager BPMgr = new BuffPoolManager();

        /// <summary>
        /// Loads the skills' config data.
        /// </summary>
        public void LoadSkills()
        {
            {
                SkillData sd = new SkillData();
                sd.ID = 10001; // IDAllocator.Instance.GetID();
                sd.KindType = (int)SkillKindType.ZhenYa;
                sd.BuffID = (int)BuffKindType.ZhenYa;
                sd.Level = 1;
                sd.Cost = 10;
                sd.CostType = 1;

                sd.TargetType = SkillTargetType.Place;

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

                //sd.Icon = "Board-Games.png";
                sd.IconID = 2;

                SkillDataPool.Add(sd.ID, sd);
            }

            {
                SkillData sd = new SkillData();
                sd.ID = 10002; // IDAllocator.Instance.GetID();
                sd.KindType = (int)SkillKindType.SuckXue;
                sd.BuffID = (int)BuffKindType.SuckXue;
                sd.Level = 1;
                sd.Cost = 10;
                sd.CostType = 1;

                sd.TargetType = SkillTargetType.Place;

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

                //sd.Icon = "Board-Games.png";
                sd.IconID = 2;

                SkillDataPool.Add(sd.ID, sd);
            }

            {
                SkillData sd = new SkillData();
                sd.ID = 10003; // IDAllocator.Instance.GetID();
                sd.KindType = (int)SkillKindType.TianLei;
                sd.BuffID = (int)BuffKindType.TianLei;
                sd.Level = 1;
                sd.Cost = 10;
                sd.CostType = 1;

                sd.TargetType = SkillTargetType.Place;

                sd.AttackEnemy = true;

                sd.EffectID = 100001;
                sd.EffectRange = 30.0f;
                sd.UseRange = 30.0f;
                sd.IsItemSkill = false;
                sd.IsRoleSkill = true;
                sd.Speed = 0.0f;
                sd.DelayTime = 0.0f;
                sd.LastTime = 0.0f;
                sd.CooldownTime = 1;

                //sd.Icon = "Board-Games.png";
                sd.IconID = 2;

                SkillDataPool.Add(sd.ID, sd);
            }

            {
                SkillData sd = new SkillData();
                sd.ID = 10008; // IDAllocator.Instance.GetID();
                sd.KindType = (int)SkillKindType.JingGuang;
                sd.BuffID = (int)BuffKindType.JingGuang;
                sd.Level = 1;
                sd.Cost = 10;
                sd.CostType = 1;

                sd.TargetType = SkillTargetType.PlayerSelf;

                sd.AttackEnemy = true;

                sd.EffectID = 100001;
                sd.EffectRange = 30.0f;
                sd.UseRange = 30.0f;
                sd.IsItemSkill = false;
                sd.IsRoleSkill = true;
                sd.Speed = 0.0f;
                sd.DelayTime = 0.0f;
                sd.LastTime = 0.0f;
                sd.CooldownTime = 1;

                //sd.Icon = "Board-Games.png";
                sd.IconID = 2;

                SkillDataPool.Add(sd.ID, sd);
            }

            // prepare skill pool system.
            SkillMaker.AddPrototype(new SuckBloodSkill());
            SkillMaker.AddPrototype(new RiseupSkill());
            SkillMaker.AddPrototype(new TianLeiSkill());
            SkillMaker.AddPrototype(new JinGuangShenZhou());

            // 
            SKMgr.Prespawn(SkillKindType.SuckXue, 3);
            SKMgr.Prespawn(SkillKindType.ZhenYa, 3);
            SKMgr.Prespawn(SkillKindType.TianLei, 3);
            SKMgr.Prespawn(SkillKindType.TianLei, 1);

            // prepare buff pool system.
            SkillMaker.AddPrototype(new RotBuff());
            SkillMaker.AddPrototype(new SuckBloodBuff());
            SkillMaker.AddPrototype(new TianLeiBuff());
            SkillMaker.AddPrototype(new JinGuangShenZhou_Buff());

            BPMgr.Prespawn(BuffKindType.SuckXue, 3);
            BPMgr.Prespawn(BuffKindType.ZhenYa, 3);
            BPMgr.Prespawn(BuffKindType.TianLei, 3);
            BPMgr.Prespawn(BuffKindType.JingGuang, 1);
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

        public virtual void OnFixedUpdate(float dt)
        {

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

        // 
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

        public BaseSkill SpawnSkill(int kindid)
        {
            return SKMgr.Spawn((SkillKindType)kindid);

            //if (kindid == 1)
            //{
            //    return new RiseupSkill();
            //}
            //else
            //{
            //    return new SuckBloodSkill();
            //}
        }

        public BaseBuff SpawnBuff(int kindType)
        {
            return BPMgr.Spawn((BuffKindType)kindType);

            //if (kindid == 1)
            //{
            //    return new RotBuff();
            //}
            //else if (kindid == 2)
            //{
            //    return new SuckBloodBuff();
            //}

            //return null;
        }

        public bool TryUseSkill(int id, BaseGameEntity target, BaseGameEntity src)
        {
            if (!System.Object.ReferenceEquals(null, target))
            {
                var data = GetSkillData(id);
                var skill = SpawnSkill(data.KindType);
                if (!System.Object.ReferenceEquals(null, target))
                {
                    skill.AddBufID(data.BuffID);
                    //skill.AddBuff(SpawnBuff(data.BuffID));
                    skill.Range = data.EffectRange;
                    skill.Delay = data.DelayTime;
                    skill.Life = data.LastTime;

                    skill.SetOwner(src);
                    skill.Use(target);

                    return true;
                }
            }

            return false;
        }

        public bool TryUseSkill(int id, ref Vector3 position, BaseGameEntity src)
        {
            var data = GetSkillData(id);
            var skill = SpawnSkill(data.KindType);

            if (!System.Object.ReferenceEquals(null, skill))
            {
                skill.AddBufID(data.BuffID);
                skill.Range = data.EffectRange;
                skill.Delay = data.DelayTime;
                skill.Life = data.LastTime;

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

        //public bool TryUseSkill(BaseSkill skill, BaseGameEntity target)
        //{
        //    if (!System.Object.ReferenceEquals(null, skill) &&
        //        !System.Object.ReferenceEquals(null, target))
        //    {
        //        skill.Use(target);
        //        return true;
        //    }

        //    return false;
        //}

        //public bool TryUseSkill(BaseSkill skill, ref Vector3 position)
        //{
        //    if (!System.Object.ReferenceEquals(null, skill))
        //    {
        //        FindCurSkillTargets(ref position, skill.Range);

        //        TinyLogger.Instance.DebugLog("$$$ skill targets count: " + Targets.Count.ToString());

        //        for (int i = 0; i < Targets.Count; ++i)
        //        {
        //            //if (!System.Object.ReferenceEquals(skill.GetOwner(), Targets[i]))
        //                skill.Use((BaseGameEntity)Targets[i]);
        //        }

        //        return true;
        //    }

        //    return false;
        //}

        //public bool TryUseSkill(BaseSkill skill)
        //{ 
        //    if (!System.Object.ReferenceEquals(null, skill))
        //    {
        //        CurSkill2Use = skill;
        //        FindCurSkillTargets(ref Position2Use, skill.Range);

        //        for (int i = 0; i < Targets.Count; ++i)
        //        {
        //            //CurSkill2Use.Use((BaseGameEntity)Targets[i]);
        //            EntityManager.Instance.PlayerEntity.UseSkill(CurSkill2Use, 
        //                (BaseGameEntity)Targets[i]);
        //            CurSkill2Use.Use((BaseGameEntity)Targets[i]);
        //        }

        //        return true;
        //    }

        //    return false;
        //}

        public void TriggerSkillOnUI(int index)
        { 
        }

        public void AddSkill(BaseSkill skill)
        {
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