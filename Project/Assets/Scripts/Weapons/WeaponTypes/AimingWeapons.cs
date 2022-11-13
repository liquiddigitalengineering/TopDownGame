using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AimingWeapon", menuName = "Weapons/AimingWeapons")]
public class AimingWeapons : WeaponSO
{
    public override void Shoot(Transform spawnPos, float angle)
    {
        AimingWeaponBullet bullet = BulletPrefab.GetComponent<AimingWeaponBullet>();
        bullet.Speed = BulletSpeed;
        bullet.Angle = angle;
        Instantiate(BulletPrefab, spawnPos.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }

    public override void Shoot(Transform spawnPos, Transform targetPos, float angle)
    {
        throw new System.NotImplementedException();
    }
}
