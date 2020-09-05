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
        PushBack
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
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void DealDamage(Vector3 origin, AmmoEvent ammoEvent)
    {
        
    }

    private static void PushBack(Vector3 origin, AmmoEvent ammoEvent)
    {
        foreach (var collider in  Physics.OverlapSphere(origin, ammoEvent.range))
        {
            Debug.Log("odrzut!");
            if(collider.GetComponent<Rigidbody>())
                collider.transform.GetComponent<Rigidbody>().AddForce((collider.transform.position- origin) * ammoEvent.power);

            if (collider.transform.GetComponent<PlayerController>())
            {
                collider.transform.GetComponent<PlayerController>().enabled = false;
                var sequnenceLost = DOTween.Sequence();
                sequnenceLost.AppendInterval(1f);
                sequnenceLost.OnComplete(() =>
                {
                    collider.transform.GetComponent<PlayerController>().enabled = true;

                });
            }
        }
    }
}
