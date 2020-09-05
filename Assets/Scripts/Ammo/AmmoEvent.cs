using System;
using UnityEngine;

[Serializable]
public class AmmoEvent
{
    public AmmoActions.Action action = AmmoActions.Action.Damage;
    public float range = 10f;
    public float power = 10f;
}
