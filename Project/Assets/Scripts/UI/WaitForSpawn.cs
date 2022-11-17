using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitForSpawn : MonoBehaviour
{
    public static bool IsSpawned;
    [SerializeField] private GameObject targetSpawnPoints, gunSpawnPoints, timerCanvas, counterObject, deathCanvas, healthBar;
    [SerializeField] private TextMeshProUGUI counterText;
    // Start is called before the first frame update
    void  Start()
    {
        StartCoroutine(Count());

    }
    private IEnumerator Count()
    {
        IsSpawned = false;
        deathCanvas.SetActive(false);
        yield return new WaitForSeconds(1);
        counterText.text = "2";
        yield return new WaitForSeconds(1);
        counterText.text = "1";
        yield return new WaitForSeconds(1);
        EnableThings();
        IsSpawned = true;
    }

    private void EnableThings()
    {
        counterObject.SetActive(false);
        targetSpawnPoints.SetActive(true);
        gunSpawnPoints.SetActive(true);
        timerCanvas.SetActive(true);
        healthBar.SetActive(true);
    }
}
