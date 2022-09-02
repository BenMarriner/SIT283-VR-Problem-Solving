using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TurnWithTouchpad : MonoBehaviour
{
    public SteamVR_Action_Vector2 turnAction;
    public SteamVR_Input_Sources handType;
    public float rotationSpeed;

    private Quaternion playerRotation;

    // Start is called before the first frame update
    void Start()
    {
        playerRotation = transform.root.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate(turnAction.GetAxis(handType));
    }

    void Rotate(Vector2 direction)
    {
        Quaternion newRotation = new Quaternion()
        {
            x = playerRotation.x,
            y = playerRotation.y + direction.x * rotationSpeed * Time.deltaTime,
            z = playerRotation.z
        };
        playerRotation = newRotation;
        print(newRotation);
        print(playerRotation);
    }
}
