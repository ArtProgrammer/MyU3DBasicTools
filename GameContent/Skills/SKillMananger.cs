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
    /// <summary>
    /// 
    /// </summary>
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
        ZhaoHuanNPC,
        ZhaoHuanItem,
        ChangeSpeed,
        None
    }

    /// <summary>
    /// 
    /// </summary>
    public enum BuffKindType
    { 
        SuckXue,
        ZhenYa,
        TianLei,
        LeiJi,
        JingGuang,
        JingHua,
        DingShen,
        JuHun,
        JuYin,
        JuYang,
        ChangeSpeed,
        None
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SkillTargetType
    {
        PlayerSelf,
        TargetBody,
        Place,
        Direction
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SkillAffectType
    {
        Role,
        Item,
        None
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
            // prepare skill pool system.
            SkillMaker.AddPrototype(new SuckBloodSkill());
            SkillMaker.AddPrototype(new RiseupSkill());
            SkillMaker.AddPrototype(new TianLeiSkill());
            SkillMaker.AddPrototype(new JinGuangShenZhou());
            SkillMaker.AddPrototype(new ZhaoHuanSkill());
            SkillMaker.AddPrototype(new ZhaoHuanItemSkill());
            SkillMaker.AddPrototype(new ChangeSpeedSkill());

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
            SkillMaker.AddPrototype(new ChangeSpeedBuff());

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

        public List<SpatialFruitNode> GetTargets()
        {
            return Targets;
        }

        public void ClearTargets()
        {
            Targets.Clear();
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

                return true;
            }
            else
            {
                return false;
            }
        }

        public BaseSkill SpawnSkill(int kindid)
        {
            return SKMgr.Spawn((SkillKindType)kindid);
        }

        public void DespawnSkill(BaseSkill skill)
        {
            if (!System.Object.ReferenceEquals(null, skill))
            {
                SKMgr.Respawn(skill);
            }
        }

        public BaseBuff SpawnBuff(int cfgid)
        {
            var data = ConfigDataMgr.Instance.BuffCfgLoader.GetDataByID(cfgid);
            if (!System.Object.ReferenceEquals(null, data))
            {
                return BPMgr.Spawn((BuffKindType)data.KindType);
            }

            return NullBuff.Instance;
        }

        public void DespawnBuff(BaseBuff buff)
        {
            if (!System.Object.ReferenceEquals(null, buff))
            {
                BPMgr.Respawn(buff);
            }
        }

        public bool TryUseSkill(int id, BaseGameEntity target, BaseGameEntity src)
        {
            if (!System.Object.ReferenceEquals(null, target))
            {
                var data = ConfigDataMgr.Instance.SkillCfgLoader.GetDataByID(id);

                var skill = SpawnSkill(data.Kind);
                if (!System.Object.ReferenceEquals(null, target))
                {
                    skill.AddBufID(data.BuffID);
                    skill.Range = data.EffectRange;
                    skill.Delay = data.DelayTime;
                    skill.Life = data.LastTime;

                    skill.SetOwner(src);
                    skill.Use(target, src);

                    return true;
                }
            }

            return false;
        }

        public bool TryUseSkill(int id, Vector3 position, BaseGameEntity src)
        {
            var data = ConfigDataMgr.Instance.SkillCfgLoader.GetDataByID(id);
            var skill = SpawnSkill(data.Kind);

            skill.AddBufID(data.BuffID);
            skill.Range = data.EffectRange;
            skill.Delay = data.DelayTime;
            skill.Life = data.LastTime;

            skill.SummonID = data.SummonID;

            skill.SetOwner(src);
            skill.Use(0, position);

            return true;
        }

        public bool TryUseSkill(int id, ref Vector3 position, BaseGameEntity src)
        {
            var data = ConfigDataMgr.Instance.SkillCfgLoader.GetDataByID(id);
            var skill = SpawnSkill(data.Kind);

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
                    if (System.Object.ReferenceEquals(skill.GetOwner(), Targets[i])) //data.AttackEnemy && 
                    {
                        continue;
                    }
                    
                    skill.Use((BaseGameEntity)Targets[i], src);
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
