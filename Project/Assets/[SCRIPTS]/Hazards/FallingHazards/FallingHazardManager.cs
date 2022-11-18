using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingHazardManager : MonoBehaviour
{
    [Header("Time in seconds")]
    [SerializeField] private float timeBeforeSpawn;
    [SerializeField] private Tilemap tilemap;
    private List<Vector3> tileWorldLocations = new List<Vector3>();

    private float timeLeft;

    void Start()
    {
        GetTiles();
        timeLeft = timeBeforeSpawn;
    }

    private void Update() => CalculateTimeBeforeSpawn();

    private void CalculateTimeBeforeSpawn()
    {
        if (timeLeft > 0)
            timeLeft -= Time.deltaTime;
        else {
            timeLeft = timeBeforeSpawn;
        }
    }

    #region Helping Methods
    private Vector3 ChooseRandomCoordinate() => tileWorldLocations[Random.Range(0, tileWorldLocations.Count)];

    private void GetTiles()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin) {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemap.HasTile(localPlace)) {
                tileWorldLocations.Add(localPlace);
            }
        }
    }
    #endregion
}
