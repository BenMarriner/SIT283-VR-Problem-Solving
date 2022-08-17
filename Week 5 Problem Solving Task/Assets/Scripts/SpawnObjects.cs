using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject prefab;
    public int objectCount = 10;
    public Vector3 minSpawnArea, maxSpawnArea;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < objectCount; i++)
        {
            SpawnBall();
        }
    }

    public void SpawnBall()
    {
        var sphere = Instantiate(prefab);
        sphere.transform.position = RandomVector(minSpawnArea, maxSpawnArea);
        Color sphereColour = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        sphere.GetComponent<Renderer>()
            .material.SetColor("_Color", sphereColour);
        sphere.GetComponent<Renderer>()
            .material.SetColor("_EmissionColor", sphereColour);
    }

    Vector3 RandomVector(Vector3 min, Vector3 max)
    {
        return new Vector3
        {
            x = Random.Range(min.x, max.x),
            y = Random.Range(min.y, max.y),
            z = Random.Range(min.z, max.z)
        };
    }
}
