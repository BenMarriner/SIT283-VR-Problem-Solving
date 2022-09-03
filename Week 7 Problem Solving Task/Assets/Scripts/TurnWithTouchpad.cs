using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TurnWithTouchpad : MonoBehaviour
{
    public SteamVR_Action_Vector2 turnAction;
    public SteamVR_Input_Sources handType;
    public float rotationSpeed;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform.root.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate(turnAction.GetAxis(handType));
    }

    void Rotate(Vector2 direction)
    {
        playerTransform.Rotate(Vector3.up * direction.x * rotationSpeed * Time.deltaTime);
    }
}
