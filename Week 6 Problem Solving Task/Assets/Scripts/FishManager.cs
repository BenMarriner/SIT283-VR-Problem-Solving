using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public GameObject prefab;
    public int objectCount = 10;
    public Vector3 minSpawnArea, maxSpawnArea;
    public float minFishSize, maxFishSize;
    public float minSwimSpeed, maxSwimSpeed;

    private Vector3 randomSpawnPosition { get { return RandomVector(minSpawnArea, maxSpawnArea); } }
    private float randomFishSize { get { return Random.Range(minFishSize, maxFishSize); } }
    private float randomSwimSpeed { get { return Random.Range(minSwimSpeed, maxSwimSpeed); } }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < objectCount; i++)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        var fish = Instantiate(prefab);
        float fishSize = randomFishSize;
        float swimSpeed = randomSwimSpeed;
        fish.transform.position = randomSpawnPosition;

        var fishStateManager = fish.GetComponent<FishStateManager>();
        fishStateManager.fishSize = fishSize;
        fishStateManager.swimSpeed = swimSpeed;
        fishStateManager.minBounds = minSpawnArea;
        fishStateManager.maxBounds = maxSpawnArea;

        SetColour(fish);
    }

    private void SetColour(GameObject fish)
    {
        // Set colour of fish
        Color fishColour = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        fish.GetComponent<Renderer>()
            .material.SetColor("_Color", fishColour);
        fish.GetComponent<Renderer>()
            .material.SetColor("_EmissionColor", fishColour);
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
