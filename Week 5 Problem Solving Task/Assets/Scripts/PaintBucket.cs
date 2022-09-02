using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucket : MonoBehaviour
{
    public Color paintColour = Color.green;
    
    GameObject bucket;
    GameObject paint;

    Material bucketColourMaterial;
    Material paintMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        Configure();
        SetBucketPaintColour(paintColour);
    }


    // Update is called once per frame
    void Update()
    {
        print(bucket);
    }

    private void OnValidate()
    {
        Configure();
        SetBucketPaintColour(paintColour);
    }

    private void Configure()
    {
        bucket = gameObject.transform.Find("Bucket").gameObject;
        paint = bucket.transform.Find("paint").gameObject;

        bucketColourMaterial = bucket.GetComponent<MeshRenderer>().sharedMaterials[1];
        paintMaterial = paint.GetComponent<MeshRenderer>().sharedMaterials[0];
    }

    public void SetBucketPaintColour(Color colour)
    {
        bucketColourMaterial.color = colour;
        paintMaterial.color = colour;
        paintColour = colour;
    }
}
