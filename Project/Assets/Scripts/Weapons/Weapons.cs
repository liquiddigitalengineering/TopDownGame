using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private List<WeaponHolderInfo> weaponPlaces;
    [SerializeField] private GameObject middlePoints;
    [SerializeField] private WeaponTypesListSO weaponList;

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

    private void Start()
    {
        EnableRandomWeapons(startEnabledTarget);
    }
  
    private void EnableRandomWeapons(int waveNumber)
    {
        DisableAllWeapons();
       
        if(startEnabledTarget < 8) startEnabledTarget = CalculateWave(waveNumber);

        for (ushort i = 0; i < startEnabledTarget; i++) {
            byte randomNumber = (byte)Random.Range(0, weaponPlaces.Count);
            GameObject weapon = weaponPlaces[randomNumber].Weapon;

            if (weaponPlaces[randomNumber].Weapon.activeInHierarchy) {
                i--;
                continue;
            }

            float angle = CalculatedRotation(weapon);

            weapon.GetComponent<WeaponHolder>().Angle = angle;
            weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            weapon.GetComponent<WeaponHolder>().AssignThings(multiplier);
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
    private void DisableAllWeapons()
    {
        for (int i = 0; i < weaponPlaces.Count; i++)
            weaponPlaces[i].Weapon.SetActive(false);
    }

    private void PlayerDied()
    {
        DisableAllWeapons();
        this.gameObject.SetActive(false);
    }
    #endregion

    #region Player's first miss
    private List<WeaponHolderInfo> disabledWeapons;

    public void AdditionalWeapon()
    {      
        startEnabledTarget++;
        disabledWeapons = weaponPlaces.FindAll(x => x.Weapon.activeInHierarchy == false);
        byte randomNumber = (byte)Random.Range(0, disabledWeapons.Count);

        int place = weaponPlaces.IndexOf(disabledWeapons[randomNumber]);

        GameObject weapon = weaponPlaces[place].Weapon;

        float angle = CalculatedRotation(weapon);

        weapon.GetComponent<WeaponHolder>().Angle = angle;
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        weapon.GetComponent<WeaponHolder>().AssignThings(multiplier);
        weapon.SetActive(true);
    }
    #endregion

    #region Player's second miss
    public void DoubleAmmos() => multiplier++;
    #endregion
}

[System.Serializable]
public struct WeaponHolderInfo
{
    public GameObject Weapon;
    public byte Id;
}
