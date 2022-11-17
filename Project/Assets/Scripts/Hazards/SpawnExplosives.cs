using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnExplosives : MonoBehaviour
{
    [Header("Time in seconds")]
    [SerializeField] private float timeBeforeSpawn;
    [Space(10)]
    [SerializeField] private GameObject centerPoint;
    [SerializeField] private ExplosivesSO explosivesSO;
    [SerializeField] private List<Transform> spawnTransforms;

    private float timeLeft;
    private ExplosiveEnum explosiveType;

    private void Awake()
    {
        timeLeft = timeBeforeSpawn;
    }

    private void Update() => CalculateTimeBeforeSpawn();

    private void CalculateTimeBeforeSpawn()
    {   
        if(timeLeft > 0)
            timeLeft -= Time.deltaTime;
        else {
            timeLeft = timeBeforeSpawn;   
            InstantiateExplosives();
        }
    }

    #region private methods
    private GameObject SelectedPrefab()
    {
        float randomNumber = Random.Range(0f, 1f);
        
        if (randomNumber >= 0.42) {
            explosiveType = ExplosiveEnum.Grenade;
            return explosivesSO.Explosives[0].Prefab;
        }
        else {
            explosiveType = ExplosiveEnum.TNT;
            return explosivesSO.Explosives[1].Prefab;
        }
    }
    private void InstantiateExplosives()
    {
        GameObject prefab = SelectedPrefab();

        if (explosiveType == ExplosiveEnum.Grenade) {
            for (int i = 0; i < explosivesSO.Explosives[0].Amount; i++) {
                prefab.GetComponent<Grenade>().Direction = centerPoint.transform;
                Instantiate(prefab, RandomTransform().position, Quaternion.identity);
            }
        }
        else {
            for (int i = 0; i < explosivesSO.Explosives[1].Amount; i++) {
                prefab.GetComponent<Tnt>().Direction = centerPoint.transform;
                Instantiate(prefab, RandomTransform().position, Quaternion.identity);
            }
        }
    }
    private Transform RandomTransform() => spawnTransforms[Random.Range(0, spawnTransforms.Count)];
    #endregion
}


