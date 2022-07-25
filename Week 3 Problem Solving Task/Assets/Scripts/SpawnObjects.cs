using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject prefab;
    public int ballCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ballCount; i++)
        {
            Instantiate(prefab);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
