using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trinitrotoluene;
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
        if (gunManager.heldGun == this)
        {
            GunUpdate();
        }

    }
    public void TryFire()
    {
        if (canFire)
        {
            //play a sound
                GameObject[] bulletInstances = new GameObject[gunStats.bulletsPerShot];
                for (int i = 0; i < gunStats.bulletsPerShot; i++)
                {
                    bulletInstances[i] = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
                }
                foreach (GameObject bulletInstance in bulletInstances)
                {
                    Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
                    Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
                    bulletScript.stats = gunStats;

                    if (Physics.Raycast(pc.cameraTransform.position, pc.cameraTransform.forward, out RaycastHit hit, 300))
                    {
                        bulletRb.AddForce(GetBulletForce(gunStats.accuracy, hit.point), ForceMode.VelocityChange);
                    }
                    else
                    {
                        bulletRb.AddForce(GetBulletForce(gunStats.accuracy), ForceMode.VelocityChange);
                    }
                }
                currentAmmo -= gunStats.bulletsPerShot;
                fireTimer = 0;
                timeSinceFire = 0;
                GameObject particleGameobject = Instantiate(shotParticles);
                particleGameobject.transform.position = bulletOrigin.position;
                particleGameobject.GetComponent<ParticleSystem>().Play();
        }
    }
    Vector3 GetBulletForce(float accuracy, Vector3 hitPoint)
    {
        float spreadFactor = (1 - accuracy) * gunManager.bulletSpreadMultiplier;

        Vector3 direction = (hitPoint - bulletOrigin.position).normalized * gunManager.bulletShootForce * gunStats.bulletSpeedMultiplier;
        direction.x += Random.Range(-spreadFactor, spreadFactor);
        direction.y += Random.Range(-spreadFactor, spreadFactor);
        direction.z += Random.Range(-spreadFactor, spreadFactor);
        return direction;
    }
    Vector3 GetBulletForce(float accuracy)
    {
        float spreadFactor = (1 - accuracy) * gunManager.bulletSpreadMultiplier;

        Vector3 direction = (pc.cameraTransform.forward).normalized * gunManager.bulletShootForce * gunStats.bulletSpeedMultiplier;
        direction.x += Random.Range(-spreadFactor, spreadFactor);
        direction.y += Random.Range(-spreadFactor, spreadFactor);
        direction.z += Random.Range(-spreadFactor, spreadFactor);
        return direction;
    }
    public void Reload()
    {
        currentAmmo += 1;
        reloadTimer = 0;
        //play a sound
    }
    public void GunUpdate()
    {
        if (InputManager.Instance.IsTryingToShootGun())
        {
            TryFire();
        }

        fireTimer += Time.deltaTime;
        timeSinceFire += Time.deltaTime;
        if (currentAmmo < gunStats.ammo && timeSinceFire > gunStats.fireRate + 1) reloadTimer += Time.deltaTime;

        if (fireTimer > gunStats.fireRate && currentAmmo >= gunStats.bulletsPerShot)
        {
            canFire = true;
        }
        else canFire = false;

        if (reloadTimer > gunStats.reloadTime && currentAmmo < gunStats.ammo)
        {
            Reload();
        }
    }
}
