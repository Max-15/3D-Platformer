using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Trinitrotoluene;
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
        }
    }
    public void DropGun()
    {
        if (heldGun != null)
        {
            Rigidbody rb = heldGun.gameObject.GetComponent<Rigidbody>();
            Collider collider = heldGun.GetComponent<Collider>();
            collider.enabled = true;
            heldGun = null;
            rb.isKinematic = false;
            rb.AddForce(pc.cameraTransform.forward * 10f, ForceMode.VelocityChange);
            rb.AddTorque(pc.cameraTransform.forward * 10, ForceMode.VelocityChange);
        }
    }
    public void GunTransformUpdate()
    {
        if (heldGun != null)
        {
            heldGun.transform.localScale = new Vector3(1,1,1);
            heldGun.transform.rotation = Quaternion.RotateTowards(heldGun.transform.rotation, Quaternion.LookRotation(-pc.cameraTransform.right * 0.7f, pc.cameraTransform.up), 3000 * Time.deltaTime);
            heldGun.transform.position = pc.cameraTransform.position + (pc.cameraTransform.forward * 1f) + (pc.cameraTransform.right * 0.7f) + ((-pc.cameraTransform.up) * 0.3f);
        }
    }
}
