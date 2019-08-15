using System;
using System.Collections.Generic;
using SimpleAI.Game;

namespace GameContent
{
    public class TriggerSystem<T> where T : Trigger<BaseGameEntity>
    {
        private List<T> TriggerList = new List<T>();

        public List<T> GetTriggers()
        {
            return TriggerList;
        }

        public void Register(T trigger)
        {
            TriggerList.Add(trigger);
        }

        public void UpdateTriggers(float dt)
        {
            int i = 0;
            while (i < TriggerList.Count)
            {
                if (TriggerList[i].RemoveFromGame)
                {
                    TriggerList.RemoveAt(i);
                }
                else
                {
                    TriggerList[i].Process(dt);
                    i++;
                }
            }
        }

        public void TryTriggers(float dt)
        {

        }

        public void Process(float dt)
        {
            UpdateTriggers(dt);
            TryTriggers(dt);
        }

        public void Clear()
        {
            TriggerList.Clear();
        }
    }
}
