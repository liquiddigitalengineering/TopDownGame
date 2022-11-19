using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HazardsList", menuName = "Hazards/HazardList")]
public class HazardSO : ScriptableObject
{
    public List<HazardGameObjectInfo> HazardsList;
}

[System.Serializable]
public class HazardGameObjectInfo
{
    public GameObject Prefab;
    public Sprite Shadow;
    public float SpawnChance;
    public float Speed;
}

