using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AimingWeapon", menuName = "Weapons/AimingWeapons")]
public class AimingWeapons : WeaponSO
{
    public override void Shoot(Transform spawnPos, float angle)
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot(Transform spawnPos, Transform targetPos)
    {
        throw new System.NotImplementedException();
    }
}
