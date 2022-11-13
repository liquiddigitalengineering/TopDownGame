using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser", menuName ="Weapons/Lasers")]
public class Lasers : WeaponSO
{
    public override void Shoot(Transform spawnPos, Transform targetPos)
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot(Transform spawnPos, float angle)
    {
        throw new System.NotImplementedException();
    }
}
