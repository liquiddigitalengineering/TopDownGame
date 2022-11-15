using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponPlaces;
    [SerializeField] private GameObject middlePoints;
    [SerializeField] private WeaponTypesListSO weaponList;

    private ushort startEnabledTarget = 1;

    private void OnEnable()
    {
        Timer.NewWeaponWaveEvent += EnableRandomWeapons;
    }
    private void OnDisable()
    {
        Timer.NewWeaponWaveEvent -= EnableRandomWeapons;
    }

    private  void Start()
    {
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

            #region Calculating rotation of the weapon
            Vector3 targ = weapon.transform.GetChild(0).transform.position;
            targ.z = 0f;

            Vector3 objectPos = middlePoints.transform.position;
            targ.x -= objectPos.x;
            targ.y -= objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            angle += Random.Range(-15f, 15f);
            weapon.GetComponent<WeaponHolder>().Angle = angle;
            weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            #endregion

            weapon.GetComponent<WeaponHolder>().AssignThings();
            weapon.SetActive(true);
        }
    }

    private ushort CalculateWave(int waveNumber) => waveNumber % 2 == 0 ? startEnabledTarget += 1 : startEnabledTarget ;

    private void DisableAllWeapons()
    {
        for (int i = 0; i < weaponPlaces.Count; i++)
            weaponPlaces[i].SetActive(false);
    }
}
