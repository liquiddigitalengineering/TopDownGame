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
    public GameObject BulletPrefab;
    public ushort Ammo;
    public ushort BulletSpeed;
    public bool IsFocusingPlayer = false;
    public WeaponTypes WeaponType;

    public abstract void Shoot(Transform spawnPos, Transform targetPos, float angle);
    public abstract void Shoot(Transform spawnPos, float angle);
}
