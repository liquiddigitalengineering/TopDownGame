using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="WeaponList", menuName = "Weapons/List")]
public class WeaponTypesListSO : ScriptableObject
{
    public List<WeaponInfo> WeaponTypesList = new List<WeaponInfo>();
}

[System.Serializable]
public class WeaponInfo
{
    public WeaponSO WeaponSO;
    public float SpawnChance;
}
