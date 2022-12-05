using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static InputManager _instance;
    public static InputManager Instance {
        get{return _instance;}
    }
    PlayerControls playerControls;
    void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }    
    private void OnDisable() {
        playerControls.Disable();
    }
    public Vector2 GetPlayerMovement(){
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta(){
        return playerControls.Player.Look.ReadValue<Vector2>();
    }
    public bool PlayerJumpedThisFrame(){
        return playerControls.Player.Jump.IsPressed();
    }
    public bool ClickStartedThisFrame(){
        return playerControls.Other.Click.triggered;
    }
    public bool MouseDown(){
        return playerControls.Other.Click.IsPressed();
    }
    public bool Sliding(){
        return playerControls.Player.Sliding.IsPressed();
    }
    public bool SlideStarted(){
        return playerControls.Player.Sliding.triggered;
    }
    public bool Grabbing(){
        return playerControls.Player.Grab.IsPressed();
    }
    public bool GrabStarted(){
        return playerControls.Player.Grab.triggered;
    }
    public Vector2 GetMousePosition(){
        return playerControls.Other.MousePosition.ReadValue<Vector2>();
    }
    public bool GunPickedUpThisFrame(){
        return playerControls.Gun.PickUpGun.triggered;
    }
    public bool GunDroppedThisFrame(){
        return playerControls.Gun.DropGun.triggered;
    }
    public bool IsTryingToShootGun(){
        return playerControls.Gun.ShootGun.IsPressed();
    }
    public bool PauseThisFrame(){
        return playerControls.Other.PauseMenu.triggered;
    }
}
