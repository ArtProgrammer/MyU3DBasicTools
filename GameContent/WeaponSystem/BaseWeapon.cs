using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Utils;
using SimpleAI.Timer;
using SimpleAI.PoolSystem;

namespace GameContent
{
    public class BaseWeapon
    {
        public int ID = 0;

        public int BulletCfgID = 0;

        public int OwnerID = 0;

        public float Rate = 10.0f;

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

        public bool IsReady()
        {
            return UseRate.IsReady();
        }

        public virtual float GetDesirability(float distToTarget)
        {
            if (Range < distToTarget)
            {
                return 0.0f;
            }

            return 1.0f;
        }

        public virtual void Use(BaseGameEntity target, BaseGameEntity origin)
        {
            if (!System.Object.ReferenceEquals(null, target))
            {
                Use(target.Body.position, origin);
            }
        }

        public virtual void Use(Vector3 pos, BaseGameEntity origin)
        {
            var dir = pos - origin.WeaponPoint.position;
            dir.Normalize();
            Use(pos, dir, origin);
        }

        public virtual void Use(Vector3 pos, Vector3 dir, BaseGameEntity origin)
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
                        bullet.OwnerID = origin.ID;
                        bullet.Speed = bulletData.Speed;
                        bullet.LifeTime = bulletData.LifeTime;

                        if (bullet)
                        {
                            bul.position = origin.WeaponPoint.position;
                            bullet.Dir = dir;
                            bullet.Go();
                        }
                    }
                }
            }
        }

        public virtual void Use(BaseGameEntity target, Transform origin)
        {
            if (!System.Object.ReferenceEquals(null, target))
            {
                Use(target.Body.position, origin);
            }
        }

        public virtual void Use(Vector3 pos, Transform origin)
        {
            var dir = pos - origin.position;
            dir.Normalize();
            Use(pos, dir, origin);
        }

        public virtual void Use(Vector3 pos, Vector3 dir, Transform origin)
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
                        //bullet.OwnerID = origin.ID;
                        bullet.Speed = bulletData.Speed;
                        bullet.LifeTime = bulletData.LifeTime;

                        if (bullet)
                        {
                            bul.position = origin.position;
                            bullet.Dir = dir;
                            bullet.Go();
                        }
                    }
                }
            }
        }

        public void Destroy()
        {
        }
    }
}
