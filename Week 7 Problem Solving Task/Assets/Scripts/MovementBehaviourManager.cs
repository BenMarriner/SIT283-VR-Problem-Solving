using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementTypes { Teleport, MoveWithHMDDirection, MoveWithControllerDirection }

public class MovementBehaviourManager : MonoBehaviour
{
    public MovementTypes movementType;
    public float moveSpeed;
    private MovementBehaviour currentMovementBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        switch (movementType)
        {
            case MovementTypes.Teleport:
                currentMovementBehaviour = GetComponent<Teleport>();
                break;
            case MovementTypes.MoveWithHMDDirection:
                currentMovementBehaviour = GetComponent<MoveWithHMDDirection>();
                break;
            case MovementTypes.MoveWithControllerDirection:
                currentMovementBehaviour = GetComponent<MoveWithControllerDirection>();
                break;
            default:
                break;
        }

        currentMovementBehaviour.movementTypeEnabled = true;
        currentMovementBehaviour.moveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
