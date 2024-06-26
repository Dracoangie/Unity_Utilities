using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Dictionary<string, List<Subscriber>> listeners = new Dictionary<string, List<Subscriber>>();


    // Raise event through different method signatures
    // ############################################################

    public static void Raise(string id)
    {
        Raise(id,null, null, true);
    }

    public static void Raise(string id, Component sender)
    {
        Raise(id, sender, null, true);
    }

    public static void Raise(string id,object data)
    {
        Raise(id,null, data, true);
    }

    public static void Raise(string id, bool remainInSubscribed)
    {
        Raise(id, null, null, remainInSubscribed);
    }


    public static void Raise(string id, Component sender,object data)
    {
        Raise(id, sender, data, true);
    }
    public static void Raise(string id, Component sender, bool remainInSubscribed)
    {
        Raise(id, sender, null, remainInSubscribed);
    }

    public static void Raise(string id, object data, bool remainInSubscribed)
    {
        Raise(id, null, data, remainInSubscribed);
    }

    public static void Raise(string id, Component sender, object data, bool remainInSubscribed)
    {
        if (string.IsNullOrEmpty(id)) return;

        List<Subscriber> subscribersList;
        if (listeners.TryGetValue(id, out subscribersList))
        {
            foreach (Subscriber subscriber in new List<Subscriber>(subscribersList))
            {
                subscriber?.HandleEvent(sender, data, remainInSubscribed);
            }
        }
    }


    // Manage Listeners
    // ############################################################

    public static void RegisterListener(Subscriber listener)
    {
        if (listener == null || string.IsNullOrEmpty(listener.id)) return;

        if (!listeners.ContainsKey(listener.id))
        {
            listeners[listener.id] = new List<Subscriber>();
        }
        if (!listeners[listener.id].Contains(listener))
        {
            listeners[listener.id].Add(listener);
        }
    }

    public static void UnregisterListener(Subscriber listener)
    {
        if (listener == null || string.IsNullOrEmpty(listener.id)) return;

        if (listeners.ContainsKey(listener.id))
        {
            listeners[listener.id].Remove(listener);
            if (listeners[listener.id].Count == 0)
            {
                listeners.Remove(listener.id);
            }
        }
    }


}