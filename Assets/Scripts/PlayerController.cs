using System;
using System.Collections;
using UnityEngine;

/*
The PlayerController is to control the Actual player with WASD controlling  
*/
public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]private float health = 100f; 
    
    // movementspeed from the character 
    [SerializeField]private float speed; 
    
     // movementspeed from the character 
    [SerializeField]private float JumpForce; 

    [SerializeField]private float damage= 2;
    // rigidbody 2d to move the Player
    private new Rigidbody2D rigidbody2D; 

    // boolean for the look direction 
    private bool facingRight = false;

    // boolean to check the Player are under a ground or not  
    bool grounded; 

    private float horizontal; 
    //Dash implemented variables 
    private bool canDash = true ;
    private bool isDashing ;

    [SerializeField] private float dashingTime = 0.2f;

    [SerializeField] private float dashingCooldown= 1f; 
    [SerializeField] private float  dashsingPower=24f; 

    [SerializeField] private TrailRenderer tr; 

    //gravtiy 

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float multGravity = 18f;

    public  float maxFallSpeed = 2; 

      //variables all for wall Jump 
    [Header("Ground Check")]
    public Transform groundCheckPos; 

    public Vector2 groundCheckSize = new Vector2(1.7f, 0.24f);
    public LayerMask groundCheckLayer;

    //variables all for wall Jump 
    [Header("Wall Check")]
    public Transform wallCheckPos; 

    public Vector2 wallCheckSize = new Vector2(0.49f, 0.03f);
    public LayerMask WallCheckLayer;

    [Header("Wall Movement")]
    public float WallSlideSpeed = 2f;
    bool isWallSliding; 
    
    bool isTouchingWall; 
    bool isWallJumping;
    public float WallJumpDirection; 
    float WallJumpTime;
    float WallJumpTimer; 
    public Vector2 WallJumpPower = new Vector2(5f,10f); 

    public float wallJumpDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {   
        isDashing = false; 
        isWallJumping = false;        
        rigidbody2D = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }
    void Update(){
            horizontal = Input.GetAxisRaw("Horizontal");
            rigidbody2D.velocity = new Vector2(horizontal*speed,rigidbody2D.velocity.y);
             grounded = Physics2D.OverlapBox(groundCheckPos.position,groundCheckSize,0 ,groundCheckLayer);
            isTouchingWall = Physics2D.OverlapBox(wallCheckPos.position,wallCheckSize , 0, WallCheckLayer);
            processGravity(); 
         if (isTouchingWall && !grounded && rigidbody2D.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -WallSlideSpeed);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isWallSliding)
            {
                WallJump();
            }
            else if (grounded)
            {
                Jump();
            }
        }

        if(facingRight==false && horizontal<0){
            Flip();
        }
        else if (facingRight==true && horizontal>0){
             Flip();
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    { 
        if(Input.GetKey(KeyCode.E) && canDash){
            StartCoroutine(Dash()); 
        }
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x*= -1; 
        transform.localScale = Scaler;
    }
    
    void Jump(){
       // if(Input.GetKeyDown(KeyCode.Space)){
            //grounded = Physics2D.OverlapBox(groundCheckPos.position,groundCheckSize,0,groundCheckLayer);
            if(grounded){
            grounded =false;
            Debug.Log("Jumped");
            rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x,JumpForce);
            Debug.Log("Jump Velocity: " + rigidbody2D.velocity);
            }
        //}
    }
    private IEnumerator Dash(){
        canDash = false; 
        isDashing = true;
        float originalGravity = rigidbody2D.gravityScale ;  
        rigidbody2D.gravityScale = 0f; 
        rigidbody2D.velocity = new Vector2(transform.localScale.x * dashsingPower,0f); 
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rigidbody2D.gravityScale = originalGravity;
        isDashing = false; 
        yield return new WaitForSeconds(dashingCooldown);
        canDash= true;
    }

    void processGravity(){
        if (rigidbody2D.velocity.y < 0){
            rigidbody2D.gravityScale = baseGravity*multGravity; 
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,Mathf.Max(rigidbody2D.velocity.y,-maxFallSpeed)); 
        }
        else{
            rigidbody2D.gravityScale= baseGravity;
        }
    }
    private bool WallCheck() {
        return Physics2D.OverlapBox(wallCheckPos.position,wallCheckSize,0,WallCheckLayer); 
    }


    //Gizmo color by selecting the separating the boxColliders 
    void OnDrawGizmoSelected(){
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.color = Color.blue; 
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
        }
    
    
    void OnCollisionEnter2D(Collision2D other){
        if(isDashing && other.gameObject.CompareTag("Enemy")){
                other.gameObject.GetComponent<Health>().takeDamage(damage);   
        }
    }
    
    public void takeDamage(float damage){
        health-=damage; 
    }

    void WallJump()
    {
        rigidbody2D.velocity = new Vector2(- rigidbody2D.velocity.x, JumpForce);
    }
}