using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentBlockFloor : Builder
{
    public GameObject apartmentPrefab;
    public GameObject corridorFloorPrefab;

    List<List<GameObject>> grid;

    public Vector2Int gridSize = new Vector2Int(2, 5);
    public float gridUnitSize = 5;

    public override void Build()
    {
        int nextRowToPlace = 0;
        int nextColumnToPlace = 0;

        grid = new List<List<GameObject>>();
        for (int x = 0; x < gridSize.x; x++)
        {
            grid.Add(new List<GameObject>());
            for (int y = 0; y < gridSize.y; y++)
            {
                switch (nextColumnToPlace, nextRowToPlace)
                {
                    case (0, 0): 
                    case (0, 1):
                        apartmentPrefab.GetComponent<Apartment>().dimensions = new Vector2(gridUnitSize, gridUnitSize);
                        grid[x].Add(PlaceObject(apartmentPrefab, GetWorldCoordinatesFromGrid(new Vector2(x, y)), Quaternion.Euler(0, -90, 0)));
                        break;

                    case (0, 2):
                    case (2, 2):
                    case (1, 0):
                    case (1, 1):
                    case (1, 2):
                        grid[x].Add(PlaceObject(corridorFloorPrefab, GetWorldCoordinatesFromGrid(new Vector2(x, y))));
                        grid[x][y].transform.localScale = new Vector3(gridUnitSize, 1.0f, gridUnitSize);
                        break;

                    case (2, 0):
                    case (2, 1):
                        apartmentPrefab.GetComponent<Apartment>().dimensions = new Vector2(gridUnitSize, gridUnitSize);
                        grid[x].Add(PlaceObject(apartmentPrefab, GetWorldCoordinatesFromGrid(new Vector2(x, y)), Quaternion.Euler(0, 90, 0)));
                        break;
                }
                nextRowToPlace = y % 3;
            }
            nextColumnToPlace = x % 3;
        }

        // Enumerate pieces
        base.Build();
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
