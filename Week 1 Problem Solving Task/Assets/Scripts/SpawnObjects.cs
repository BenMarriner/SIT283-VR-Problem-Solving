using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject prefab;
    public GameObject floor;
    public int ballCount;
    public float ballHeight;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < ballCount; x++)
        {
            for (int y = 0; y < ballCount; y++)
            {
                for (int z = 0; z < ballCount; z++)
                {
                    Transform floorTransform = floor.transform;
                    Vector3 ballPosition = new Vector3
                    {
                        x = Mathf.Lerp(floorTransform.localScale.x * -1, floorTransform.localScale.x, x / (ballCount - 1)) / 2.0f,
                        y = ballHeight * 2.0f * y,
                        z = Mathf.Lerp(floorTransform.localScale.z * -1, floorTransform.localScale.z, z / (ballCount - 1)) / 2.0f
                    };

                    GameObject ball = Instantiate(prefab, ballPosition, Quaternion.identity);

                    print(string.Format("X: {0}, Y: {1}, Z: {2}", x, y, z));
                    print(string.Format("Coords: {0}", ballPosition));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
