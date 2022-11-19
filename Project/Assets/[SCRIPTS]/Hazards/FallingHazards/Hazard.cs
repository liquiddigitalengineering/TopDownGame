using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public delegate void OnFell();
    public static event OnFell FellEvent;

    [HideInInspector]
    public float Speed;
    [HideInInspector]
    public Vector2 Direction;


    [SerializeField] private Animator anim;
    [Header("Camera shaking")]
    [SerializeField] private float shakeDuration = 2f;
    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private float appropriateDistance;

    private float shakeTimer;
    private bool isFalling = true;
    private Vector3 orignalCameraPos;
    private bool canShake = false;
    private Transform cameraTransform;


    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        orignalCameraPos = cameraTransform.localPosition;
    }

    private void Update()
    {
        if (canShake) StartCameraShakeEffect();
        if (!isFalling) return;

        Vector2 difference = Direction - (Vector2)transform.position;
        transform.position = Vector2.MoveTowards(transform.position, Direction, Speed * Time.deltaTime);
        
        if (difference.sqrMagnitude < appropriateDistance) {
            isFalling = false;
            canShake = true;
            shakeTimer = shakeDuration;
            FellEvent();
            isFalling = false;
            anim.SetTrigger("Slam");
        }      
    }

    public void DisableObject()
    {
        this.gameObject.SetActive(false);
        cameraTransform.position = orignalCameraPos;
        canShake = false;
        isFalling = true;
    }

    private void StartCameraShakeEffect()
    {
        if (shakeTimer > 0) {
            cameraTransform.localPosition = orignalCameraPos + Random.insideUnitSphere * shakeAmount;
            shakeTimer -= Time.deltaTime;
        }
        else {
            shakeTimer = 0f;
            cameraTransform.position = orignalCameraPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerController>().DeathEvent();
    }
}
