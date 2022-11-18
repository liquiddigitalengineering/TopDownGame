using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public float Speed;
    public ushort MiniVersions;
    public Transform Target;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spread;

    private Vector2 direction;
    
    private void Start()
    {
        direction = (Target.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * Speed;

        Invoke("SpawnMoreBullets", 0.5f);
    }

    private void SpawnMoreBullets()
    {
        for (int i = 0; i < MiniVersions; i++) {
            GameObject bulletPrefab =  Instantiate(prefab, this.transform.position, transform.rotation);
            Rigidbody2D gameRB = bulletPrefab.GetComponent<Rigidbody2D>();
            Vector2 dir = (Target.position - transform.position);
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-spread, spread);
            gameRB.velocity = (dir + pdir).normalized * Speed;
        }

        Destroy(this.gameObject);
    }
}
