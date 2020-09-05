using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AmmoEventPushBack : AmmoEvent
{
    public override void OnBlow(Vector3 origin)
    {
        foreach (var collider in  Physics.OverlapSphere(origin, range))
        {
            Debug.Log("odrzut!");
            if(collider.GetComponent<Rigidbody>())
                collider.transform.GetComponent<Rigidbody>().AddForce((collider.transform.position- origin) * power);

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
