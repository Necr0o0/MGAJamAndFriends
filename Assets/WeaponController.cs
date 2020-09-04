using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int ammo;
    public GameObject Bullet;


    public void Shoot()
    {
        if (ammo > 0)
        {
            ammo--;
            var bullet =  Instantiate(Bullet,transform.GetChild(0).position,Quaternion.identity);
            bullet.transform.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward * 1000f);

        }
    }


}
