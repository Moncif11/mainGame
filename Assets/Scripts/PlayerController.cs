using System;
using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

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
    public float WallSlideSpeed = 2;
    bool isWallSliding; 
    
    bool isWallJumping;
    public float WallJumpDirection; 
    float WallJumpTime;
    float WallJumpTimer; 
    public Vector2 WallJumpPower = new Vector2(5f,10f); 

    public float wallJumpDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }
    void Update(){
        horizontal = Input.GetAxisRaw("Horizontal");
        Jump();
        processGravity();
        processWallSliding();
        ProcessWallJumping();  
        if(facingRight==false && horizontal<0){
            Flip();
        }
        else if (facingRight==true && horizontal>0){
             Flip();
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {   //grounded = Physics2D.OverlapCircle(groundCheck.position);
            rigidbody2D.velocity = new Vector2(horizontal*speed,rigidbody2D.velocity.y);

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
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Jumped");
            if(grounded){
            rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x,JumpForce);
            Debug.Log("Jump Velocity: " + rigidbody2D.velocity);
            }
        }
        grounded = Physics2D.OverlapBox(groundCheckPos.position,groundCheckSize,0,groundCheckLayer);
        Debug.Log("grounded : "+grounded);

        if(WallJumpTimer >0f && Input.GetKeyDown(KeyCode.Space)){
                isWallJumping = true; 
                rigidbody2D.velocity = new Vector2(WallJumpDirection*WallJumpPower.x,WallJumpPower.y); 
                WallJumpTimer = 0f; 
                if(transform.localScale.x != WallJumpDirection){
                    facingRight = !facingRight;
                    Vector3 Scaler = transform.localScale;
                    Scaler.x*= -1; 
                    transform.localScale = Scaler;
                }
            Invoke(nameof(CancelWallJump),wallJumpDuration + 0.1f);
        }

        
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

    private void processWallSliding(){
        if (grounded==false && WallCheck()){
            Debug.Log("WallSliding");
            isWallSliding = true;
            Debug.Log("Horizontal Input: "+horizontal); 
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,Mathf.Max(rigidbody2D.velocity.y,-WallSlideSpeed));
            Debug.Log("Gravity : " + rigidbody2D.gravityScale);
        }
        else{
            isWallSliding = false;
        }
        Debug.Log("isWallSliding: " + isWallSliding); 
    }

    //Gizmo color by selecting the separating the boxColliders 
    void OnDrawGizmoSelected(){
        Gizmos.color = Color.yellow; 
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
        }
    
    private bool WallCheck() {
        return Physics2D.OverlapBox(wallCheckPos.position,wallCheckSize,0,WallCheckLayer); 
    }

    private void WallJump( ){
        if(isWallSliding){
            isWallJumping = false; 
            WallJumpDirection = -transform.localScale.x;
            WallJumpTimer = WallJumpTime;
        }
        else{
            WallJumpTimer -=Time.deltaTime; 
        }
    }

    private void ProcessWallJumping(){
        if(isWallSliding){
            isWallJumping = false; 
            WallJumpDirection = -transform.localScale.x;
            WallJumpTimer = WallJumpTime;
            CancelInvoke(nameof(CancelWallJump));
        }
        else{
            WallJumpTimer -= Time.deltaTime; 

        }
    }
    private void CancelWallJump(){
        isWallJumping = false;
    }
    
    void OnCollisionEnter2D(Collision2D other){
        if(isDashing && other.gameObject.CompareTag("Enemy")){
                other.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);   
        }
    }

    public void takeDamage(float damage){
        health-=damage; 
    }
}
