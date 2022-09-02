using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    public Color PaintBrushColour = Color.green;
    public int brushSize = 100;

    GameObject brush;
    Material brushHeadMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        Configure();
    }


    // Update is called once per frame
    void Update()
    {
        EvaluatePaintableSurface();
    }

    private void OnTriggerEnter(Collider other)
    {
        var bucketComponent = other.transform.root.GetComponent<PaintBucket>();
        if (bucketComponent != null) SetBrushColour(bucketComponent.paintColour);
    }

    private void OnCollisionEnter(Collision other)
    {
        var bucketComponent = other.transform.root.GetComponent<PaintBucket>();
        if (bucketComponent != null)
        {
            Physics.IgnoreCollision(GetComponent<MeshCollider>(), bucketComponent.GetComponentInChildren<MeshCollider>());
        }
    }

    private void OnValidate()
    {
        Configure();
        SetBrushColour(PaintBrushColour);
    }

    private void Configure()
    {
        brush = gameObject.transform.Find("Brush").gameObject;
        brushHeadMaterial = brush.GetComponent<MeshRenderer>().sharedMaterials[1];
        SetBrushColour(PaintBrushColour);
    }

    public void SetBrushColour(Color brushColour)
    {
        brushHeadMaterial.color = brushColour;
        PaintBrushColour = brushColour;
    }

    private void EvaluatePaintableSurface()
    {
        Vector3 rayStartPos = transform.position;
        Vector3 rayDirection = transform.forward;
        RaycastHit hit;
        
        if (Physics.Raycast(rayStartPos, rayDirection, out hit, 0.5f))
        {
            var paintable = hit.transform.gameObject.GetComponent<Paintable>();

            if (paintable)
            {
                print("Touching paintable surface");
                
                Texture2D tex = paintable.surfaceTexture;
                Vector2 pixelUV = hit.textureCoord;
                pixelUV.x *= tex.width;
                pixelUV.y *= tex.height;

                for (int x = -(brushSize / 2); x < brushSize / 2; x++)
                {
                    for (int y = -(brushSize / 2); y < brushSize / 2; y++)
                    {
                        tex.SetPixel((int)pixelUV.x + x, (int)pixelUV.y + y, PaintBrushColour);
                    }
                }

                tex.Apply();
            }
        }
    }
}
