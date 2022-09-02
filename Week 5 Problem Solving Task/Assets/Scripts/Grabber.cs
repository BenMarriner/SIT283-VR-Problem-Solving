using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grabber : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;
    Collider grabberCollider;

    // Start is called before the first frame update
    void Start()
    {
        grabberCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.TryGetComponent<Grabable>(out var grabable))
        {
            var grabableTransform = grabable.transform;
            var grabableRigidBody = grabable.GetComponentInChildren<Rigidbody>();

            if (grabAction.GetState(handType))
            {
                grabableRigidBody.isKinematic = true;
                grabableTransform.SetParent(transform);
                grabableTransform.localPosition = Vector3.zero;
            }
            else
            {
                grabableRigidBody.isKinematic = false;
                grabableTransform.SetParent(null);
            }
        }
    }
}
