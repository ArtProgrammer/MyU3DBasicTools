using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
using GameContent.Skill;
using GameContent.Item;

namespace GameContent.UsableItem
{
    public interface IPrototype<T>
    {
        T Clone();
    }

    public interface IBaseUsableItem
    {
        void TakeEffect();

        void Use(Vector3 pos);

        void Use(BaseGameEntity target, BaseGameEntity dst = null);

        void Use(List<BaseGameEntity> targets);

        void Use(IBaseUsableItem target);

        void Use(List<IBaseUsableItem> targets);
    }
}