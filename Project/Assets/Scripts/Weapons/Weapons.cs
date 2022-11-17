using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponPlaces;
    [SerializeField] private GameObject middlePoints;

    private ushort startEnabledTarget = 1;
    private ushort multiplier = 1;

    private void OnEnable()
    {
        Timer.NewWeaponWaveEvent += EnableRandomWeapons;
        PlayerController.PlayerDiedEvent += PlayerDied;
    }

    private void OnDisable()
    {
        Timer.NewWeaponWaveEvent -= EnableRandomWeapons;
        PlayerController.PlayerDiedEvent -= PlayerDied;
    }
    private void Awake()
    {
        DisableLasers();
        EnableRandomWeapons(startEnabledTarget);
    }
  
    private void EnableRandomWeapons(int waveNumber)
    {
        DisableAllWeapons();
       
        if(startEnabledTarget < 8) startEnabledTarget = CalculateWave(waveNumber);

        for (ushort i = 0; i < startEnabledTarget; i++) {
            byte randomNumber = (byte)Random.Range(0, weaponPlaces.Count);
            GameObject weapon = weaponPlaces[randomNumber];

            if (weaponPlaces[randomNumber].activeInHierarchy) {
                i--;
                continue;
            }

            float angle = CalculatedRotation(weapon);

            weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            weapon.GetComponent<WeaponHolder>().AssignThings(multiplier, angle);
            weapon.transform.GetChild(1).gameObject.SetActive(true);
            weapon.SetActive(true);
        }
    }

    private ushort CalculateWave(int waveNumber) => waveNumber % 2 == 0 ? startEnabledTarget += 1 : startEnabledTarget ;
   
    #region Calculating rotation of the weapon
    private float CalculatedRotation(GameObject weapon)
    {      
        Vector3 targ = weapon.transform.GetChild(0).transform.position;
        targ.z = 0f;

        Vector3 objectPos = middlePoints.transform.position;
        targ.x -= objectPos.x;
        targ.y -= objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        angle += Random.Range(-15f, 15f);
        return angle;
    }
    #endregion

    #region private methods
    private void DisableLasers()
    {
        for (int i = 0; i < weaponPlaces.Count; i++) {
            weaponPlaces[i].transform.GetChild(1).GetComponent<LineRenderer>().enabled = false;
            weaponPlaces[i].transform.GetChild(1).GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
    private void DisableAllWeapons()
    {
        for (int i = 0; i < weaponPlaces.Count; i++)
            weaponPlaces[i].SetActive(false);
    }

    private void PlayerDied()
    {
        DisableAllWeapons();
        this.gameObject.SetActive(false);
    }
    #endregion

    #region Player's first miss
    private List<GameObject> disabledWeapons;

    public void AdditionalWeapon()
    {      
        startEnabledTarget++;
        disabledWeapons = weaponPlaces.FindAll(x => x.activeInHierarchy == false);
        byte randomNumber = (byte)Random.Range(0, disabledWeapons.Count);

        int place = weaponPlaces.IndexOf(disabledWeapons[randomNumber]);

        GameObject weapon = weaponPlaces[place];
        float angle = CalculatedRotation(weapon);

        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        weapon.GetComponent<WeaponHolder>().AssignThings(multiplier, angle);
        weapon.SetActive(true);
    }
    #endregion

    #region Player's second miss
    public void DoubleAmmos() => multiplier++;
    #endregion
}
