using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public delegate void OnBulletMissed();
    public static event OnBulletMissed BulletMissed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 direction;
    private Vector3 mousePos;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //direction = (mousePos - transform.position);
        //rb.velocity = new Vector2(direction.x, direction.y).normalized * Speed;
    }

    private void Start()
    {
        direction = (mousePos - transform.position);
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target")) {
            collision.GetComponent<Target>().DisableTarget();
            Destroy(this.gameObject);
        }
        else if(collision.CompareTag("Border")) {
            BulletMissed();
        }

    }
}
