using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Weapons/Shotguns")]
public class Shotguns : WeaponSO
{
    public ushort MiniVersions;

    public override void Shoot(Transform spawnPos, Transform targetPos)
    {
        ShotgunBullet bullet = BulletPrefab.GetComponent<ShotgunBullet>();
        bullet.Speed = BulletSpeed;
        bullet.MiniVersions = MiniVersions;
        bullet.Target = targetPos;
        Instantiate(BulletPrefab, spawnPos.position, spawnPos.rotation);
    }
}
