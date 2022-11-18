using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Healtbar")]
    [SerializeField] private Sprite[] healthBarSprites;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Weapons weapon;
    [SerializeField] private PlayerController playerController;

    private void OnEnable()
    {
        Bullet.BulletMissed += UpdateHealthBar;
    }
    private void OnDisable()
    {
        Bullet.BulletMissed -= UpdateHealthBar;
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
