using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{

    protected EventManager() { }

    public delegate void ObjectPositionAction(string objectType, Transform objectTransform);
    public static event ObjectPositionAction OnObjectNewPosition;


    public void ObjectNewPosition(string objectType, Transform objectTransform)
    {
        OnObjectNewPosition?.Invoke(objectType, objectTransform);
    }
}