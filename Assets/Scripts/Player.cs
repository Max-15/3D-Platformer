using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    PlayerController pc;
    public LayerMask whatIsGun;
    public GunManager gunManager;
    InputManager im;
    private void Start()
    {
        pc = GetComponent<PlayerController>();
        im = InputManager.Instance;
    }
    void Update()
    {
        if (im.PauseThisFrame())
        {
            Trinitrotoluene.Functions.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        if (im.GunPickedUpThisFrame())
        {
            if (gunManager.heldGun == null)
            {
                print("gunPickUpPressed");
                RaycastHit hit;
                Physics.Raycast(transform.position, pc.cameraTransform.forward, out hit, 5, whatIsGun, QueryTriggerInteraction.Ignore);



                if (hit.transform != null)
                {
                    GunScript gunScript;
                    print("gun raycast hit: " + hit.transform.gameObject.name);
                    if (hit.transform.TryGetComponent<GunScript>(out gunScript))
                    {
                        gunManager.PickUpGun(gunScript);
                        print("picked up gun: " + gunScript.transform.gameObject.name);
                    }
                }
                else
                {
                    print("gun raycast hit: " + "nothing");
                }
            }
        }
        if (im.GunDroppedThisFrame())
        {
            if (gunManager.heldGun != null)
            {
                gunManager.DropGun();
            }
        }

        gunManager.GunUpdate();
    }
}