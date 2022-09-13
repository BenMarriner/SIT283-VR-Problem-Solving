using UnityEngine;

public class Apartment : Builder
{
    public GameObject floor1mPrefab;
    public GameObject floor2mPrefab;
    public GameObject wall1mPrefab;
    public GameObject wall2mPrefab;
    public GameObject wall4mPrefab;
    public GameObject wallDoorway1mPrefab;
    public GameObject wallDoorway2mPrefab;
    public GameObject wallDoorway4mPrefab;
    public GameObject wallWindow1mPrefab;
    public GameObject wallWindow2mPrefab;
    public GameObject wallWindow4mPrefab;

    public Vector2 dimensions = new Vector2(5, 5);

    [HideInInspector]
    public GameObject floor;
    [HideInInspector]
    public GameObject[] sideWalls;
    [HideInInspector]
    public GameObject frontWall;
    [HideInInspector]
    public GameObject[] backWalls;
    [HideInInspector]
    public GameObject ceiling;

    public override void EnumeratePieces()
    {
        Pieces.AddRange(sideWalls);
        Pieces.AddRange(backWalls);
        Pieces.Add(floor);
        Pieces.Add(ceiling);
        Pieces.Add(frontWall);
    }

    public override void Build()
    {
        // Floor
        floor = BuildFloor(dimensions.x, dimensions.y);
        var floorLeftSnapPoint = GetSnapPoint(floor, new Vector3(-1, 1, 0));
        var floorRightSnapPoint = GetSnapPoint(floor, new Vector3(1, 1, 0));
        var floorFrontSnapPoint = GetSnapPoint(floor, new Vector3(0, 1, 1));
        var floorBackSnapPoint = GetSnapPoint(floor, new Vector3(0, 1, -1));

        // Side walls
        sideWalls = new GameObject[2];
        sideWalls[0] = BuildWall(wall1mPrefab, dimensions.y);
        sideWalls[1] = BuildWall(wall1mPrefab, dimensions.y);

        var leftWallSnapPoint = GetSnapPoint(sideWalls[0], new Vector3(0, -1, 1));
        var rightWallSnapPoint = GetSnapPoint(sideWalls[1], new Vector3(0, -1, 1));
        var leftWallTopSnapPoint = GetSnapPoint(sideWalls[0], new Vector3(0, 1, 0));

        SnapToObject(sideWalls[0], leftWallSnapPoint, floorLeftSnapPoint);
        SnapToObject(sideWalls[1], rightWallSnapPoint, floorRightSnapPoint);

        RotateAroundSnapPoint(sideWalls[0], floorLeftSnapPoint, Vector3.up, -90);
        RotateAroundSnapPoint(sideWalls[1], floorRightSnapPoint, Vector3.up, 90);

        // Front wall
        frontWall = BuildWall(wallWindow1mPrefab, dimensions.x);
        var frontWallSnapPoint = GetSnapPoint(frontWall, new Vector3(0, -1, 1));

        SnapToObject(frontWall, frontWallSnapPoint, floorFrontSnapPoint);

        // Back wall
        backWalls = new GameObject[3];
        backWalls[0] = BuildWall(wallDoorway1mPrefab, 1.0f);
        backWalls[1] = BuildWall(wall1mPrefab, (dimensions.x - 1.0f) / 2.0f);
        backWalls[2] = BuildWall(wallWindow1mPrefab, (dimensions.x - 1.0f) / 2.0f);
        
        var backWallSnapPoints = new Vector3[3];
        backWallSnapPoints[0] = GetSnapPoint(backWalls[0], new Vector3(0, -1, -1));
        backWallSnapPoints[1] = GetSnapPoint(backWalls[1], new Vector3(-1, -1, -1));
        backWallSnapPoints[2] = GetSnapPoint(backWalls[2], new Vector3(1, -1, -1));

        SnapToObject(backWalls[0], backWallSnapPoints[0], floorBackSnapPoint);
        SnapToObject(backWalls[1], backWallSnapPoints[1], floorBackSnapPoint, SnapAxes.Z);
        SnapToObject(backWalls[2], backWallSnapPoints[2], floorBackSnapPoint, SnapAxes.Z);

        SnapToObject(backWalls[1], backWallSnapPoints[1], floorLeftSnapPoint, SnapAxes.X);
        SnapToObject(backWalls[2], backWallSnapPoints[2], floorRightSnapPoint, SnapAxes.X);

        // Ceiling
        ceiling = BuildFloor(dimensions.x, dimensions.y);

        var ceilingSnapPoint = GetSnapPoint(ceiling, new Vector3(0, -1, 0));

        SnapToObject(ceiling, ceilingSnapPoint, leftWallTopSnapPoint, SnapAxes.Y);

        // Enumerate pieces
        base.Build();
    }

    public GameObject BuildFloor(float length, float width)
    {
        var floor = PlaceObject(floor1mPrefab);
        floor.transform.localScale = new Vector3(length, 1, width);

        return floor;
    }

    public GameObject BuildDoorway(float length)
    {
        return BuildWall(wallDoorway1mPrefab, length);
    }

    public GameObject BuildWindow(float length)
    {
        return BuildWall(wallWindow1mPrefab, length);
    }

    public GameObject BuildWall(GameObject wallPrefab, float length)
    {
        var wall = PlaceObject(wallPrefab);
        wall.transform.localScale = new Vector3(length, wall.transform.localScale.y, wall.transform.localScale.z);

        return wall;
    }

    //public void BuildFloor(int length, int width)
    //{
    //    if (length == 0 || width == 0) return;

    //    var floorBlocks = new GameObject[length, width];

    //    for (int x = 0; x < length; x++)
    //    {
    //        for (int y = 0; y < width; y++)
    //        {
    //            if (x == 0 && y == 0)           floorBlocks[x, y] = PlaceObject(floor1mPrefab);
    //            else if (!floorBlocks[x, 0])    floorBlocks[x, y] = StackObjectNextTo(floor1mPrefab, floorBlocks[x - 1, 0], Vector3.right);
    //            else                            floorBlocks[x, y] = StackObjectNextTo(floor1mPrefab, floorBlocks[x, y - 1], Vector3.forward);
    //        }
    //    }
    //}

    //public void BuildWindow(int length)
    //{
    //    int[] wallLengths = { 4, 2, 1 };
    //    int[] numWalls = { 0, 0, 0 };

    //    var walls = new GameObject[numWalls[0] + numWalls[1] + numWalls[2]];

    //    // Evaluate how many of which wall lengths we need
    //    for (int i = 0; i < wallLengths.Length; i++)
    //    {
    //        int remainingLength = length;
    //        int currEvaluatingWallLength = wallLengths[i];

    //        while (remainingLength >= currEvaluatingWallLength)
    //        {
    //            if (remainingLength - currEvaluatingWallLength >= currEvaluatingWallLength)
    //            {
    //                numWalls[i]++;
    //            }
    //            remainingLength -= currEvaluatingWallLength;
    //        }
    //    }

    //    for (int i = 0; i < numWalls.Length; i++)
    //    {
    //        for (int j = 0; j < numWalls[i]; j++)
    //        {
    //            switch (i)
    //            {
    //                case 0:

    //            }
    //        }
    //    }
    //}
}