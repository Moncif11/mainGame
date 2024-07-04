using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class CapePlayerController : NetworkBehaviour
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
    public bool dashReady;
    public bool multiplayer = false;
    private GameObject CapeIdle;
    private GameObject CapeRun;
    private bool watchOther = false;
    private GameObject otherPlayer;
    public int dropOffset;

    public LayerMask platform;
    public LayerMask enemy;
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
    [SerializeField] private float damage = 20;
    public GameObject bulletPrefab;


    public void Start() {
        isWallJumping = false;
        dash = false;
        dashReady = true;
        originJumpforce = jumpforce;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originGravity = rb.gravityScale;
        tr = GetComponent<TrailRenderer>();
        myCam = Camera.main.transform;
        CapeIdle =transform.GetChild(10).gameObject;
        CapeIdle.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color32(123, 0, 22, 255);
        CapeRun =transform.GetChild(9).gameObject;
        CapeRun.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color32(123, 0, 22, 255);
        CapeRun.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color32(123, 0, 22, 255);
        if(SceneManager.GetActiveScene().name == "Lobby"  || SceneManager.GetActiveScene().name == "MultiLevel1" || SceneManager.GetActiveScene().name == "MultiLevel2")
        {
            multiplayer = true;
        }
        if (!IsOwner && multiplayer) {
            GameObject Head = transform.GetChild(3).gameObject;
            Head.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);
            GameObject LegLeft = transform.GetChild(5).gameObject;
            LegLeft.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);
            GameObject LegRight = transform.GetChild(6).gameObject;
            LegRight.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);
            GameObject Arm = transform.GetChild(13).gameObject;
            Arm.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);
        }

        if (SceneManager.GetActiveScene().name != "Lobby" && SceneManager.GetActiveScene().name != "TestScene" && GetComponent<Health>().healthBar != null) {
            GetComponent<Health>().resetHealth();
        }
    }
    void FixedUpdate()
    {
        if (!IsOwner && multiplayer) {
            return;
        }
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
        try {
            float okeee = myCam.transform.position.x;
        }
        catch (Exception e) {
            print("no Cam!");
            Start();
        }
        if (Input.GetKeyDown(KeyCode.F) && multiplayer) {
            watchOther = true;
            GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
            for (int i = 0; i < players.Length; i++) {
                if (this.gameObject.GetInstanceID() != players[i].GetInstanceID()){
                    otherPlayer = players[i];
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.F) && multiplayer) {
            watchOther = false;
        }

        if (watchOther) {
            myCam.transform.position = new Vector3(otherPlayer.transform.position.x, otherPlayer.transform.position.y,
                myCam.transform.position.z);
        }
        else {
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
    }

    void Update()
    {
        if (!IsOwner && multiplayer) {
            return;
        }
        
        lastOnGroundTime += Time.deltaTime;
        jump = false;
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (touchPosition.x > myCam.transform.position.x)
            {
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
        if (!isGrounded) {
            isGrounded = Physics2D.OverlapCircle(feetPos.position, feetCheckRadius, enemy);
        }
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
            if (moveInput > 0 && direction != "right")
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                direction = "right";
            }
            else if (moveInput < 0 && direction != "left")
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                direction = "left";
            }   
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            if (direction == "right") {
                GetComponent<Health>().itemDirektDroppedFromPlayer(dropOffset);
            }
            else {
                GetComponent<Health>().itemDirektDroppedFromPlayer(-dropOffset);
            }
        }
        
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Z)) && dashReady)
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

        if (transform.position.y < deadEnd) {
            dying();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "crusher" && isGrounded)
        {
            jumpforce = jumpforce * 0.8f;
            dying();
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

    public void dying()
    {
        movementFreezed = true;
        Quaternion rotation = transform.rotation;
        rotation.z = 90;
        transform.rotation = rotation;
        rotation = transform.rotation;
        rotation.z = 0;
        transform.rotation = rotation;
        List<GameObject> checkpoints = GameObject.FindGameObjectsWithTag ("Checkpoint").ToList();
        checkpoints = checkpoints.ToList().OrderBy(x => x.transform.position.x).ToList();
        Debug.Log("checkpointslength: "+checkpoints.Count );
        GameObject lastCheckpoint = checkpoints[0];
        for (int i = 0; i < checkpoints.Count; i++) {
            if (transform.position.x >= checkpoints[i].transform.position.x) {
                lastCheckpoint = checkpoints[i];
            }
        }
        transform.position = lastCheckpoint.transform.position;
        movementFreezed = false;
        Start();
    }

    IEnumerator dashing()
    {   dash = true;
        dashReady = false;
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
        if(dash && other.gameObject.CompareTag("Enemy")) {
                if (multiplayer) {
                    float enemyID = other.gameObject.GetComponent<Health>().myid;
                    if (OwnerClientId == 0) {
                        dashDamagaeClientRpc(enemyID);
                    }
                    else {
                        dashDamagaeServerRpc(enemyID);
                        other.gameObject.GetComponent<Health>().takeDamage(damage);
                    }
                }
                else {
                    other.gameObject.GetComponent<Health>().takeDamage(damage);
                }
        }

        if (other.gameObject.CompareTag("DamageObstacle")) {
            GetComponent<Health>().takeDamage(damage);
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

    public void changeCapeColor(byte red, byte green, byte blue) {
        CapeIdle.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color32(red, green, blue, 255);
        CapeRun.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color32(red, green, blue, 255);
        CapeRun.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color32(red, green, blue, 255);
    }

    public void changeCapeToOriginalColor() {
        changeCapeColor(123,  0, 22);
    }
    
    [ServerRpc]
    private void dashDamagaeServerRpc(float enemyID) {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Health>().myid == enemyID) {
                enemy.GetComponent<Health>().takeDamage(damage);
            }
        }
    }
    [ClientRpc]
    private void dashDamagaeClientRpc(float enemyID) {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Health>().myid == enemyID) {
                enemy.GetComponent<Health>().takeDamage(damage);
            }
        }
    }
}