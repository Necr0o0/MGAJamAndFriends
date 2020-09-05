using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Magazine magazine = default;
    [SerializeField] private Rigidbody playerRb;
    
    public GameObject Bullet;
    
    private int ammo;

    private void Start()
    {
        ammo = magazine.MagazineSize;
    }

    public void Shoot()
    {
        if (ammo > 0)
        {
            ammo--;
            var bullet = GameManager.singleton.bombPool.GetObject(transform.position + playerRb.transform.forward);

            BombController bomb = bullet.GetComponent<BombController>();
            
            bomb.Initialize(magazine.Shoot());
            var rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = transform.GetChild(0).forward * 10f + playerRb.velocity;
        }
    }
}
