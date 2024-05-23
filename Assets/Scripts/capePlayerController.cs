using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class CapePlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    string lastDir;
   public string direction;
    string currentState;
    string currentCapeState;
    float moveInput;
    float originJumpforce;
    float originGravity;
    float jumptimecounter;
    float lastOnGroundTime;
    bool isGrounded;
    private bool touchesWall;
    bool isJumping;
    private bool isWallJumping;
    bool movementFreezed;
    bool jump;
    public bool dash;
    bool dashReady;

    public LayerMask platform;
    //public Joystick joystick;
    public Transform feetPos;
    public Transform handPos;
    public Transform myCam;
    public float jumpforce;
    public float speed;
    [Header("Dash")]

    [SerializeField] private TrailRenderer tr; 
     [SerializeField] private float  dashingPower=24f; 
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    public float wallJumpCooldown;

    [Header("Enviroment Settings")]
    public float feetCheckRadius;
    public float handCheckRadius;
    public float jumptime;
    public float deadEnd;
    public float movBorderX;
    public float movBorderY;

    [Header("Camera Settings")]
    public float cameraOffsetY;
    public float worldEndMinX;
    public float worldEndMaxX;
    public float worldEndMinY;
    public float coyoteTime;
    public float slideSpeed;

    [Header("Stats")]
    [SerializeField] private float damage = 2;
    public GameObject bulletPrefab; 
    void Start()
    {
        isWallJumping = false;
        dash = false;
        dashReady = true;
        originJumpforce = jumpforce;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originGravity = rb.gravityScale;
        tr = GetComponent<TrailRenderer>();
    }

    void FixedUpdate()
    {
        //moveInput = joystick.Horizontal;
        
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetKey(KeyCode.A))
            {
                if (lastDir == "right")
                {
                    moveInput = -1;
                }
                else
                {
                    moveInput = 1;
                }
            }
            else
            {
                moveInput = Input.GetAxisRaw("Horizontal");
            }

            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (moveInput < 0)
                {
                    lastDir = "left";
                }
                else if (moveInput > 0)
                {
                    lastDir = "right";
                }
            }
        

        if (movementFreezed)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            if (dash)
            {
                if (direction == "left")
                {
                    rb.velocity = new Vector2(-1 * dashSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                }
            }
            else
            {
                if (isWallJumping)
                {
                    if (direction == "left")
                    {
                        rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                    }
                }
                else
                {
                    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
                }
            }
        }

        if (transform.position.x - myCam.transform.position.x > movBorderX)
        {
            myCam.transform.position = new Vector3(transform.position.x - movBorderX, myCam.transform.position.y,
                myCam.transform.position.z);
        }
        else if (transform.position.x - myCam.transform.position.x < -movBorderX)
        {
            myCam.transform.position = new Vector3(transform.position.x + movBorderX, myCam.transform.position.y,
                myCam.transform.position.z);
        }

        if (transform.position.y - myCam.transform.position.y > movBorderY)
        {
            myCam.transform.position = new Vector3(myCam.transform.position.x, transform.position.y - movBorderY,
                myCam.transform.position.z);
        }
        else if (transform.position.y - myCam.transform.position.y < -movBorderY)
        {
            myCam.transform.position = new Vector3(myCam.transform.position.x, transform.position.y + movBorderY,
                myCam.transform.position.z);
        }

        myCam.transform.position = Vector3.Lerp(myCam.transform.position,
            new Vector3(transform.position.x, transform.position.y + cameraOffsetY, myCam.transform.position.z), 0.01f);
        if (myCam.transform.position.y < worldEndMinY)
        {
            myCam.transform.position =
                new Vector3(myCam.transform.position.x, worldEndMinY, myCam.transform.position.z);
        }

        if (myCam.transform.position.x < worldEndMinX)
        {
            myCam.transform.position =
                new Vector3(worldEndMinX, myCam.transform.position.y, myCam.transform.position.z);
        }

        if (myCam.transform.position.x > worldEndMaxX)
        {
            myCam.transform.position =
                new Vector3(worldEndMaxX, myCam.transform.position.y, myCam.transform.position.z);
        }
    }

    void Update()
    {
        lastOnGroundTime += Time.deltaTime;
        jump = false;
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Debug.Log(touchPosition);
            if (touchPosition.x > myCam.transform.position.x)
            {
                Debug.Log("jump");
                jump = true;
            }
        }

        if (Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(1);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (touchPosition.x > myCam.transform.position.x)
            {
                jump = true;
            }
        }
        touchesWall = Physics2D.OverlapCircle(handPos.position, handCheckRadius, platform);
        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetCheckRadius, platform);
        if (dash)
        {
            animator.speed = 2;
        }
        else
        {
            animator.speed = 1;
        }

        if (moveInput != 0 && !touchesWall)
        {
            changeCapeAnimationState("cape-run");
        }
        else
        {
            changeCapeAnimationState("cape-idle");
        }
        if (touchesWall&&!isGrounded&&moveInput!=0&&!isJumping)
        {
            changeAnimationState("wall-slide");
            rb.gravityScale = 0f;
            rb.velocity = Vector2.down * slideSpeed;
        }
        else
        {
            rb.gravityScale = originGravity;
            if (!isGrounded)
            {
                changeAnimationState("jump");
            }
            else if (moveInput == 0)
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    changeAnimationState("duck");
                }
                else
                {
                    changeAnimationState("idle");
                }
            }
            else
            {
                changeAnimationState("run");
            }
        }

        if (!isWallJumping)
        {
            if (moveInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                direction = "right";
            }
            else if (moveInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                direction = "left";
            }   
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashReady)
        {
            StartCoroutine(dashing());
        }

        if ((isGrounded && jump) || (lastOnGroundTime < coyoteTime) && Input.GetKeyDown(KeyCode.Space) &&
            !isGrounded && !isJumping || animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "wall-slide" && Input.GetKeyDown(KeyCode.Space) )
        {
            if (touchesWall && Input.GetKey(KeyCode.Space) && !isGrounded)
            {
                StartCoroutine(wallJumping());
            }
            rb.velocity = Vector2.up * jumpforce;
            jumptimecounter = jumptime;
            isJumping = true;
        }

        if (isGrounded)
        {
            lastOnGroundTime = 0f;
            isWallJumping = false;
        }

        if (jump && isJumping)
        {
            if (jumptimecounter > 0)
            {
                rb.velocity = Vector2.up * jumpforce;
                jumptimecounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (jump == false)
        {
            isJumping = false;
        }

        if (transform.position.y < deadEnd)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        /*if(Input.GetKeyDown(KeyCode.E)){
            GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position + transform.right, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(transform.right*1000);
        }
        */
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "crusher" && isGrounded)
        {
            jumpforce = jumpforce * 0.8f;
            StartCoroutine(dying());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "heart" && animator.GetBool("isSquishingToDeath"))
        {
            animator.SetBool("isSquishingToDeath", false);
            Destroy(other.gameObject);
            jumpforce = originJumpforce;
        }
    }

    IEnumerator dying()
    {
        movementFreezed = true;
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator dashing()
    {   dash = true;
        float originalGravity = rb.gravityScale ;  
        rb.gravityScale = 0f; 
        rb.velocity = new Vector2(transform.localScale.x * dashingPower,0f); 
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        dash = false; 
        yield return new WaitForSeconds(dashCooldown);
        dashReady =  true;
    }

    IEnumerator wallJumping()
    {
        isWallJumping = true;
        if (direction == "left")
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            direction = "right";
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            direction = "left";
        }
        yield return new WaitForSecondsRealtime(wallJumpCooldown);
        isWallJumping = false;
    }
    
    void OnCollisionEnter2D(Collision2D other){
        if(dash && other.gameObject.CompareTag("Enemy")){
                other.gameObject.GetComponent<Health>().takeDamage(damage);   
        }
    }
    void changeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    void changeCapeAnimationState(string newState)
    {
        if (currentCapeState == newState) return;
        animator.Play(newState);
        currentCapeState = newState;
    }
}