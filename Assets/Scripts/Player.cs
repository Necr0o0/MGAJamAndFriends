using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float acquiredDamage = 4;
    private float maxMass;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        maxMass = rb.mass;
    }

    protected virtual void DealDamage(float damage)
    {
        acquiredDamage += damage;
        rb.mass = (2f - Mathf.Log(acquiredDamage)) * maxMass;
    }
}
