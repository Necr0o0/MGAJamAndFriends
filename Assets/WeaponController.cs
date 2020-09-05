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
            var bullet =  Instantiate(Bullet,transform.GetChild(0).position,Quaternion.identity);
            var rb = bullet.transform.GetComponent<Rigidbody>();
            float randX = Random.Range(-1f, 1f);
            float randY = Random.Range(-1f, 1f);
            float randZ = Random.Range(-1f, 1f);

            rb.angularVelocity = new Vector3(randX, randY, randZ) *100f;
            bullet.transform.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward * 1000f);

        }
    }


}
