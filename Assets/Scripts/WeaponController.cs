using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float ammoReloadTime = 2.2f;
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] private Magazine magazine = default;
    [SerializeField] private Rigidbody playerRb = default;
    private Gamepad _gamepad;

    private int currentShoots = 0;
    private bool canShoot = false;
    private bool reloading = false;
    private WaitForSeconds reloadWait;
    private WaitForSeconds cooldownWait;
    
    private void Start()
    {
        reloadWait = new WaitForSeconds(ammoReloadTime);
        cooldownWait = new WaitForSeconds(cooldown);
        magazine.Initialize(StopReloading);
    }

    private void Update()
    {
        if ( Gamepad.current != null && Gamepad.current.xButton.wasPressedThisFrame && !reloading)
        {
            reloading = true;
            StartCoroutine(magazine.LoadNewTexture(reloadWait, StopReloading));
        }
    }

    public void Shoot()
    {
        if (!canShoot || reloading)
            return;
        
        if (currentShoots < magazine.MagazineSize)
        {
            currentShoots++;
            var bullet = GameManager.singleton.bombPool.GetObject(transform.position + playerRb.transform.forward);

            BombController bomb = bullet.GetComponent<BombController>();
            
            bomb.Initialize(magazine.Shoot());
            var rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = transform.GetChild(0).forward * 10f + playerRb.velocity;

            StartCoroutine(Cooldown());
        }
        else
        {
            canShoot = false;
        }
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return cooldownWait;
        canShoot = true;
    }

    public void StopReloading()
    {
        currentShoots = 0;
        reloading = false;
        canShoot = true;
    }
}
