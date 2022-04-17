using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private int place = 0;


    public void SpwanSinglePoint()
    {
        if (positions.Count > place) {
            Vector3 newPlace = positions[place];
            // Instantiate(pointPrefab, new Vector3(1,0,1));
            Instantiate(pointPrefab, positions[place], Quaternion.identity);
            place += 1;
        }
    }

    public int GetCount()
    {
        return positions.Count;
    }

    public void AddSpawns(List<Vector3> positions_add)
    {
        positions.AddRange(positions_add);
    }
}
