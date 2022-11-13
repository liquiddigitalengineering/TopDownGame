using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingWeaponBullet : MonoBehaviour
{
    public float Speed, Angle;
    public Transform playerTransform;

    [SerializeField] private Rigidbody2D rb;
    private Vector2 direction;

    private void Start()
    {
        direction = (transform.forward - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * Speed;
    }
}
