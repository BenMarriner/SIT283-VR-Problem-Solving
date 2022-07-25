using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour
{
    List<GameObject> spheres = new List<GameObject>();
    public float spawnRadius = 1;

    private Vector3 GetRandomPositionInRadius(float radius)
    {
        return Random.insideUnitSphere * radius;
    }

    private bool IsWithinRadius(Transform objectTransform, float radius)
    {
        return 
    }

    // Start is called before the first frame update
    void Start()
    {
        spheres.AddRange(GameObject.FindGameObjectsWithTag("Sphere"));
        foreach (GameObject sphere in spheres)
        {
            sphere.transform.parent = transform;
            sphere.transform.position = GetRandomPositionInRadius(spawnRadius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject sphere in spheres)
        {
            
        }
    }
}
