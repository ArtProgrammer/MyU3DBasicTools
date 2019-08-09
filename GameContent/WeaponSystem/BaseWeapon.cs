using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Utils;
using SimpleAI.Timer;
using SimpleAI.PoolSystem;

namespace GameContent
{
    public class BaseWeapon : MonoBehaviour, IUpdateable
    {
        public int ID = 0;

        public int BulletCfgID = 0;        

        public float Rate = 1.0f * 0.5f;

        public Regulator UseRate;

        public float Range = 10.0f;

        public BaseWeapon(WeaponConfig data)
        {
            ID = IDAllocator.Instance.GetID();
            BulletCfgID = data.BulletCfgID;
            Rate = data.Rate;
            Range = data.Range;
            //data.Prefab;
            
            UseRate = new Regulator(Rate);
        }

        // Start is called before the first frame update
        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);
        }

        // Update is called once per frame
        public virtual void OnUpdate(float dt)
        {
            
        }

        public virtual void Use(BaseGameEntity target)
        {
            if (!System.Object.ReferenceEquals(null, target))
            {
                Use(target.Position);
            }
        }

        public virtual void Use(Vector3 pos)
        {
            var dir = pos - transform.position;
            dir.Normalize();
            Use(pos, dir);
        }

        public virtual void Use(Vector3 pos, Vector3 dir)
        {
            if (UseRate.IsReady())
            {
                var prefab = BulletCfgMgr.Instance.GetPrefabByID(BulletCfgID);
                if (prefab)
                {
                    var gameObj = PrefabPoolingSystem.Instance.Spawn(prefab);
                    var bulletData = BulletCfgMgr.Instance.GetDataByID(BulletCfgID);
                    if (gameObj &&
                        !System.Object.ReferenceEquals(null, bulletData))
                    {
                        Transform bul = gameObj.transform;
                        if (bul)
                        {
                            var bullet = bul.GetComponent<SimpleBullet>();
                            bullet.ID = IDAllocator.Instance.GetID();
                            bullet.Speed = bulletData.Speed;

                            if (bullet)
                            {
                                bul.position = transform.position;
                                bullet.Dir = dir;
                                bullet.Go();
                            }
                        }
                    }
                }
            }
        }

        public void Destroy()
        {
            GameLogicSupvisor.Instance.Unregister(this);
        }
    }
}