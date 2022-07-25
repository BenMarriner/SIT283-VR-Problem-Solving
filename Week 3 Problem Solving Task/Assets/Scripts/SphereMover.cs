using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class containing information about the moving spheres
/// </summary>
class SphereInfo
{
    public GameObject gameObject; // Sphere object
    public Vector3 targetPos = Vector3.zero; // Current position the sphere is moving to

    public SphereInfo(GameObject gameObject, Vector3 targetPos)
    {
        this.gameObject = gameObject;
        this.targetPos = targetPos;
    }
}

public class SphereMover : MonoBehaviour
{ 
    List<SphereInfo> spheres = new List<SphereInfo>(); // Track moving spheres in this list
    public float movementRadius = 1.0f; // Radius within which the spheres will travel
    public float moveSpeed = 1.0f; // Speed at which the spheres will move
    public bool showRadiusSphere = true; // Whether to highlight the radius the spheres travel within
    public GameObject radiusSphere; // Sphere that highlights the travel radius
    public float sphereScale = 1.0f; // Scale of the moving spheres

    private Vector3 FloatToVector3(float value)
    {
        return new Vector3(value, value, value);
    }

    /// <summary>
    /// Gets a random position on the surface of the unit sphere and scales it with the radius
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    private Vector3 GetRandomPositionOnSphereRadius(float radius)
    {
        return Random.onUnitSphere * radius;
    }

    /// <summary>
    /// Calculate a new target position for a sphere
    /// </summary>
    /// <returns></returns>
    private Vector3 CalculateNewPosition()
    {
        return (transform.position + GetRandomPositionOnSphereRadius(movementRadius)) / 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find spheres in game world
        GameObject[] sphereObjects = GameObject.FindGameObjectsWithTag("Sphere");
       
        foreach (var sphere in sphereObjects)
        {
            spheres.Add(new SphereInfo(sphere, CalculateNewPosition()));
            sphere.transform.parent = transform;

            Color sphereColour = Random.ColorHSV(0, 1, 1, 1, 1, 1);
            sphere.gameObject.GetComponent<Renderer>().material.SetColor("_Color", sphereColour);
            sphere.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", sphereColour);

        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var sphere in spheres)
        {
            Vector3 currentPos = sphere.gameObject.transform.position;

            if (currentPos == sphere.targetPos) sphere.targetPos = CalculateNewPosition();
            else currentPos = Vector3.MoveTowards(currentPos, sphere.targetPos, moveSpeed * Time.deltaTime);

            sphere.gameObject.transform.position = currentPos;
            sphere.gameObject.transform.localScale = FloatToVector3(sphereScale);

            Debug.Log($"Current Position: {currentPos}");
            Debug.Log($"Target Position: {sphere.targetPos}");
        }

        radiusSphere.SetActive(showRadiusSphere);
        radiusSphere.transform.localScale = FloatToVector3(movementRadius);
    }

    
}
