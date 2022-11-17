using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Targets : MonoBehaviour
{
    public ushort ActiveTargets { get => (ushort)activatedPlaces.Count; }
    [SerializeField] private List<GameObject> targetPlaces;

    private List<GameObject> activatedPlaces = new List<GameObject>();

    private ushort startEnabledTarget = 2;

    private void OnEnable()
    {
        Timer.NewTargetWave += EnableRandomTarget;
    }
    private void OnDisable()
    {
        Timer.NewTargetWave -= EnableRandomTarget;
    }

    private void Start()
    {
        EnableRandomTarget(1);
    }
    byte randomNumber;

    private void EnableRandomTarget(int waveNumber)
    {
        if(startEnabledTarget < 15)
            startEnabledTarget = CalculateWave(waveNumber);

        for (ushort i = 0; i < startEnabledTarget; i++) 
        {
            randomNumber = (byte)Random.Range(0, targetPlaces.Count);
            targetPlaces[randomNumber].SetActive(true);

            activatedPlaces.Add(targetPlaces[randomNumber]);
            targetPlaces.Remove(targetPlaces[randomNumber]);
        }
    }

    private ushort CalculateWave(int waveNumber) => waveNumber % 2 == 0 ? startEnabledTarget : startEnabledTarget += 1;
    public void RemoveGameobjectFromList(GameObject gameObject) => activatedPlaces.Remove(gameObject);
    public void AddGameobjectToList(GameObject gameObject) => targetPlaces.Add(gameObject);
}