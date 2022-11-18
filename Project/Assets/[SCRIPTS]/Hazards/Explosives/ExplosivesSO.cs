using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExplosiveEnum { Grenade, TNT};
[CreateAssetMenu(fileName ="Explosives list", menuName ="Hazards/Explosives/ExplosiveList")]
public class ExplosivesSO : ScriptableObject
{
    public List<ExplosivesInfo> Explosives;
}

[System.Serializable]
public class ExplosivesInfo
{
    public GameObject Prefab;
    public ExplosiveEnum ExplosiveType;
    public byte Amount;
}
