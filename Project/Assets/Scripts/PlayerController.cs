using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Rigidbody2D rb;
    private GameObject wheel;
    private Transform MouseTransform;
    public Animator animator;

    private GameObject topRightLimitObject;
    private GameObject bottomLeftLimitObject;

    private Vector3 topRightLimit;
    private Vector3 bottomLeftLimit;

    Vector2 movement;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        wheel = GameObject.Find("WeaponWheel");

        MouseTransform = wheel.transform;

        topRightLimitObject = GameObject.Find("TopRightLimit");
        bottomLeftLimitObject = GameObject.Find("BottomLeftLimit");
        topRightLimit = topRightLimitObject.transform.position;
        bottomLeftLimit = bottomLeftLimitObject.transform.position;
    }

    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Vector2 mousePosition = Input.mousePosition;
        if(wheel.transform.localEulerAngles.z < 180){
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x)*-1, transform.localScale.y);} //When the pointer is left of the player, flip the player to face left
        if(wheel.transform.localEulerAngles.z > 180){
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);} //When the pointer is right of the player, flip the player to face right
        MouseDirection();
        wheel.transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        if(rb.position.x <= bottomLeftLimit.x || rb.position.y <= bottomLeftLimit.y){
            rb.position = new Vector2(transform.position.x+0.015f, transform.position.y+0.015f);
        }
        if(rb.position.x >= topRightLimit.x || rb.position.y >= topRightLimit.y){
            rb.position = new Vector2(transform.position.x-0.015f, transform.position.y-0.015f);
        }
    }

    private void MouseDirection(){ //Function calculates angle of mouse to player, sets aim cursor to value. 
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)-MouseTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        MouseTransform.rotation = rotation;
    }
    public void DeathEvent(){
        animator.SetBool("EndGame", true);
    }
}
