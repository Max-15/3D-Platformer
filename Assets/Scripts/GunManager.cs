using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Trinitrotoluene;
using TMPro;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerController), typeof(Player))]
public class GunManager : MonoBehaviour
{
    static GunManager _instance;
    public static GunManager Instance {
        get{return _instance;}
    }
    PlayerController pc;

    public Transform gunParent;
    public float bulletShootForce;

    public TMP_Text ammoText;
    public Transform ammoGroup;
    // public GameObject ammoIcon;
    public Image reloadBar;
    public Image fireRateBar;
    public CanvasGroup gunStatsGroup;

    [NonSerialized] public GunScript heldGun;
    Transform heldGunParent;
    private void Awake()
    {
        _instance = this;
        heldGun = null;
        pc = GetComponent<PlayerController>();
    }
    public void PickUpGun(GunScript gun)
    {
        if (heldGun == null)
        {
            heldGun = gun;
            heldGunParent = heldGun.transform.parent;
            heldGun.transform.SetParent(gunParent);
            Rigidbody rb = heldGun.GetComponent<Rigidbody>();
            Collider collider = heldGun.GetComponent<Collider>();
            collider.enabled = false;
            rb.isKinematic = true;
            
            for(int i = 0; i < heldGun.gunStats.ammo; i++){
                GameObject ammoIconInstance = ammoGroup.GetChild(i).gameObject;
                ammoIconInstance.SetActive(true);
                print("Bullet Icon Activated");
            }
        }
    }
    public void DropGun()
    {
        if (heldGun != null)
        {
            Rigidbody rb = heldGun.gameObject.GetComponent<Rigidbody>();
            Collider collider = heldGun.GetComponent<Collider>();
            collider.enabled = true;
            for(int i = 0; i < ammoGroup.childCount; i++){
                ammoGroup.GetChild(i).gameObject.SetActive(false);
                print("Disabled Bullet Icon");
            }
            heldGun = null;
            rb.isKinematic = false;
            rb.AddForce(pc.cameraTransform.forward * 10f, ForceMode.VelocityChange);
            rb.AddTorque(pc.cameraTransform.forward * 10, ForceMode.VelocityChange);
            
        }
    }
    public void GunManagerUpdate()
    {
        if (heldGun != null)
        {
            ammoText.SetText("<color=#FF7900>"+ heldGun.currentAmmo +"<size=25></color> / " + heldGun.gunStats.ammo);
            gunStatsGroup.alpha += Functions.f.MoveTowards(gunStatsGroup.alpha, 1, 0.1f);
            reloadBar.fillAmount = heldGun.reloadTimer / heldGun.gunStats.reloadTime;
            fireRateBar.fillAmount = heldGun.fireTimer / heldGun.gunStats.fireRate;

            for(int i = 0; i < ammoGroup.childCount; i++){
                if(heldGun.currentAmmo > i){
                    ammoGroup.GetChild(i).GetComponent<Image>().color = new Color(1,1,1,1);
                } else {
                    ammoGroup.GetChild(i).GetComponent<Image>().color = new Color(1,1,1,0.3f);
                }
            }

            heldGun.transform.localScale = new Vector3(1,1,1);
            heldGun.transform.rotation = Quaternion.RotateTowards(heldGun.transform.rotation, Quaternion.LookRotation(-pc.cameraTransform.right * 0.7f, pc.cameraTransform.up), 3000 * Time.deltaTime);
            heldGun.transform.position = pc.cameraTransform.position + (pc.cameraTransform.forward * 1f) + (pc.cameraTransform.right * 0.7f) + ((-pc.cameraTransform.up) * 0.3f);
        } else {
            gunStatsGroup.alpha += Functions.f.MoveTowards(gunStatsGroup.alpha, 0, 0.1f);
        }
    }
}
