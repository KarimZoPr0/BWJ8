using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEvent : MonoBehaviour
{
    public GameEvent gameEvent;

    public void EventRaiser()
    {
        gameEvent.Raise();
    }
}
