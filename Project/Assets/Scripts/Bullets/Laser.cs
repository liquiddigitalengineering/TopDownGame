using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");

        if (collision.CompareTag("Player"))
            playerController.DeathEvent();
    }
}
