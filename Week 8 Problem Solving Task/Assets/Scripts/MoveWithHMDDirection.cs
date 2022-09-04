using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MoveWithHMDDirection : MovementBehaviour
{
    public Transform hmdTransform;

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
            x = hmdTransform.transform.forward.x,
            y = 0,
            z = hmdTransform.transform.forward.z
        };

        base.Update();
    }
}
