using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean laserAction;
    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    public Vector3 hitPoint;
    public GameObject hitObject;

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, 0.5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3
        (
            laserTransform.localScale.x,
            laserTransform.localScale.y,
            hit.distance
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (laserAction.GetState(handType))
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                hitObject = hit.transform.gameObject;
                InteractableObject interactableObject = hitObject.GetComponent<InteractableObject>();
                if (interactableObject)
                {
                    interactableObject.Interact();
                }
                ShowLaser(hit);
            }

            print("Firing laser");
        }
        else laser.SetActive(false);
    }
}
