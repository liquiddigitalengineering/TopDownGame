using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public delegate void OnNewTargetWave(int wave);
    public static event OnNewTargetWave NewTargetWave ;

    public delegate void OnNewWeaponWave(int wave);
    public static event OnNewWeaponWave NewWeaponWaveEvent;

    [SerializeField] private Targets targets;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Weapons weapon;
    [SerializeField] private GameObject deathCanvas;
    [Min(0)]
    [SerializeField] private float timerSecondsTargets = 15;
    [SerializeField] private float timerSecondsWeapons = 20;
    [Header("Healtbar")]
    [SerializeField] private Sprite[] healthBarSprites;
    [SerializeField] private Image healthBarImage;

    private float timeLeftTargets, timeLeftWeapons;
    private ushort targetWaveNumber, weaponWaveNumber = 1;

    private void OnEnable()
    {
        PlayerController.PlayerDiedEvent += DisableScript;
        Bullet.BulletMissed += UpdateHealthBar;
    }
    private void OnDisable()
    {
        PlayerController.PlayerDiedEvent -= DisableScript;
        Bullet.BulletMissed += UpdateHealthBar;
    }

    private void Awake()
    {
        deathCanvas.SetActive(false);
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
            playerController.DeathEvent();
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

    int currentSprite = -1;
    private void UpdateHealthBar()
    {
        currentSprite++;
        healthBarImage.sprite = healthBarSprites[currentSprite];
        if (currentSprite == 0)
            weapon.AdditionalWeapon();
        else if (currentSprite == 1)
            weapon.DoubleAmmos();
        else if (currentSprite == 2)
            playerController.DeathEvent();
    }
}
