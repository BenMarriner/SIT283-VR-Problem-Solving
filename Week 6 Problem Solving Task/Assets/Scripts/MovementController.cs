using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject targetObject;
    public float baseSpeed = 10;
    public float speedMultiplier = 10;

    [HideInInspector]
    public bool canSwim = true;

    private Rigidbody rb;
    private Vector3 randomisedTargetLocation;

    private float finalSpeed { get { return baseSpeed * speedMultiplier; } }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //if (!targetObject) targetLocation = transform.position;
        //else targetLocation = targetObject.transform.position;

        //if (swimTowardsTarget) SwimTowards(targetLocation, finalSpeed);
        //else SwimAway(targetLocation, finalSpeed);
    }

    public void SwimRandom()
    {
        if (randomisedTargetLocation == null || randomisedTargetLocation == transform.position)
        {
            randomisedTargetLocation = new Vector3()
            {
                x = Random.Range(0, 100),
                y = Random.Range(0, 100),
                z = Random.Range(0, 100)
            };
        }

        SwimTowards(randomisedTargetLocation, finalSpeed);
    }

    public void SwimTowards(GameObject targetObject)
    {
        SwimTowards(targetObject.transform.position, finalSpeed);
    }

    public void SwimAway(GameObject targetObject)
    {
        SwimAway(targetObject.transform.position, finalSpeed);
    }

    private void SwimTowards(Vector3 targetLocation, float speed)
    {
        rb.transform.LookAt(targetLocation);
        SwimForwards(speed);
    }

    private void SwimAway(Vector3 targetLocation, float speed)
    {
        rb.transform.LookAt(rb.transform.position - targetLocation);
        SwimForwards(speed);
    }

    private void SwimForwards(float speed)
    {
        if (canSwim) rb.velocity = rb.transform.forward * speed * Time.fixedDeltaTime;
        else rb.velocity = Vector3.zero;
    }
}
