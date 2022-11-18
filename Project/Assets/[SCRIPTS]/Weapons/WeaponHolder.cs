using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private WeaponTypesListSO WeaponList;
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float defAngle;

    private Transform playerTransform;
    private ushort ammos = 1;
    private bool canShoot = true;

    private void Awake()
    {
        this.lineRenderer.enabled = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    public void AssignThings(ushort multiplier, float angle)
    {
        lineRenderer.enabled = false;
        weaponSO = SelectedWeapon();
        ammos = (ushort)(weaponSO.Ammo * multiplier);
        spriteRenderer.sprite = weaponSO.WeaponSprite;
        defAngle = angle;
        canShoot = true;

        transform.GetChild(0).transform.localPosition = weaponSO.Firepoint;
        Vector2 secondChilPos = new Vector2(weaponSO.Firepoint.x - 10, weaponSO.Firepoint.y);
        transform.GetChild(2).transform.localPosition = secondChilPos;

        if (weaponSO.WeaponType != WeaponTypes.laser)
            this.transform.GetChild(1).gameObject.GetComponent<PolygonCollider2D>().enabled = false;  
    }

    private void Update()
    {
        if (!this.gameObject.activeInHierarchy) return;

        Shooting();      
    }

    private void Shooting()
    {
        if (weaponSO.WeaponType == WeaponTypes.aimingWeapon)
            RotateTowardsPlayer();
        else if (weaponSO.WeaponType == WeaponTypes.laser)
            Laser();
        else if (weaponSO.WeaponType == WeaponTypes.assaultRifle)
            AssaultRifle();
        else if(weaponSO.WeaponType == WeaponTypes.shotgun)
            Shotgun();
    }

    #region Weapons
    #region Shotgun
    private void Shotgun()
    {
        if (!canShoot || ammos < 0) return;
        StartCoroutine(ShotgunCoroutine());
    }

    private IEnumerator ShotgunCoroutine()
    {
        ammos--;
        canShoot = false;
        weaponSO.Shoot(this.transform.GetChild(0), this.transform.GetChild(2));
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }
    #endregion
    private List<WeaponInfo> weapons = new List<WeaponInfo>();
    #region Select weapon based on spawn %
    private WeaponSO SelectedWeapon()
    {
        float randomNumber = Random.Range(0f, 1f);

        if (randomNumber >= 0.8 && randomNumber <= 1)
            weapons = WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance <= 0.3 && weapon.SpawnChance >= 0.1); //0.3,0.2,0,1
        else if (randomNumber >= 0.5 && randomNumber < 0.8)
            weapons = WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance < 0.6 && weapon.SpawnChance > 0.4); //0.5
        else if (randomNumber >= 0.4 && randomNumber < 0.5)
            weapons = WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance < 0.7 && weapon.SpawnChance > 0.5); //0.6
        else if (randomNumber >= 0.3 && randomNumber < 0.4)
            weapons = WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance < 0.8 &&weapon.SpawnChance > 0.6); //0.7
        else if (randomNumber < 0.3)
            weapons = WeaponList.WeaponTypesList.FindAll(weapon => weapon.SpawnChance < 1 && weapon.SpawnChance > 0.7); //0.8 & 0.9

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

        if (PlayerController.IsMoving && canShoot && ammos > 0)
            StartCoroutine(ShootingCoroutine());
    }

    private IEnumerator ShootingCoroutine() //used for Aiming weapons
    {
        ammos --;
        canShoot = false;
        weaponSO.Shoot(this.transform.GetChild(0).transform, this.transform.GetChild(2));
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
        if (weaponSO.Sprites.Count > 0) spriteRenderer.sprite = weaponSO.Sprites[1];
        yield return new WaitForSeconds(1.5f);
        EnableLaser();
        ammos --;
        canShoot = false;
        Vector2 dir = (this.transform.GetChild(2).transform.position - this.transform.GetChild(0).transform.position);
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, transform.GetChild(0).transform.position);
        lineRenderer.SetPosition(1, dir);

        yield return new WaitForSeconds(weaponSO.LaserTime);
        lineRenderer.enabled = false;
        this.transform.GetChild(1).gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        if (weaponSO.Sprites.Count > 0) spriteRenderer.sprite = weaponSO.Sprites[0];
        
        yield return new WaitForSeconds(2f);
        canShoot = true;    
    }
    private void EnableLaser()
    {
        GameObject laserObject = this.transform.GetChild(1).gameObject;
        laserObject.GetComponent<PolygonCollider2D>().enabled = true;
        laserObject.GetComponent<Laser>().LaserEnabled();
    }
    #endregion

    #region AssaultRiflex
    private void AssaultRifle()
    {
        if (!canShoot || ammos <= 0) return;

        
        StartCoroutine(AssaultRifleCoroutine());
    }

    private float newAngle;
    private IEnumerator AssaultRifleCoroutine()
    {
        if (weaponSO.Sprites.Count > 0) spriteRenderer.sprite = weaponSO.Sprites[1];
        canShoot = false;
        newAngle = defAngle;
        newAngle += Random.Range(-10, 10);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
        yield return new WaitForSeconds(1f);    
        weaponSO.Shoot(this.transform.GetChild(0), this.transform.GetChild(2));
        if (weaponSO.Sprites.Count > 0) spriteRenderer.sprite = weaponSO.Sprites[0];
        yield return new WaitForSeconds(2f);
        canShoot = true;
        newAngle = defAngle;     
    }
    #endregion
    #endregion  
}
