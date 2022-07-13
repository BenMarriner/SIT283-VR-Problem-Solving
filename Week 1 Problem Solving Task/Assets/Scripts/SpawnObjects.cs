using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject prefab;
    public GameObject floor;
    public int ballCount;
    public float ballGridMaxHeight;
    public float ballGridGroundOffset;
    private GameObject[,,] balls;

    private void EvaluatePosition(GameObject ball, int xIndex, int yIndex, int zIndex)
    {
        Transform floorTransform = floor.transform;
        // float ballGridHeightOffset = floorTransform.localScale.y / 2 + ballGridGroundOffset;

        Vector3 ballPosition = new Vector3
        {
            //x = Mathf.Lerp(floorTransform.localScale.x * -1, floorTransform.localScale.x, xIndex / (float)(ballCount - 1)) / 2,
            //y = Mathf.Lerp(ballGridHeightOffset, ballGridMaxHeight, yIndex / (float)(ballCount - 1)),
            //z = Mathf.Lerp(floorTransform.localScale.z * -1, floorTransform.localScale.z, zIndex / (float)(ballCount - 1)) / 2

            x = Random.Range(-1 * floorTransform.localScale.x, floorTransform.localScale.x) / 2,
            y = Random.Range(ballGridGroundOffset, ballGridMaxHeight),
            z = Random.Range(-1 * floorTransform.localScale.z, floorTransform.localScale.z) / 2
        };

        ball.transform.position = ballPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        balls = new GameObject[ballCount, ballCount, ballCount];

        for (int x = 0; x < ballCount; x++)
        {
            for (int y = 0; y < ballCount; y++)
            {
                for (int z = 0; z < ballCount; z++)
                {
                    balls[x, y, z] = Instantiate(prefab);
                    EvaluatePosition(balls[x, y, z], x, y, z);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
