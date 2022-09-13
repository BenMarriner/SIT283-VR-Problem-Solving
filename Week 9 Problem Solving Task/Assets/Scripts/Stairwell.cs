using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairwell : Builder
{
    public GameObject staircasePrefab;
    public GameObject sideWallPrefab;
    public GameObject sideWallTopFloorPrefab;
    public GameObject backWallPrefab;
    public GameObject backWallTopFloorPrefab;
    public GameObject doorwayPrefab;
    public GameObject doorWayTopFloorPrefab;
    public GameObject bottomFloorPrefab;
    public GameObject ceilingPrefab;

    public int numFloors;

    public GameObject floor;
    public GameObject[] leftSideWalls;
    public GameObject[] rightSideWalls;
    public GameObject[] backWalls;
    public GameObject[] doorways;
    public GameObject[] staircases;

    public override void EnumeratePieces()
    {
        Pieces.Add(floor);
        Pieces.AddRange(leftSideWalls);
        Pieces.AddRange(rightSideWalls);
        Pieces.AddRange(backWalls);
        Pieces.AddRange(doorways);
        Pieces.AddRange(staircases);
    }

    // Build stairwell
    public override void Build()
    {
        if (numFloors == 0 || numFloors == 1) return;


        // Floor
        floor = PlaceObject(bottomFloorPrefab, transform.position);
        Vector3 floorLeftSnapPoint = GetSnapPoint(floor, Vector3.left);
        Vector3 floorRightSnapPoint = GetSnapPoint(floor, Vector3.right);
        Vector3 floorForwardSnapPoint = GetSnapPoint(floor, Vector3.forward);
        Vector3 floorBackSnapPoint = GetSnapPoint(floor, Vector3.back);
        Vector3 floorCenterSnapPoint = GetSnapPoint(floor, Vector3.zero);


        // Side wall
        Vector3 sideWallSnapPointDirection = Vector3.back;
        int numSideWalls = numFloors % 2 == 0 ? numFloors : (numFloors + 1) / 2;

        leftSideWalls = BuildSideWall(floorLeftSnapPoint, sideWallSnapPointDirection, numSideWalls, 90);
        rightSideWalls = BuildSideWall(floorRightSnapPoint, sideWallSnapPointDirection, numSideWalls, -90);

        // Back wall
        Vector3 backWallSnapPointDirection = Vector3.forward;
        int numBackWalls = numFloors % 2 == 0 ? numFloors / 2 : (numFloors + 1) / 2;
        backWalls = BuildBackWall(floorForwardSnapPoint, backWallSnapPointDirection, numBackWalls);

        // Doorways
        Vector3 doorwaySnapPointDirection = Vector3.back;
        int numDoorways = numFloors % 2 == 0 ? numFloors / 2 : (numFloors + 1) / 2;
        doorways = BuildDoorway(floorBackSnapPoint, doorwaySnapPointDirection, numDoorways);

        // Staircase
        Vector3 staircaseSnapPointDirection = Vector3.zero;
        int numStaircases = numFloors % 2 == 0 ? numFloors : (numFloors - 1);
        staircases = BuildStaircase(floorCenterSnapPoint, staircaseSnapPointDirection, numStaircases, -90);

        // Ceiling
        //var ceiling = PlaceObject(ceilingPrefab, transform.position);

        // Enumerate pieces
        base.Build();
    }

    public GameObject[] BuildSideWall(Vector3 snapPoint, Vector3 subPartSnapPointDirection, int numWalls, float rotation = 0)
    {
        List<GameObject> walls = new List<GameObject>(); 
        walls.AddRange(BuildPart(sideWallPrefab, snapPoint, subPartSnapPointDirection, numWalls - 1, rotation));
        
        if (numWalls % 2 != 0)
        {
            var topFloorWall = PlaceObject(sideWallTopFloorPrefab);
            Vector3 sideWallTopFloorSnapPoint = GetSnapPoint(topFloorWall, Vector3.forward);
            var prevWall = walls[walls.Count - 1];
            Vector3 prevSWallSnapPoint = GetSnapPoint(prevWall, new Vector3(0, 1, 1));

            //RotateAroundSnapPoint(topFloorWall, sideWallTopFloorSnapPoint, Vector3.up, rotation);
            //SnapToObject(topFloorWall, sideWallTopFloorSnapPoint, prevSWallSnapPoint);

            StackObjectNextTo(sideWallTopFloorPrefab, prevWall, Vector3.up);
        }

        return walls.ToArray();
    }
    private GameObject[] BuildBackWall(Vector3 snapPoint, Vector3 subPartSnapPointDirection, int numWalls, float rotation = 0)
    {
        return BuildPart(backWallPrefab, snapPoint, subPartSnapPointDirection, numWalls, rotation);
    }

    private GameObject[] BuildStaircase(Vector3 snapPoint, Vector3 subPartSnapPointDirection, int numStaircases, float rotation = 0)
    {
        return BuildPart(staircasePrefab, snapPoint, subPartSnapPointDirection, numStaircases, rotation);
    }

    private GameObject[] BuildDoorway(Vector3 snapPoint, Vector3 subPartSnapPointDirection, int numDoorways, float rotation = 0)
    {
        return BuildPart(doorwayPrefab, snapPoint, subPartSnapPointDirection, numDoorways, rotation);
    }
}
