using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Magazine magazine = default;
    [SerializeField] private Rigidbody playerRb = default;
    
    public GameObject Bullet;
    
    private int currentShoots = 0;

    public void Shoot()
    {
        if (currentShoots < magazine.MagazineSize)
        {
            currentShoots++;
            var bullet = GameManager.singleton.bombPool.GetObject(transform.position + playerRb.transform.forward);

            BombController bomb = bullet.GetComponent<BombController>();
            
            bomb.Initialize(magazine.Shoot());
            var rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = transform.GetChild(0).forward * 10f + playerRb.velocity;
        }
    }
}
