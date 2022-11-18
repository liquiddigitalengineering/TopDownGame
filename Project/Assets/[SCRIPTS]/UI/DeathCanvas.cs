using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCanvas : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;

    private bool canBeRestarted = false;
    private void OnEnable()
    {
        PlayerController.PlayerDiedEvent += Death;
    }
    private void OnDisable()
    {
        PlayerController.PlayerDiedEvent -= Death;
    }

    private void Update()
    {
        if (!canBeRestarted) return;
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private async void Death()
    {
        await Task.Delay(1000);
        deathMenu.SetActive(true);
        canBeRestarted = true;
    }
}
