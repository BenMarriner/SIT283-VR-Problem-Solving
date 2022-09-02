using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MoveWithControllerDirection : MovementBehaviour
{
    public SteamVR_Behaviour_Pose controllerPose;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Update move direction before calling the parent update function
        moveDirection = new Vector3()
        {
            x = controllerPose.transform.forward.x,
            y = 0,
            z = controllerPose.transform.forward.z
        };

        base.Update();
    }
}
