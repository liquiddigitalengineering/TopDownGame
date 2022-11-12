using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponPlaces;
    [SerializeField] private GameObject middlePoint;
        
    private ushort startEnabledTarget = 1;

    private void OnEnable()
    {
        Timer.NewWeaponWaveEvent += EnableRandomWeapons;
    }
    private void OnDisable()
    {
        Timer.NewWeaponWaveEvent -= EnableRandomWeapons;
    }

    private void Start()
    {
        EnableRandomWeapons(startEnabledTarget);
    }

    private void EnableRandomWeapons(int waveNumber)
    {
        if(startEnabledTarget < 8)
            startEnabledTarget = CalculateWave(waveNumber);

        for (ushort i = 0; i < startEnabledTarget; i++) {
            byte randomNumber = (byte)Random.Range(0, weaponPlaces.Count);
            GameObject weapon = weaponPlaces[randomNumber];
        }
    }

    private ushort CalculateWave(int waveNumber) => waveNumber % 2 == 0 ? startEnabledTarget : startEnabledTarget += 1;
}
