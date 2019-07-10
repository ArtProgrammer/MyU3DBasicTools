using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent.Skill
{
    public class SkillProducer
    {
        //private static SkillProducer TheInstance = new SkillProducer();

        //private SkillProducer() { }

        //static SkillProducer() { }

        //public static SkillProducer Instance
        //{ 
        //    get
        //    {
        //        return TheInstance;
        //    }
        //}

        private Dictionary<SkillKindType, BaseSkill> SkillPrototypes =
            new Dictionary<SkillKindType, BaseSkill>();

        private Dictionary<BuffKindType, BaseBuff> BuffPrototypes =
            new Dictionary<BuffKindType, BaseBuff>();

        public void AddPrototype(BaseSkill skill)
        { 
            if (!System.Object.ReferenceEquals(null, skill))
            { 
                if (!SkillPrototypes.ContainsKey(skill.KindType))
                {
                    SkillPrototypes.Add(skill.KindType, skill);
                }
            }
        }

        public BaseSkill FindAndClone(SkillKindType skt)
        { 
            if (SkillPrototypes.ContainsKey(skt))
            {
                return SkillPrototypes[skt].Clone();
            }

            return NullSkill.Instance.Clone();
        }

        public void CleanSkillPrototypes()
        {
            SkillPrototypes.Clear();
        }

        public void AddPrototype(BaseBuff buff)
        {
            if (!System.Object.ReferenceEquals(null, buff))
            {
                if (!BuffPrototypes.ContainsKey(buff.KindType))
                {
                    BuffPrototypes.Add(buff.KindType, buff);
                }
            }
        }

        public BaseBuff FindAndClone(BuffKindType skt)
        {
            if (BuffPrototypes.ContainsKey(skt))
            {
                return BuffPrototypes[skt].Clone();
            }

            return NullBuff.Instance.Clone();
        }

        public void CleanBuffPrototypes()
        {
            SkillPrototypes.Clear();
        }

        public void CleanPrototypes()
        {
            CleanBuffPrototypes();
            CleanSkillPrototypes();
        }
    }
}