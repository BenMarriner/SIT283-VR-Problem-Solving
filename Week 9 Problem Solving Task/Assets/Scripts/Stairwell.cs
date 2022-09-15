using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairwell : Builder
{
    public enum WallType { Side, Back, Doorway }
    
    public GameObject floor;
    public GameObject ceiling;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject frontWall;
    public GameObject doorway;
    public GameObject staircase;

    public bool isBottomFloorStairwell;
    public bool isTopFloorStairwell;

    public GameObject staircasePrefab;
    public GameObject sideWallPrefab;
    public GameObject sideWallTopFloorPrefab;
    public GameObject frontWallPrefab;
    public GameObject frontWallTopFloorPrefab;
    public GameObject doorwayPrefab;
    public GameObject doorWayTopFloorPrefab;
    public GameObject bottomFloorPrefab;
    public GameObject ceilingPrefab;

    // Build stairwell
    public override void Build()
    {
        // Snap points
        Vector3 centerSnapPoint = transform.position;
        Vector3 leftSnapPoint = centerSnapPoint + Vector3.Scale(bottomFloorPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.extents, new Vector3(-1, 0, 0));
        Vector3 rightSnapPoint = centerSnapPoint + Vector3.Scale(bottomFloorPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.extents, new Vector3(1, 0, 0));
        Vector3 frontSnapPoint = centerSnapPoint + Vector3.Scale(bottomFloorPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.extents, new Vector3(0, 0, 1));
        Vector3 backSnapPoint = centerSnapPoint + Vector3.Scale(bottomFloorPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.extents, new Vector3(0, 0, -1));

        // Floor
        if (isBottomFloorStairwell) floor = PlaceObject(bottomFloorPrefab, transform.position);

        // Side walls 
        leftWall = BuildWall(WallType.Side, leftSnapPoint, -90);
        var leftWallTopSnapPoint = GetSnapPoint(leftWall, new Vector3(0, 1, 0));
        rightWall = BuildWall(WallType.Side, rightSnapPoint, 90);

        // Back wall
        frontWall = BuildWall(WallType.Back, frontSnapPoint, 0);

        // Doorway
        doorway = BuildWall(WallType.Doorway, backSnapPoint, 180);

        // Staircase
        if (!isTopFloorStairwell)
        {
            staircase = PlaceObject(staircasePrefab);
            RotateAroundSnapPoint(staircase, centerSnapPoint, Vector3.up, -90);
            var staircaseSnapPoint = GetSnapPoint(staircase, new Vector3(0, -1, 0));
            SnapToObject(staircase, staircaseSnapPoint, centerSnapPoint);
        }

        // Ceiling
        if (isTopFloorStairwell)
        {
            ceiling = PlaceObject(ceilingPrefab, new Vector3(centerSnapPoint.x, 0, centerSnapPoint.z));
            Vector3 ceilingSnapPoint = GetSnapPoint(ceiling, new Vector3(0, -1, 0));
            SnapToObject(ceiling, ceilingSnapPoint, leftWallTopSnapPoint, SnapAxes.Y);
        }

        // Enumerate pieces
        base.Build();
    }

    public GameObject BuildWall(WallType type, Vector3 floorSnapPoint, float rotation)
    {
        // Choose prefab based on type number
        GameObject prefab;
        switch (type)
        {
            case WallType.Side:
                prefab = isBottomFloorStairwell || isTopFloorStairwell ? sideWallTopFloorPrefab : sideWallPrefab;
                break;
            case WallType.Back:
                prefab = isBottomFloorStairwell || isTopFloorStairwell ? frontWallTopFloorPrefab : frontWallPrefab;
                break;
            case WallType.Doorway:
                prefab = isBottomFloorStairwell || isTopFloorStairwell ? doorWayTopFloorPrefab : doorwayPrefab;
                break;
            default:
                prefab = isBottomFloorStairwell || isTopFloorStairwell ? sideWallTopFloorPrefab : sideWallPrefab;
                break;
        }

        // Place wall object and snap it to the floor
        var wall = PlaceObject(prefab);
        var wallSnapPoint = GetSnapPoint(wall, new Vector3(0, -1, 1));

        RotateAroundSnapPoint(wall, wallSnapPoint, Vector3.up, rotation);
        SnapToObject(wall, wallSnapPoint, floorSnapPoint);

        return wall;
    }

    public override void EnumeratePieces()
    {
        Pieces.Add(floor);
        Pieces.Add(leftWall);
        Pieces.Add(rightWall);
        Pieces.Add(frontWall);
        Pieces.Add(doorway);
        Pieces.Add(staircase);
    }
}
