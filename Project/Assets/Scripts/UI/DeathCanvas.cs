using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DeathCanvas : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;

    private void OnEnable()
    {
        PlayerController.PlayerDiedEvent += Death;
    }
    private void OnDisable()
    {
        PlayerController.PlayerDiedEvent -= Death;
    }

    private async void Death()
    {
        await Task.Delay(1000);
        deathMenu.SetActive(true);
    }
}
