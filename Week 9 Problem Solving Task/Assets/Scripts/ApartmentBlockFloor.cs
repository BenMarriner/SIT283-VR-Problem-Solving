using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentBlockFloor : Builder
{
    public GameObject apartmentPrefab;
    public GameObject corridorPrefab;
    public GameObject stairwellPrefab;

    List<List<GameObject>> grid;

    public Vector2Int gridSize = new Vector2Int(2, 5);
    public float gridUnitSize = 5;

    public bool isBottomFloor;

    public override void Build()
    {
        grid = new List<List<GameObject>>();
        for (int x = 0; x < gridSize.x; x++)
        {
            int nextColumnToPlace = x % 3;
            grid.Add(new List<GameObject>());
            for (int y = 0; y < gridSize.y; y++)
            {
                int nextRowToPlace = y % 3;
                switch (nextColumnToPlace, nextRowToPlace)
                {
                    case (0, 0):
                    case (0, 1):
                        if (x == gridSize.x - 1) BuildCorridor(x, y);
                        else BuildApartment(x, y, -90);
                        break;

                    case (0, 2):
                    case (2, 2):
                    case (1, 0):
                    case (1, 1):
                    case (1, 2):
                        BuildCorridor(x, y);
                        break;

                    case (2, 0):
                    case (2, 1):
                        if (x == gridSize.x - 1 && gridSize.x > 3) BuildCorridor(x, y);
                        else BuildApartment(x, y, 90);
                        break;
                }
            }
        }
        //// Add row with stairwells
        //grid.Add(new List<GameObject>());
        //for (int i = 0; i < gridSize.x; i++)
        //{
        //    BuildStairwell(i, gridSize.x - 1, stairwellType);
        //}

        // Enumerate pieces
        base.Build();

        
    }

    //private void BuildStairwell(int x, int y, int type)
    //{
    //    var stairwellPrefabComponent = stairwellPrefab.GetComponent<Stairwell>();

    //    switch (type)
    //    {
    //        case 0:
    //            stairwellPrefabComponent.isTopFloorStairwell = false;
    //            stairwellPrefabComponent.isBottomFloorStairwell = false;
    //            break;
    //        case 1:
    //            stairwellPrefabComponent.isTopFloorStairwell = false;
    //            stairwellPrefabComponent.isBottomFloorStairwell = true;
    //            break;
    //        case 2:
    //            stairwellPrefabComponent.isTopFloorStairwell = true;
    //            stairwellPrefabComponent.isBottomFloorStairwell = false;
    //            break;
    //    }

    //    if (type != -1)
    //    {
    //        grid[x].Add(PlaceObject(stairwellPrefab, GetWorldCoordinatesFromGrid(new Vector2(x, y))));
    //    }
    //}

    private void BuildCorridor(int x, int y)
    {
        var corridorComponent = corridorPrefab.GetComponent<Corridor>();

        corridorComponent.buildFrontWall = y == gridSize.y - 1;
        corridorComponent.buildBackWall = y == 0;
        corridorComponent.buildLeftWall = x == 0;
        corridorComponent.buildRightWall = x == gridSize.x - 1;
        
        corridorComponent.backWallType = (x == 1 && y == 0 && isBottomFloor) ? Corridor.WallType.Door : Corridor.WallType.Window; // Use door type for the entrance to the building
        corridorComponent.frontWallType = Corridor.WallType.Window;
        corridorComponent.leftWallType = Corridor.WallType.Window;
        corridorComponent.rightWallType = Corridor.WallType.Window;


        corridorComponent.floorSize = gridUnitSize;

        grid[x].Add(PlaceObject(corridorPrefab, GetWorldCoordinatesFromGrid(new Vector2(x, y))));
    }

    private void BuildApartment(int x, int y, float rotation)
    {
        apartmentPrefab.GetComponent<Apartment>().dimensions = new Vector2(gridUnitSize, gridUnitSize);
        bool frontWallHasWindow = x == 0 || x == gridSize.x - 2; // If this is the first or last x column, the apartments will have a window
        apartmentPrefab.GetComponent<Apartment>().frontWallIsWindow = frontWallHasWindow;
        grid[x].Add(PlaceObject(apartmentPrefab, GetWorldCoordinatesFromGrid(new Vector2(x, y)), Quaternion.Euler(0, rotation, 0)));
    }

    public override void EnumeratePieces()
    {
        foreach (var column in grid) Pieces.AddRange(column);
    }

    private Vector3 GetWorldCoordinatesFromGrid(Vector2 coords)
    {
        var newCoords = new Vector3()
        {
            x = coords.x * gridUnitSize + transform.position.x,
            y = transform.position.y,
            z = coords.y * gridUnitSize + transform.position.z,
        };

        return newCoords;
    }

    private int GetStructureTypeFromGridSquare(int x, int y)
    {
        if (x < 0 || y < 0 || x >= gridSize.x || y >= gridSize.y) return -1;
        else if (grid[x][y].GetComponent<Apartment>()) return 0;
        else if (grid[x][y].GetComponent<Corridor>()) return 1;
        else return -1;
    }
}
