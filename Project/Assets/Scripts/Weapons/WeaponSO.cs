using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSO : ScriptableObject
{
    public Sprite WeaponSprite;
    public GameObject BulletPrefab;
    public ushort Ammo;
    public ushort BulletSpeed;
    public bool IsFocusingPlayer = false;

    public abstract void Shoot();
    public abstract void Shoot(Transform target);
}
