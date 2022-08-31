using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public abstract class MovementBehaviour : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean moveAction;
    protected Vector3 moveDirection;
    protected GameObject player;
    protected GameObject cameraRig;

    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public bool movementTypeEnabled = false;

    protected virtual void Start()
    {
        player = transform.root.gameObject;
        cameraRig = player.transform.Find("[CameraRig]").gameObject;
    }

    protected virtual void Update()
    {
        if (movementTypeEnabled && moveAction.GetState(handType)) Move();
    }

    protected virtual void Move()
    {
        player.transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
