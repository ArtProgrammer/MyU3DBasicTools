using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Spatial;
using GameContent.Skill;
using GameContent.Item;

namespace SimpleAI.Supervisors
{
    public class GlorySupervisor : MonoBehaviour
    {
        public void Initialize()
        {
            Reload();
            DontDestroyOnLoad(gameObject);
        }

        public void Reload()
        {
            SKillMananger.Instance.LoadSkills();

            ItemManager.Instance.LoadDatas();

            // load from config
            SpatialManager.Instance.Init(0, 0, 0, 100, 100, 100);
        }

        void Awake()
        {
            Initialize();
        }
    }
}