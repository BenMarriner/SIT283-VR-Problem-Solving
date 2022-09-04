using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    public bool UsePreferredGrabbingPoint;
    public bool UsePreferredCarryAngles;
    public Transform grabableGrabTransform;
    public Quaternion grabableGrabRotation;
    public bool IsObjectBeingHeld { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (!grabableGrabTransform)
        {
            grabableGrabTransform = transform.root.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grab(Transform grabberGrabTransform)
    {
        IsObjectBeingHeld = true;
        GetComponent<Rigidbody>().isKinematic = true;
        
        // If using preferred grabbing point
        if (UsePreferredGrabbingPoint)
        {
            // Attach item to grab point
            grabableGrabTransform.SetParent(grabberGrabTransform);
            grabableGrabTransform.localPosition = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.SetParent(grabberGrabTransform);
        }

        // If using preferred carry angles, set the rotation of the grabable accordingly
        if (UsePreferredCarryAngles)
        {
            transform.localRotation = Quaternion.identity;
            grabableGrabTransform.localRotation = grabableGrabRotation;
        }
    }

    public void Drop()
    {
        IsObjectBeingHeld = false;
        GetComponent<Rigidbody>().isKinematic = false;
        grabableGrabTransform.parent = null;
    }
}
