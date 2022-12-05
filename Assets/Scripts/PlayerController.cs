using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Trinitrotoluene;
[RequireComponent(typeof(Rigidbody), typeof(Grab))]
public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float baseSpeed = 1000f;
    [SerializeField] float jumpHeight = 1.0f;
    private float gravityValue;

    [Header("References")]
    [SerializeField] Transform eyeLevel;
    [SerializeField] SphereCollider groundCheck;
    [SerializeField] Grab grab;
    [SerializeField] TMP_Text speedText;
    private Rigidbody rb;

    float lastYposition;
    bool grounded;
    bool sliding;

    InputManager im;
    public Transform cameraTransform;
    Vector3 lastPosition;
    float deltaPosition;
    float speed;
    float speedSmoothed;
    private void Start()
    {
        lastPosition = transform.position;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gravityValue = Physics.gravity.y;
        im = InputManager.Instance;
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        speedSmoothed.MoveTowards(speed, 0.3f);
        /////////////////////
        deltaPosition = Vector3.Distance(transform.position, lastPosition);
        StartCoroutine(addToSpeedAndRemove(deltaPosition));
        speedText.SetText("<mspace=25>" + Math.f.RoundTenth(speedSmoothed) + "</mspace>" + " m/s");
        grounded = IsGrounded();
        /////////////////////
        lastPosition = transform.position;

        if (grounded)
        {
            if (im.SlideStarted())
            {
                transform.localScale = new Vector3(1, 1f, 1);
                transform.position += new Vector3(0, -0.5f, 0);
                sliding = true;
            }
            if (im.Sliding() || im.SlideStarted())
            {
                sliding = true;
            }
            else sliding = false;
        }
        else sliding = false;

        if (grounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }

        Vector2 movement = im.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;
        move = move.normalized;

        if (!sliding)
        {
            rb.AddForce(move * Time.deltaTime * baseSpeed, ForceMode.Acceleration);
            if (grounded)
            {
                if (movement.magnitude > 0)
                    CounterMovement(baseSpeed * 0.3f);
                else CounterMovement(baseSpeed);
            } else {
                CounterMovement(baseSpeed * 0.2f);
            }
            transform.localScale = new Vector3(1, 1.5f, 1);
        }

        if (im.PlayerJumpedThisFrame() && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * -3.0f * gravityValue), rb.velocity.z);
        }

        if (im.Grabbing())
        {
            grab.GrabObject();
        }
        else
        {
            if (grab.objectGrabbing != null)
                grab.StopGrab();
        }
        lastYposition = transform.position.y;
    }
    public bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.transform.position, groundCheck.radius, whatIsGround, QueryTriggerInteraction.Ignore);
        if (colliders.Length > 0) print("IS GROUNDED on " + colliders[0].gameObject.name);
        else print("IS NOT GROUNDED");
        return colliders.Length > 0;
    }
    void CounterMovement(float magnitude)
    {
        Vector2 moveSpeed = new Vector2(rb.velocity.x, rb.velocity.z);
        Vector2 counterForce = moveSpeed * Time.deltaTime * -magnitude;

        if (Mathf.Sign(moveSpeed.x - counterForce.x) != Mathf.Sign(moveSpeed.x))
            counterForce.x = -moveSpeed.x;
        if (Mathf.Sign(moveSpeed.y - counterForce.y) != Mathf.Sign(moveSpeed.y))
            counterForce.y = -moveSpeed.y;

        rb.AddForce(counterForce.x, 0, counterForce.y);
    }
    public IEnumerator addToSpeedAndRemove(float addition){
        speed += addition;
        yield return new WaitForSeconds(1);
        speed -= addition;
    }
}