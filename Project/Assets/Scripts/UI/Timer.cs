using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void OnNewWave(int wave);
    public static event OnNewWave NewWaveEvent;

    public delegate void OnDeath();
    public static event OnDeath DeathEvent;

    [SerializeField] private Targets targets;
    [SerializeField] private TextMeshProUGUI timerText;
    [Min(0)]
    [SerializeField] private float timerSeconds = 15;

    private float timeLeft;
    private ushort waveNumber = 1;

    private void Awake()
    {
        timeLeft = timerSeconds;
    }

    void Update()
    {
        CalculateTime();
    }

    private void CalculateTime()
    {
        if (timeLeft > 0 && targets.ActiveTargets > 0) {
            timeLeft -= Time.deltaTime;
            timerText.text = System.Math.Round(timeLeft, 0).ToString(); 
        }   
        else if (timeLeft <= 0 && targets.ActiveTargets > 0) {
            DeathEvent();
        }
        else {
            timeLeft = timerSeconds;
            waveNumber++;           
            NewWaveEvent(waveNumber);
        }      
    }
}
