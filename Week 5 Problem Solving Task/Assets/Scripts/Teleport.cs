using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleport : MovementBehaviour
{
    private LaserPointer pointer;
    private Vector3 teleportPosition;
    GameObject playerCamera;

    protected override void Start()
    {
        base.Start();
        pointer = GetComponent<LaserPointer>();
        playerCamera = cameraRig.transform.Find("Camera").gameObject;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (movementTypeEnabled)
        {
            if (moveAction.GetState(handType)) pointer.laserEnabled = true;
            if (moveAction.GetStateUp(handType))
            {
                pointer.laserEnabled = false;

                Vector3 playerCameraOffset = new Vector3
                {
                    x = playerCamera.transform.localPosition.x,
                    y = 0,
                    z = playerCamera.transform.localPosition.z
                };

                teleportPosition = pointer.HitPoint - playerCameraOffset;
                player.transform.position = teleportPosition;
            }
        }
    }
}
