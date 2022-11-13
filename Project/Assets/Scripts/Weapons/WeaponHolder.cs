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
    [SerializeField] private LineRenderer lineRenderer;

    private float defAngle;

    private SpriteRenderer spriteRenderer;  
    private float timeBetweenShooting = 2f;
    private float timeLeft;

    private Transform playerTransform;
    private ushort ammos = 1;


    private bool canShoot = true;

    private void OnEnable()
    {
        //weaponSO = SelectedWeapon();
        ammos = weaponSO.Ammo;
        spriteRenderer.sprite = weaponSO.WeaponSprite;
        timeLeft = timeBetweenShooting;
        defAngle = Angle;
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.transform;

        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void Update()
    {
        if (!this.gameObject.activeInHierarchy) return;

        if (weaponSO.WeaponType == WeaponTypes.aimingWeapon)
            RotateTowardsPlayer();
        else if (weaponSO.WeaponType == WeaponTypes.laser)
            Laser();
        else if (weaponSO.WeaponType == WeaponTypes.assaultRifle)
            AssaultRifle();
        else
            CalculateTimeBetweenShots();
       
    }

    private void CalculateTimeBetweenShots()
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

    #region Aiming weapons
    private void RotateTowardsPlayer()
    {
        Vector3 targ = transform.position;
        targ.z = 0f;

        Vector3 objectPos = playerTransform.position;
        targ.x -= objectPos.x;
        targ.y -= objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Weapon.IsNotMoving && canShoot && ammos > 0)
            StartCoroutine(ShootingCoroutine());
    }

    private IEnumerator ShootingCoroutine() //used for Aiming weapons
    {
        ammos --;
        canShoot = false;
        weaponSO.Shoot(this.transform, Angle);
        yield return new WaitForSeconds(1.5f);
        canShoot = true;
    }
    #endregion

    #region Lasers
    private void Laser()
    {
        if (!canShoot || ammos <= 0) return;

        StartCoroutine(LaserCoroutine());
    }

    private IEnumerator LaserCoroutine()
    {
        ammos --;
        canShoot = false;
        Vector2 dir = (this.transform.GetChild(2).transform.position - this.transform.GetChild(0).transform.position);
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.GetChild(0).transform.position);
        lineRenderer.SetPosition(1, dir);

        yield return new WaitForSeconds(weaponSO.LaserTime);
        lineRenderer.enabled = false;

        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
    #endregion

    #region AssaultRiflex
    private void AssaultRifle()
    {
        if (!canShoot || ammos <= 0) return;

        StartCoroutine(AssaultRifleCoroutine());
    }

    private IEnumerator AssaultRifleCoroutine()
    {
        canShoot = false;
        defAngle += Random.Range(-25, 25);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, defAngle));
        yield return new WaitForSeconds(1f);
       
        weaponSO.Shoot(this.transform, this.transform.GetChild(2).transform, defAngle);
        yield return new WaitForSeconds(2f);
        canShoot = true;
        defAngle = Angle;
    }
    #endregion
}
