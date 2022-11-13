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
        Instantiate(BulletPrefab, spawnPos.position, spawnPos.rotation);
    }

    public override void Shoot(Transform spawnPos, Transform targetPos, float angle)
    {
        AimingWeaponBullet bullet = BulletPrefab.GetComponent<AimingWeaponBullet>();
        bullet.Speed = BulletSpeed;
        bullet.Angle = angle;
        bullet.targetTransform = targetPos;
        Instantiate(BulletPrefab, spawnPos.position, spawnPos.rotation);
    }
}
