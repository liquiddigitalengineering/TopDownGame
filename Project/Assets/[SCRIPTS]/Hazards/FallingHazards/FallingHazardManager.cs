using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingHazardManager : MonoBehaviour
{
    [Header("Time in seconds")]
    [SerializeField] private float timeBeforeSpawn;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private HazardSO hazardSO;
    [SerializeField] private List<GameObject> hazardsGameobjects;
    [Header("Shadow")]
    [SerializeField] private GameObject shadow;
    [SerializeField] private SpriteRenderer shadowSpriteRenderer;
    private List<Vector3> tileWorldLocations = new List<Vector3>();

    private float timeLeft;

    private void OnEnable()
    {
        Hazard.FellEvent += ObjectFell;
    }

    private void OnDisable()
    {
        Hazard.FellEvent -= ObjectFell;
    }

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
            SpawnHazard();
        }
    }

    #region Helping Methods
    private void SpawnHazard()
    {
        HazardGameObjectInfo selectedHazard = SelectedHazard();
        int index = hazardSO.HazardsList.IndexOf(selectedHazard);
        GameObject hazardGameobject = hazardsGameobjects[index];
        Vector2 direction = tileWorldLocations[Random.Range(0, tileWorldLocations.Count)];
        Vector2 fallTowards = direction;
        EnableShadow(fallTowards, selectedHazard.Shadow, hazardGameobject.transform.localScale);
        direction.y += 12;
        Hazard hazardScript = hazardGameobject.GetComponent<Hazard>();
        hazardScript.Speed = selectedHazard.Speed;
        hazardScript.Direction = fallTowards;
        hazardGameobject.transform.position = direction;
        hazardGameobject.SetActive(true);
    }
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

    private HazardGameObjectInfo SelectedHazard()
    {
        List<HazardGameObjectInfo> hazards = new List<HazardGameObjectInfo>();
        float randomNumber = Random.Range(0f, 1f);
        if (randomNumber >= 0.8)
            return hazardSO.HazardsList[3];
        else if (randomNumber < 0.79)
            hazards = hazardSO.HazardsList.FindAll(hazard => hazard.SpawnChance > 0.3);

       return hazards[Random.Range(0, hazards.Count)];
    }

    private void EnableShadow(Vector2 transform, Sprite shadowSprite, Vector3 scale)
    {
        shadowSpriteRenderer.sprite = shadowSprite;
        shadow.transform.position = transform;
        shadow.transform.localScale = scale;
        shadow.SetActive(true);      
    }

    private void ObjectFell()
    {
        shadow.SetActive(false);
    }
    #endregion
}

