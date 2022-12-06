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

    int currentAmmo;
    float fireTimer = 0;
    float reloadTimer = 0;
    bool canFire;
    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        gunManager = GunManager.Instance;
        canFire = false;
        fireTimer = 0;
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
            //particle effect
            //fire things
            GameObject bulletInstance = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
            Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
            bulletRb.AddForce(pc.cameraTransform.forward.normalized * gunManager.bulletShootForce);
            currentAmmo--;
            fireTimer = 0;
            GameObject particleGameobject = Instantiate(shotParticles);
            particleGameobject.transform.position = bulletOrigin.position;
            particleGameobject.GetComponent<ParticleSystem>().Play();
        }
    }
    public void Reload()
    {
        currentAmmo += 1;
        //play a sound
    }
    public void GunUpdate(){
        if(InputManager.Instance.IsTryingToShootGun()){
            TryFire();
        }

        fireTimer += Time.deltaTime;
        if (currentAmmo < gunStats.ammo) reloadTimer += Time.deltaTime;

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
