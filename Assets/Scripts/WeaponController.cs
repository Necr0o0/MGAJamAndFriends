using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponController : MonoBehaviour
{
    public int ammo;
    public GameObject Bullet;
    private Rigidbody rb;
    


    public void Shoot()
    {
        if (ammo > 0)
        {
            ammo--;
            var bullet = GameManager.singleton.bombPool.GetObject(transform.position);
            
            var rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.GetChild(0).forward * 1000f);
        }
    }


}
