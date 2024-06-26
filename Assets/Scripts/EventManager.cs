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
        Raise(id,null, null);
    }

    public static void Raise(string id,object data)
    {
        Raise(id,null, data);
    }

    public static void Raise(string id,Component sender)
    {
        Raise(id,sender, null);
    }

    public static void Raise(string id, Component sender, object data)
    {
        if (string.IsNullOrEmpty(id)) return;

        List<Subscriber> subscribersList;
        if (listeners.TryGetValue(id, out subscribersList))
        {
            // Se hace una copia para evitar problemas si la lista es modificada durante la iteración
            foreach (Subscriber subscriber in new List<Subscriber>(subscribersList))
            {
                subscriber?.OnEventRaised(sender, data);
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