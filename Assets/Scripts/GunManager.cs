using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunScript heldGun;
    public void PickUpGun(GunScript gun){
        if(heldGun == null){
            heldGun = gun;
        }
    }
    public void DropGun(){

    }
}
