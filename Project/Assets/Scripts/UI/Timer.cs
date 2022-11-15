using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void OnNewTargetWave(int wave);
    public static event OnNewTargetWave NewTargetWave ;

    public delegate void OnNewWeaponWave(int wave);
    public static event OnNewWeaponWave NewWeaponWaveEvent;

    public delegate void OnDeath();
    public static event OnDeath DeathEvent;

    [SerializeField] private Targets targets;
    [SerializeField] private TextMeshProUGUI timerText;
    [Min(0)]
    [SerializeField] private float timerSecondsTargets = 15;
    [SerializeField] private float timerSecondsWeapons = 20;
   
    private float timeLeftTargets, timeLeftWeapons;
    private ushort targetWaveNumber, weaponWaveNumber = 1;

    private void OnEnable()
    {
        PlayerController.PlayerDiedEvent += DisableScript;
    }
    private void OnDisable()
    {
        PlayerController.PlayerDiedEvent -= DisableScript;
    }

    private void Awake()
    {
        timeLeftTargets = timerSecondsTargets;
    }

    void Update()
    {
        CalculateTargetTime();
        CalculateWeaponTime();
    }

    private void CalculateTargetTime()
    {
        if (timeLeftTargets > 0 && targets.ActiveTargets > 0) {
            timeLeftTargets -= Time.deltaTime;
            timerText.text = System.Math.Round(timeLeftTargets, 0).ToString(); 
        }   
        else if (timeLeftTargets <= 0 && targets.ActiveTargets > 0) {
            DeathEvent();
            //GetComponent<PlayerController>().Death();
        }
        else {
            timeLeftTargets = timerSecondsTargets;
            targetWaveNumber++;           
            NewTargetWave(targetWaveNumber);
        }      
    }

    private void CalculateWeaponTime()
    {
        if (timeLeftWeapons > 0)
            timeLeftWeapons -= Time.deltaTime;
        else {
            timeLeftWeapons = timerSecondsWeapons;
            weaponWaveNumber++;
            NewWeaponWaveEvent(weaponWaveNumber);
        }
    }

    private void DisableScript()
    {
        this.gameObject.SetActive(false);
    }
}
