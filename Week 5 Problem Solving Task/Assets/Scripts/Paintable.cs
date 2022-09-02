using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    [HideInInspector]
    public Texture2D surfaceTexture;

    // Start is called before the first frame update
    void Start()
    {
        surfaceTexture = GetComponent<MeshRenderer>().material.mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
