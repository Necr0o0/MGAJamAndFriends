using System;
using UnityEngine;

[Serializable]
public abstract class AmmoEvent
{
    [SerializeField] protected float range = 10f;
    [SerializeField] protected float power = 10f;
    
    public abstract void OnBlow(Vector3 origin);
}
