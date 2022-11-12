using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Weapons/Shotguns")]
public class Shotguns : WeaponSO
{
    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot(Transform target)
    {
        throw new System.NotImplementedException();
    }
}
