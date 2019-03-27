using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Utils;

public interface IUpdateable
{
    void OnUpdate(float dt);
}

public class GameLogicSupvisor : SingletonAsComponent<GameLogicSupvisor>
{
    public static GameLogicSupvisor Instance
    {
        get { return ((GameLogicSupvisor)InsideInstance); }
    }

    GameLogicSupvisor()
    {
        Debug.Log("$ GameLogicSupervisor ctor");
    }

    private void Start()
    {
        Debug.Log("$ GameLogicSupervisor start");
    }

    List<IUpdateable> UpdateableObjects = new List<IUpdateable>();

    public void Register(IUpdateable obj)
    { 
        if (!Instance.UpdateableObjects.Contains(obj))
        {
            Instance.UpdateableObjects.Add(obj);
        }
    }

    public void Unregister(IUpdateable obj)
    { 
        if (Instance.UpdateableObjects.Contains(obj))
        {
            Instance.UpdateableObjects.Remove(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        for (int i = 0; i < Instance.UpdateableObjects.Count; i++)
        {
            Instance.UpdateableObjects[i].OnUpdate(dt);
        }
    }
}
