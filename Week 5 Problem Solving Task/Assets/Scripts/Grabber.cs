using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grabber : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;
    [HideInInspector]
    public GameObject heldObject;
    public Transform grabberGrabTransform;
    public bool snapToGrabberGrabTransform; // If true, objects will snap to the grab point of the grabber

    private Collider objectDetector;
    private List<GameObject> collidingObjects;

    public bool IsHoldingObject { get { return heldObject != null; } }

    // Start is called before the first frame update
    void Start()
    {
        if (!grabberGrabTransform)
        {
            grabberGrabTransform.position = Vector3.zero;
            grabberGrabTransform.rotation = Quaternion.identity;
        }

        objectDetector = GetComponent<Collider>();
        collidingObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // The SteamVR CameraRig prefab always deactivates the controller's children for some reason. This will keep the grab point active
        grabberGrabTransform.gameObject.SetActive(true);

        GameObject closestObject = null;

        // Determine the closest object to the grabber object
        // Only evaluate closest objects if we're not holding anything
        if (!IsHoldingObject && collidingObjects.Count > 0)
        {
            closestObject = collidingObjects[0];
            float closestObjectDistance = Vector3.Distance(collidingObjects[0].transform.position, transform.position);

            foreach (var obj in collidingObjects)
            {
                float distance = Vector3.Distance(obj.transform.position, transform.position);
                if (distance < closestObjectDistance)
                {
                    closestObject = obj;
                }
            }
        }

        if (grabAction.GetStateDown(handType))
        {
            if (!IsHoldingObject && closestObject) Grab(closestObject.GetComponent<Grabable>());
            else Drop();
        }
    }

    /**
     * The OnTriggerEnter and OnTriggerExit events will filter out any objects that do not have a Grabable component
    **/

    private void OnTriggerEnter(Collider other)
    {
        if (!collidingObjects.Contains(other.gameObject) && other.gameObject.GetComponent<Grabable>() != null)
        {
            collidingObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collidingObjects.Remove(other.gameObject);
    }

    void Grab(Grabable item)
    {
        if (!item.IsObjectBeingHeld)
        {
            heldObject = item.gameObject;
            // Attach to grab point of grabber if a grab point has been provided. Otherwise, use the game object's transform as the grab point
            item.Grab(snapToGrabberGrabTransform ? grabberGrabTransform : transform);
        }
    }

    void Drop()
    {
        if (IsHoldingObject)
        {
            heldObject.GetComponent<Grabable>().Drop();
            heldObject = null;
        }
    }
}