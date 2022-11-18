using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    [HideInInspector]
    public Transform Direction;

    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private CircleCollider2D col;

    private void Start()
    {
        col.enabled = false;
    }

    private void Update()
    {
        if (speed > 0) {
            speed -= Random.Range(0.1f, 0.3f);
            transform.position = Vector2.MoveTowards(transform.position, Direction.position, speed * Time.deltaTime);
        }
        else {
            Booom();
        }
    }

    private void Booom()
    {
        StartCoroutine(ExplodeCoroutine());
    }

    private IEnumerator ExplodeCoroutine()
    {
        Particles();
        col.enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private void Particles()
    {
        particles.Play();
        ParticleSystem.EmissionModule em = particles.emission;
        em.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerController>().DeathEvent();
    }
}
