using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentBlockFloor : Builder
{
    public GameObject apartmentPrefab;
    public GameObject corridorFloorPrefab;

    List<List<List<GameObject>>> grid;

    public Vector2Int gridSize = new Vector2Int(2, 5);
    public float gridUnitSize = 5;

    private const float ceilingHeight = 2.8f;

    public override void Build()
    {

        grid = new List<List<List<GameObject>>>();
        for (int x = 0; x < gridSize.x; x++)
        {
            int nextColumnToPlace = x % 3;
            grid.Add(new List<List<GameObject>>());
            for (int y = 0; y < gridSize.y; y++)
            {
                int nextRowToPlace = y % 3;
                grid[x].Add(new List<GameObject>());
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

        // Enumerate pieces
        base.Build();
    }

    private void BuildCorridor(int x, int y)
    {
        grid[x][y].Add(PlaceObject(corridorFloorPrefab, GetWorldCoordinatesFromGrid(new Vector2(x, y))));
        var corridorFloor = grid[x][y][0];
        corridorFloor.transform.localScale = new Vector3(gridUnitSize, 1.0f, gridUnitSize);

        grid[x][y].Add(PlaceObject(corridorFloorPrefab, GetWorldCoordinatesFromGrid(new Vector2(x, y))));
        var corridorCeiling = grid[x][y][1];
        var corridorCeilingSnapPoint = GetSnapPoint(corridorCeiling, Vector3.down);
        //SnapTo()
    }

    private void BuildApartment(int x, int y, float rotation)
    {
        apartmentPrefab.GetComponent<Apartment>().dimensions = new Vector2(gridUnitSize, gridUnitSize);
        bool frontWallHasWindow = x == 0 || x == gridSize.x - 2; // If this is the first or last x column, the apartments will have a window
        apartmentPrefab.GetComponent<Apartment>().frontWallIsWindow = frontWallHasWindow;
        grid[x][y].Add(PlaceObject(apartmentPrefab, GetWorldCoordinatesFromGrid(new Vector2(x, y)), Quaternion.Euler(0, rotation, 0)));
    }

    public override void EnumeratePieces()
    {
        //foreach (var row in apartments) pieces.AddRange(row);
    }

    private Vector3 GetWorldCoordinatesFromGrid(Vector2 coords)
    {
        var newCoords = new Vector3()
        {
            x = coords.x * gridUnitSize,
            y = 0,
            z = coords.y * gridUnitSize,
        };

        return newCoords;
    }
}
