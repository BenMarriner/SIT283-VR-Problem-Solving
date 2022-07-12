using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisedMovement : MonoBehaviour
{
    public GameObject floor;
    private Vector3 moveDirection;
    private float speed;
    public float ballGridMaxHeight;
    public float ballGridGroundOffset;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = (Random.insideUnitSphere);
        speed = Random.Range(0.01f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position + moveDirection * speed / Time.deltaTime;

        transform.localPosition = newPos;
    }
}
