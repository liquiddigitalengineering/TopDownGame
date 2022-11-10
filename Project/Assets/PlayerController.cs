using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Rigidbody2D rb;
    private GameObject wheel;
    private Transform m_transform;

    public enum State {left, right}

    Vector2 movement;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        wheel = GameObject.Find("WeaponWheel");

        m_transform = wheel.transform;
    }

    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Vector2 mousePosition = Input.mousePosition;
        if(wheel.transform.localEulerAngles.z < 180){
            transform.localScale = new Vector2(-1, 1);}
        if(wheel.transform.localEulerAngles.z > 180){
            transform.localScale = new Vector2(1, 1);}
        LAMouse();
        wheel.transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    private void LAMouse(){
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)-m_transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        m_transform.rotation = rotation;
    }
}
