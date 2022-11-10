using System;
using System.Collections.Generic;
using Pattern.Observer;


public static class EventDispatcher
{
    static Dictionary<EventName, List<Action<EventName, object>>> callbackDict = new Dictionary<EventName, List<Action<EventName, object>>>();

    static List<Action<EventName, object>> GetCallbacksList(EventName EventName)
    {
        if (!callbackDict.ContainsKey(EventName))
        {
            callbackDict.Add(EventName, new List<Action<EventName, object>>());
        }

        return callbackDict[EventName];
    }

    public static void Dispatch(EventName EventName, object data = null)
    {
        var callbacks = GetCallbacksList(EventName);
        var cloneCallbacks = new Action<EventName, object>[callbacks.Count];
        callbacks.CopyTo(cloneCallbacks);

        for (var i = 0; i < cloneCallbacks.Length; i++)
        {
            cloneCallbacks[i]?.Invoke(EventName, data);
        }
    }

    public static void AddListener(EventName EventName, System.Action<EventName, object> callback)
    {
        var callbacks = GetCallbacksList(EventName);
        if (callbacks.Contains(callback))
        {
            return;
        }
        callbacks.Add(callback);
    }

    public static void RemoveListener(EventName EventName, System.Action<EventName, object> callback)
    {
        var callbacks = GetCallbacksList(EventName);
        if (callbacks.Contains(callback))
        {
            callbacks.Remove(callback);
        }
    }

    public static void RemoveAllListeners()
    {
        callbackDict.Clear();
    }
}
