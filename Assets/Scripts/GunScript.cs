using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    PlayerController pc;
    GunManager gunManager;
    public GameObject bullet;
    public Transform bulletOrigin;
    public Gun gunStats;
    public GameObject shotParticles;
    [System.NonSerialized] public int currentAmmo;
     [System.NonSerialized] public float fireTimer = 0;
    [System.NonSerialized] public float reloadTimer = 0;
    bool canFire;
    float timeSinceFire = 0;
    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        gunManager = GunManager.Instance;
        canFire = false;
        timeSinceFire = 0;
        fireTimer = gunStats.fireRate;
        reloadTimer = 0;
        currentAmmo = gunStats.ammo;
    }
    private void Update()
    {
        if(gunManager.heldGun == this){
            GunUpdate();
        }

    }
    public void TryFire()
    {
        if (canFire)
        {
            //play a sound
            GameObject bulletInstance = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
            Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
            Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
            bulletScript.stats = gunStats;
            bulletRb.AddForce(pc.cameraTransform.forward.normalized * gunManager.bulletShootForce);
            currentAmmo--;
            fireTimer = 0;
            timeSinceFire = 0;
            GameObject particleGameobject = Instantiate(shotParticles);
            particleGameobject.transform.position = bulletOrigin.position;
            particleGameobject.GetComponent<ParticleSystem>().Play();
        }
    }
    public void Reload()
    {
        currentAmmo += 1;
        reloadTimer = 0;
        //play a sound
    }
    public void GunUpdate(){
        if(InputManager.Instance.IsTryingToShootGun()){
            TryFire();
        }

        fireTimer += Time.deltaTime;
        timeSinceFire += Time.deltaTime;
        if (currentAmmo < gunStats.ammo && timeSinceFire > gunStats.fireRate + 1) reloadTimer += Time.deltaTime;

        if (fireTimer > gunStats.fireRate && currentAmmo > 0){
            canFire = true;
        }
        else canFire = false;

        if (reloadTimer > gunStats.reloadTime && currentAmmo < gunStats.ammo)
        {
            Reload();
        }
    }
}
