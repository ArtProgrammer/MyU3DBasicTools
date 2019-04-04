//
// Des: This component used to recognition enemies, alliance, others like them.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent.Defence
{
    public enum RaceTypeEnum
    { 
        Dog,
        Cat,
        Rabit,
        Wolf,
        Nimei,
        None
    }

    public class DefenceSystem
    {
        private static readonly DefenceSystem TheInstance = new DefenceSystem();

        private DefenceSystem() { } 

        static DefenceSystem() { }

        public static DefenceSystem Instance
        { 
            get
            {
                return TheInstance;
            }
        }

        private Dictionary<int, int> RaceRelations =
            new Dictionary<int, int>();

        private Dictionary<int, int> CampRelations =
            new Dictionary<int, int>();

        public Dictionary<int, RaceTypeEnum> RaceTypeMap =
            new Dictionary<int, RaceTypeEnum>();

        public void Initialize()
        {
            ;
        }

        public void Load()
        {
            // RaceRelations come from config.
            // CampRelations come from both config and database.
        }

        public RaceTypeEnum Int2RaceType(int val)
        { 
            if (RaceTypeMap.ContainsKey(val))
            {
                return RaceTypeMap[val];
            }

            return RaceTypeEnum.None;
        }

        public int RaceType2Int(RaceTypeEnum type)
        { 
            if (RaceTypeMap.ContainsValue(type))
            { 
                //return RaceTypeMap.
            }

            return 0;
        }

        /// <summary>
        /// Ises the enemy race.
        /// </summary>
        /// <returns><c>true</c>, 
        /// if enemy race was ised, <c>false</c> otherwise.</returns>
        /// <param name="srcType">Source type.</param>
        /// <param name="dstType">Dst type.</param>
        public bool IsEnemyRace(int srcType, int dstType)
        {
            return srcType != dstType;
        }

        public bool IsEnemyRace(RaceTypeEnum src, RaceTypeEnum dst)
        {
            return src != dst;
        }

        /// <summary>
        /// Ises the enemy.
        /// </summary>
        /// <returns><c>true</c>, 
        /// if enemy was ised, <c>false</c> otherwise.</returns>
        /// <param name="srcType">Source type.</param>
        /// <param name="dstType">Dst type.</param>
        public static bool IsEnemy(int srcType, int dstType)
        {
            return srcType != dstType;
        }

        /// <summary>
        /// Ises the alliance.
        /// </summary>
        /// <returns><c>true</c>, 
        /// if alliance was ised, <c>false</c> otherwise.</returns>
        /// <param name="srcCamp">Source camp.</param>
        /// <param name="dstCamp">Dst camp.</param>
        public static bool IsAlliance(int srcCamp, int dstCamp)
        {
            return srcCamp == dstCamp;
        }

        /// <summary>
        /// Ises the dangerous.
        /// </summary>
        /// <returns><c>true</c>, 
        /// if dangerous was ised, <c>false</c> otherwise.</returns>
        /// <param name="srcType">Source type.</param>
        /// <param name="dstType">Dst type.</param>
        public static bool IsDangerous(int srcType, int dstType)
        {
            return srcType != dstType;
        }
    }
}