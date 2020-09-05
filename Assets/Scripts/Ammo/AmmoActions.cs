using System;
using System.Collections.Generic;
using DG.Tweening;
using Microsoft.Win32;
using UnityEngine;

public static class AmmoActions
{
    public enum Action
    {
        Damage,
        PushBack,
        PullIn,
        Explosion,
        ScreenShake
    }

    public static void UseEvents(Vector3 origin, List<AmmoEvent> ammoEvents)
    {
        foreach (AmmoEvent ammoEvent in ammoEvents)
        {
            UseEvent(origin, ammoEvent);
        }
    }

    public static void UseEvent(Vector3 origin, AmmoEvent ammoEvent)
    {
        switch (ammoEvent.action)
        {
            case Action.Damage:
                DealDamage(origin, ammoEvent);
                break;
            case Action.PushBack:
                PushBack(origin, ammoEvent);
                break;
            case Action.PullIn:
                PullIn(origin, ammoEvent);
                break;
            case Action.Explosion:
                break;
            case Action.ScreenShake:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void DealDamage(Vector3 origin, AmmoEvent ammoEvent)
    {
        foreach (var collider in Physics.OverlapSphere(origin, ammoEvent.range))
        {
            collider.SendMessage("DealDamage", ammoEvent.power);
        }
    }
    
    private static void PullIn(Vector3 origin, AmmoEvent ammoEvent)
    {
        foreach (var collider in  Physics.OverlapSphere(origin, ammoEvent.range))
        {
            if(collider.GetComponent<Rigidbody>())
                collider.transform.GetComponent<Rigidbody>().AddForce((collider.transform.position - origin).normalized * ammoEvent.power *-1);
        }
    }

    private static void PushBack(Vector3 origin, AmmoEvent ammoEvent)
    {
        foreach (var collider in  Physics.OverlapSphere(origin, ammoEvent.range))
        {
            if (collider.GetComponent<Rigidbody>())
            {
                Vector3 distance = collider.transform.position - origin;
                    collider.transform.GetComponent<Rigidbody>().AddForce((ammoEvent.range - distance.magnitude) * ammoEvent.power * (distance.normalized+ Vector3.up * 0.4f));
            }
        }
    }

    private static void Explosion(Vector3 origin, AmmoEvent ammoEvent)
    {
        foreach (var collider in  Physics.OverlapSphere(origin, ammoEvent.range))
        {
            collider.SendMessage("Explosion", SendMessageOptions.DontRequireReceiver);
        }
    }

    private static void ScreenShake(Vector3 origin, AmmoEvent ammoEvent)
    {
        foreach (var collider in  Physics.OverlapSphere(origin, ammoEvent.range))
        {
            collider.SendMessage("ScreenShake", Vector3.Magnitude(origin - collider.transform.position) * ammoEvent.power,SendMessageOptions.DontRequireReceiver);
        }
    }
}
