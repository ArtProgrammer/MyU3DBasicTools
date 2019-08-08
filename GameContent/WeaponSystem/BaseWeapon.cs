using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Utils;
using SimpleAI.Timer;

namespace GameContent
{
    public class BaseWeapon : MonoBehaviour, IUpdateable
    {
        public int ID = 0;

        public int BulletID = 0;

        public Transform Bullet = null;

        public float Rate = 1.0f / 2;

        public Regulator UseRate;

        public float Range = 10.0f;

        // Start is called before the first frame update
        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);

            UseRate = new Regulator(Rate);
        }

        // Update is called once per frame
        public virtual void OnUpdate(float dt)
        {
            
        }

        public virtual void Use(BaseGameEntity target)
        {
            if (target && UseRate.IsReady())
            {
                //Bullet.Go();
                Transform bul = Instantiate<Transform>(Bullet);
                if (bul)
                {
                    var bullet = bul.GetComponent<SimpleBullet>();

                    if (bullet)
                    {
                        bul.position = transform.position;
                        bullet.Dir = target.Position - transform.position;
                        bullet.Dir.Normalize();
                        bullet.Go();
                    }
                }                
            }
        }

        public virtual void Use(Vector3 pos)
        {

        }

        public virtual void Use(Vector3 pos, Vector3 dir)
        {

        }

        public void Destroy()
        {
            GameLogicSupvisor.Instance.Unregister(this);
        }
    }
}