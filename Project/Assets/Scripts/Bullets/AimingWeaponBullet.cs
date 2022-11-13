using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingWeaponBullet : MonoBehaviour
{
    public float Speed, Angle;
    public Transform targetTransform;

    [SerializeField] private Rigidbody2D rb;
    private Vector3 direction;


    private void Start()
    {
        direction = (targetTransform.position - transform.position).normalized;
        Debug.Log(direction);
        rb.velocity = new Vector2(direction.x, direction.y) * Speed;
    }
}
