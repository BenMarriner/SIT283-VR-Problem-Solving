using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentBlock : Builder
{
    public GameObject apartmentBlockFloorPrefab;
    public int numFloors = 2;
    public Vector2Int gridSize = new Vector2Int(2, 5);
    public float gridUnitSize = 5;

    List<GameObject> floors = new List<GameObject>();
    
    public override void Build()
    {
        for (int i = 0; i < numFloors; i++)
        {
            //// Stairwells
            //if (numFloors == 1)
            //{
            //    apartmentBlockFloorPrefab.GetComponent<ApartmentBlockFloor>().stairwellType = 0;
            //}
            //else if (i == 0)
            //{
            //    apartmentBlockFloorPrefab.GetComponent<ApartmentBlockFloor>().stairwellType = 1;
            //}
            //else if (i == numFloors - 1)
            //{
            //    apartmentBlockFloorPrefab.GetComponent<ApartmentBlockFloor>().stairwellType = 2;
            //}
            //else
            //{
            //    apartmentBlockFloorPrefab.GetComponent<ApartmentBlockFloor>().stairwellType = 0;
            //}

            // Build floor
            var apartmentBlockFloorPrefabComponent = apartmentBlockFloorPrefab.GetComponent<ApartmentBlockFloor>();

            apartmentBlockFloorPrefabComponent.gridSize = gridSize;
            apartmentBlockFloorPrefabComponent.gridUnitSize = gridUnitSize;
            if (i == 0) apartmentBlockFloorPrefabComponent.isBottomFloor = true;
            else apartmentBlockFloorPrefabComponent.isBottomFloor = false;

            if (numFloors == 1)
            {
                floors.Add(PlaceObject(apartmentBlockFloorPrefab, transform.position));
            }
            else
            {
                if (i == 0)
                {
                    floors.Add(PlaceObject(apartmentBlockFloorPrefab, transform.position));
                }
                else
                {
                    floors.Add(PlaceObject(apartmentBlockFloorPrefab, floors[i - 1].transform.position));
                    var prevFloor = floors[i - 1];
                    var prevFloorSnapPoint = Vector3.Scale(prevFloor.GetComponent<Builder>().bounds.size, new Vector3(0, 1, 0)) + prevFloor.transform.position;
                    floors[i].transform.position = prevFloorSnapPoint;
                }
            }
        }

        // Enumerate pieces
        base.Build();
    }

    public override void EnumeratePieces()
    {
        Pieces.AddRange(floors);
    }
}
