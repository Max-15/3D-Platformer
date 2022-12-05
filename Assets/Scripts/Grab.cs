using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grab : MonoBehaviour
{
    [SerializeField] LayerMask grabbableLayers;
    [SerializeField] PlayerController playerController;
    [SerializeField] LineRenderer lr;

    [NonSerialized] public Rigidbody objectGrabbing = null;
    [NonSerialized] public SpringJoint grabJoint;
    [NonSerialized] public Vector3 grabPoint;
    [NonSerialized] public Vector2 objectOldDrags;
    private void Start() {
        lr.enabled = false;
    }
    public void GrabObject()
    {
        if (objectGrabbing == null)
            StartGrab();
        else HoldGrab();
    }
    public void StartGrab()
    {
        RaycastHit[] hits = Physics.RaycastAll(playerController.cameraTransform.position, playerController.cameraTransform.forward, 10, grabbableLayers, QueryTriggerInteraction.Ignore);
        if (hits.Length == 0) return;
        for (int i = 0; i < hits.Length; i++)
        {
            print("started dragging " + hits[i].collider.gameObject.name + ", layer:" + hits[i].collider.gameObject.layer);
            Rigidbody objectRb;
            if (hits[i].collider.gameObject.TryGetComponent<Rigidbody>(out objectRb))
            {
                objectGrabbing = objectRb;

                grabPoint =  hits[i].point;
                grabJoint = objectGrabbing.gameObject.AddComponent<SpringJoint>();
                //grabJoint.anchor = grabPoint - objectGrabbing.position
                grabJoint.autoConfigureConnectedAnchor = false;
                grabJoint.minDistance = 0f;
                grabJoint.maxDistance = 0f;
                grabJoint.damper = 4;
                grabJoint.spring = 40;
                grabJoint.massScale = 5;
                
                objectOldDrags.x = objectGrabbing.drag;
                objectOldDrags.y = objectGrabbing.angularDrag;
                objectGrabbing.angularDrag = 5;
                objectGrabbing.drag = 5;

                lr.enabled = true;
                lr.positionCount = 2;

                SetLinesAndAnchor();
                return;
            }
            else
            {
                continue;
            }
        }
    }
    public void HoldGrab()
    {
        SetLinesAndAnchor();
    }
    public void StopGrab(){
        Destroy(grabJoint);
        lr.enabled = false;
        objectGrabbing.drag = objectOldDrags.x;
        objectGrabbing.angularDrag = objectOldDrags.y;
        objectGrabbing = null;
    }
    public Vector3 GetThrowPosition(){
        return playerController.cameraTransform.position + playerController.cameraTransform.forward * 5.5f;
    }
    public void SetLinesAndAnchor(){
        grabJoint.connectedAnchor = GetThrowPosition();
        lr.SetPosition(0, objectGrabbing.transform.position);
        lr.SetPosition(1, GetThrowPosition());
    }
}
