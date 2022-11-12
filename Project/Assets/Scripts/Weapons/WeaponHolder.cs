using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public WeaponTypesListSO WeaponList;

    private SpriteRenderer spriteRenderer;

    private WeaponSO weaponSO;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponSO = SelectedWeapon();

        spriteRenderer.sprite = weaponSO.WeaponSprite;

    }

    private WeaponSO SelectedWeapon()
    {
        List<WeaponInfo> weapons;

        byte randomNumber = (byte)Random.Range(0, 1);

        if (randomNumber >= 0.8)
            weapons =  WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance >= 0.8);
        else if (randomNumber >= 0.5)
            weapons =  WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance >= 0.5);
        else if (randomNumber >= 0.4)
           weapons =  WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance >= 0.4);
        else if (randomNumber >= 0.3)
            weapons = WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance >= 0.3);
        else if (randomNumber >= 0.2)
            weapons = WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance >= 0.2);
        else
            weapons = WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance >= 0.1);

        if (weapons.Count == 1) return weapons[0].WeaponSO;
        else return weapons[Random.Range(0, weapons.Count)].WeaponSO;
    }
}
