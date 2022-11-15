using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingWeaponBullet : MonoBehaviour
{
    public float Speed;
    public Transform TargetTransform;

    [SerializeField] private Rigidbody2D rb;
    private Vector3 direction;


    private void Start()
    {
        direction = (TargetTransform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * Speed;
    }
}
