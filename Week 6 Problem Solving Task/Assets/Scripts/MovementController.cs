using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject targetObject;
    public float baseSpeed = 10;
    public float speedMultiplier = 10;

    [HideInInspector]
    public bool canSwim = true;
    [HideInInspector]
    public bool isFrozen = false;
    [HideInInspector]
    public Vector3 minBounds = Vector3.zero, maxBounds = new Vector3(10, 10, 10);

    private Rigidbody rb;
    private Vector3 randomisedTargetLocation;

    private float finalSpeed { get { return baseSpeed * speedMultiplier; } }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        randomisedTargetLocation = new Vector3()
        {
            x = Random.Range(minBounds.x, maxBounds.x),
            y = Random.Range(minBounds.y, maxBounds.y),
            z = Random.Range(minBounds.z, maxBounds.z)
        };
    }

    private void FixedUpdate()
    {
        //if (!targetObject) targetLocation = transform.position;
        //else targetLocation = targetObject.transform.position;

        //if (swimTowardsTarget) SwimTowards(targetLocation, finalSpeed);
        //else SwimAway(targetLocation, finalSpeed);

        if (isFrozen) rb.constraints = RigidbodyConstraints.FreezeAll;
        else rb.constraints = RigidbodyConstraints.None;
    }

    public void SwimRandom()
    {
        if (IsAlmostEqual(randomisedTargetLocation, transform.position, 1.0f))
        {
            randomisedTargetLocation = new Vector3()
            {
                x = Random.Range(minBounds.x, maxBounds.x),
                y = Random.Range(minBounds.y, maxBounds.y),
                z = Random.Range(minBounds.z, maxBounds.z)
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

    private bool IsAlmostEqual(Vector3 v1, Vector3 v2, float threshold)
    {
        return Vector3.Distance(v1, v2) <= threshold;
    }
}
