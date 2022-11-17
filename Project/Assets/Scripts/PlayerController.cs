using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnPlayerDied();
    public static event OnPlayerDied PlayerDiedEvent;

    public static bool IsMoving { get; private set; }
    [SerializeField] [Min(0)] private float movementSpeed = 5f;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private BoxCollider2D boxCollider2;

    private Rigidbody2D rb;
    private GameObject wheel;
    private Transform mouseTransform;
    public Animator animator;

    private GameObject topRightLimitObject, bottomLeftLimitObject;

    private Vector3 topRightLimit, bottomLeftLimit;
    private bool canMove = true;
    Vector2 movement;

    #region Take care of events :) (it's not greek village, really xd)
    private void OnEnable()
    {
        PlayerHealthController.PlayerDeathEvent += DeathEvent;
    }
    private void OnDisable()
    {
        PlayerHealthController.PlayerDeathEvent -= DeathEvent;
    }
    #endregion

    void Start(){
        IsMoving = false;
        rb = GetComponent<Rigidbody2D>();
        wheel = GameObject.Find("WeaponWheel");

        mouseTransform = wheel.transform;

        topRightLimitObject = GameObject.Find("TopRightLimit");
        bottomLeftLimitObject = GameObject.Find("BottomLeftLimit");
        //topRightLimit = topRightLimitObject.transform.position;
        //bottomLeftLimit = bottomLeftLimitObject.transform.position;
    }

    void Update(){
        if (!canMove) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Vector2 mousePosition = Input.mousePosition;
        if(wheel.transform.localEulerAngles.z > 180){
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x)*-1, transform.localScale.y);} //When the pointer is left of the player, flip the player to face left
        if(wheel.transform.localEulerAngles.z < 180){
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);} //When the pointer is right of the player, flip the player to face right
        MouseDirection();
        wheel.transform.position = new Vector2(transform.position.x, transform.position.y);

        if (movement == Vector2.zero) IsMoving = false;
        else IsMoving = true;
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
        // if(rb.position.x <= bottomLeftLimit.x || rb.position.y <= bottomLeftLimit.y){
        //     rb.position = new Vector2(transform.position.x+0.015f, transform.position.y+0.015f);
        // }
        // if(rb.position.x >= topRightLimit.x || rb.position.y >= topRightLimit.y){
        //     rb.position = new Vector2(transform.position.x-0.015f, transform.position.y-0.015f);
        // }
    }

    private void MouseDirection(){ //Function calculates angle of mouse to player, sets aim cursor to value. 
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)-mouseTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        mouseTransform.rotation = rotation;
    }

    public async void DeathEvent(){
        canMove = false;
        movement = Vector2.zero;
        animator.SetBool("EndGame", true);
        capsuleCollider.enabled = false;
        boxCollider2.enabled = false;
        await Task.Delay(1000);
        PlayerDiedEvent();     
    }
}
