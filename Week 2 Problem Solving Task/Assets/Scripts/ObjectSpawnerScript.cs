using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerScript : MonoBehaviour
{
    public int numObjects = 5; // Number of objects to spawn
    public GameObject prefab; // Template to spawn objects of
    public float distancePerObject = 1; // Spacing between each object
    
    /// <summary>
    /// Sets the position of a spawned object in the line of objects
    /// This function intends to satisfy the second part of the challenge since the function is called
    /// multiple times in the code with different parameter values
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="index"></param>
    private void SetPosition(GameObject gameObject, int index)
    {
        // Calculate new position
        Vector3 newPosition = transform.position;
        newPosition += index * distancePerObject * Vector3.right;

        gameObject.transform.position = newPosition; // First object will spawn at the location of the script's parent
    } 
    
    // Start is called before the first frame update
    void Start()
    {
        // Spawn n number of objects (n is defined by numObjects variable)
        for (int i = 0; i < numObjects; i++)
        {
            var spawnedObject = Instantiate(prefab);
            SetPosition(spawnedObject, i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
