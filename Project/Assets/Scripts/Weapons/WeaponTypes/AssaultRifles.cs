using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssaultRifle", menuName = "Weapons/AssaultRifles")]
public class AssaultRifles : WeaponSO
{
    public override void Shoot(Transform spawnPos, Transform targetPos, float angle)
    {
        AimingWeaponBullet bullet = BulletPrefab.GetComponent<AimingWeaponBullet>();
        bullet.Speed = BulletSpeed;
        bullet.Angle = angle;
        bullet.targetTransform = targetPos;
        Instantiate(BulletPrefab, spawnPos.position, spawnPos.rotation);
    }

    public override void Shoot(Transform spawnPos, float angle)
    {
        AimingWeaponBullet bullet = BulletPrefab.GetComponent<AimingWeaponBullet>();
        bullet.Speed = BulletSpeed;
        bullet.Angle = angle;
        bullet.targetTransform = spawnPos;
        Instantiate(BulletPrefab, spawnPos.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
