using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Use the mouse to move the rigidbody of an object */

public class MouseShake : MonoBehaviour
{
    public Vector3 minPos, maxPos;
    public float moveSpeed;
    private Rigidbody rb;
    public Camera playerCam;

    private Vector3 ClampVector(Vector3 vector, Vector3 min, Vector3 max)
    {
        Vector3 newVector = new Vector3
        {
            x = Mathf.Lerp(vector.x, min.x, max.x),
            y = Mathf.Lerp(vector.y, min.y, max.y),
            z = Mathf.Lerp(vector.z, min.z, max.z)
        };

        return newVector;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position from centre of screen
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;

        // Cast ray from camera to world to determine where the object will move to
        Physics.Raycast(playerCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);

        // Move object to new position over time
        rb.position = Vector3.Lerp(rb.position, hit.point, Time.deltaTime * moveSpeed);
        ClampVector(rb.position, minPos, maxPos);
    }
}
