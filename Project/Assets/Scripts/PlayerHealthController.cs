using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public delegate void OnPlayerDeath();
    public static OnPlayerDeath PlayerDeathEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            PlayerDeathEvent();
    }
}
