using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Grenade : MonoBehaviour
{
    [HideInInspector]
    public Transform Direction;

    [SerializeField] private float speed;
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private CircleCollider2D col;

    private bool isExploding = false;

    private void Start()
    {
        col.enabled = false;
        speed = Random.Range(15, 17);
    }

    private void Update()
    {
        if (isExploding) return;

        if(speed > 0) {
            speed -= Random.Range(0.1f, 0.3f);
            transform.position = Vector2.MoveTowards(transform.position, Direction.position, speed * Time.deltaTime);
        }
        else {
            anim.enabled = false;
            isExploding = true;
            Invoke("Booom", 3);
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
