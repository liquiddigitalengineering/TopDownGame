using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    aimingWeapon, shotgun, laser, assaultRifle
}

public abstract class WeaponSO : ScriptableObject
{
    public Sprite WeaponSprite;
    public WeaponTypes WeaponType;
    [Space(10)]
    public GameObject BulletPrefab;
    public ushort Ammo;
    public ushort BulletSpeed;
    [Space(10)]
    public ushort LaserTime;

    public abstract void Shoot(Transform spawnPos, Transform targetPos, float angle);
    public abstract void Shoot(Transform spawnPos, float angle);
}
