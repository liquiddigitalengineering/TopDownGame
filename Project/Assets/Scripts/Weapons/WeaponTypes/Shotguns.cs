using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Weapons/Shotguns")]
public class Shotguns : WeaponSO
{
    public ushort MiniVersions;

    public override void Shoot(Transform spawnPos, Transform targetPos)
    {
       
    }

    public override void Shoot(Transform spawnPos, float angle)
    {
        ShotgunBullet bullet = BulletPrefab.GetComponent<ShotgunBullet>();
        bullet.Speed = BulletSpeed;
        bullet.Angle = angle;
        bullet.MiniVersions = MiniVersions;
        Instantiate(BulletPrefab, spawnPos.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
