using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [HideInInspector]
    public float Angle;
    [SerializeField] private WeaponTypesListSO WeaponList;
    [SerializeField] private WeaponSO weaponSO;
    
    private SpriteRenderer spriteRenderer;  
    private float timeBetweenShooting = 4f;
    private float timeLeft;

    private Transform playerTransform;
    private ushort ammos = 1;

    private void OnEnable()
    {
        weaponSO = SelectedWeapon();
        ammos = weaponSO.Ammo;
        spriteRenderer.sprite = weaponSO.WeaponSprite;
        timeLeft = timeBetweenShooting;
    }

    private void Start()
    {
        //playerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.transform;

        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void Update()
    {
        if (!this.gameObject.activeInHierarchy) return;
        
        CalculateTime();
    }

    private void CalculateTime()
    {
        if(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
        }
        else if(timeLeft <= 0 && ammos > 0) {
            timeLeft = timeBetweenShooting;
            ammos--;
            weaponSO.Shoot(this.transform.GetChild(0).gameObject.transform, Angle);
        }
    }

    #region Select weapon based on spawn %
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
    #endregion
}
