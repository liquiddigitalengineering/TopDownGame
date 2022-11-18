using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestTime : MonoBehaviour
{
    private float timeSurvived;
    private bool canCount = true;

    private void OnEnable()
    {
        PlayerController.PlayerDiedEvent += StopTimer;
    }

    private void OnDisable()
    {
        PlayerController.PlayerDiedEvent -= StopTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!WaitForSpawn.IsSpawned || !canCount) return;

        timeSurvived += Time.deltaTime;
    }

    private void StopTimer()
    {
        canCount = false;

        if (!PlayerPrefs.HasKey("BestTime"))
            PlayerPrefs.SetFloat("BestTime", timeSurvived);
        else {
            float previousBestTime = PlayerPrefs.GetFloat("BestTime");

            if(previousBestTime < timeSurvived)
                PlayerPrefs.SetFloat("BestTime", timeSurvived);
        }

        Debug.Log(timeSurvived);
    }
}
