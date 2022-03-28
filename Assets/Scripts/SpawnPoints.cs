using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public GameObject pointPrefab;
    public List<Vector3> positions;
    private int place = 0;

    public void SpwanSinglePoint()
    {
        if (positions.Count > place) {
            Vector3 newPlace = positions[place];
            // Instantiate(pointPrefab, new Vector3(1,0,1));
            Instantiate(pointPrefab, positions[place], Quaternion.identity);
            place += 1;
        }
    }
}
