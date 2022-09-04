using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucket : MonoBehaviour
{
    public Color paintColour = Color.green;
    
    GameObject paint;

    Material bucketColourMaterial;
    Material paintMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        Configure();
        SetBucketPaintColour(paintColour);
        Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider>(), GetComponent<Collider>());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void Configure()
    {
        paint = transform.Find("paint").gameObject;

        bucketColourMaterial = GetComponent<MeshRenderer>().materials[1];
        paintMaterial = paint.GetComponent<MeshRenderer>().materials[0];
    }

    public void SetBucketPaintColour(Color colour)
    {
        bucketColourMaterial.color = colour;
        paintMaterial.color = colour;
        paintColour = colour;
    }
}
