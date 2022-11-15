using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssaultRifle", menuName = "Weapons/AssaultRifles")]
public class AssaultRifles : WeaponSO
{
    public override void Shoot(Transform spawnPos, Transform targetPos)
    {
        AimingWeaponBullet bullet = BulletPrefab.GetComponent<AimingWeaponBullet>();
        bullet.Speed = BulletSpeed;
        bullet.TargetTransform = targetPos;
        Instantiate(BulletPrefab, spawnPos.position, spawnPos.rotation);
    }
}
