using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float ammoReloadTime = 0.2f;
    [SerializeField] private Magazine magazine = default;
    [SerializeField] private Rigidbody playerRb = default;

    private int currentShoots = 0;
    private bool canShoot = false;
    private bool reloading = false;
    private WaitForSeconds reloadWait;

    private void Start()
    {
        reloadWait = new WaitForSeconds(ammoReloadTime);
        magazine.Initialize(StopReloading);
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame && !reloading)
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
        }
        else
        {
            canShoot = false;
        }
    }

    public void StopReloading()
    {
        currentShoots = 0;
        reloading = false;
        canShoot = true;
    }
}
