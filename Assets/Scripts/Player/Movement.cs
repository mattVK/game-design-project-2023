using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{

    //draw gizmos
    private void OnDrawGizmos()
    {
        
         Gizmos.color = Color.red;
         Gizmos.DrawLine(playerRb.position, transform.position + new Vector3(knockbackAngle().x, knockbackAngle().y, 0));
        

    }

    //basic 2D movement variables
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float maxSpeed = 75f;
    [SerializeField] private float flightGravity = 4f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float fallDrag = 20f;
    private bool knockbackJump = false;
    [SerializeField] private LayerMask groundLayerMask;
    



    //knockback movement variables
    [SerializeField] private float knockbackSpeed = 10f;

    //shooting variables
    [SerializeField] private int bulletCountMax = 6;
    [SerializeField] private int bulletCount = 6;
    [SerializeField] private float bulletBurstSpeed = 0.2f;
    [SerializeField] private float bulletSpread = 65f;


    [SerializeField] private float reloadTime = 0.5f;
    private Boolean isReloading = false;


    [SerializeField] private GameObject bulletPrefab;
    private GameObject bullet;
    private float bulletSpeed = 30f;
    
    
    
    

    //shooting functions

    //check if player is shooting
    Boolean isPlayerShootingLeftMB()
    {
        
        return Input.GetMouseButtonDown(0);
    }

    Boolean isPlayerShootingRightMB()
    {
        return Input.GetMouseButtonDown(1);
    }

    //player's mouse position in screen space
    Vector2 playerShotPosition()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    //bullet instantiation
    void shootBulletLeftClick()
    {
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 shootDirection = playerShotPosition() - playerRb.position;
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * bulletSpeed;
        
    }


    //bullet bursting when right click
    IEnumerator delayBetweenBulletBurst()
    {
        for (int i = bulletCount; i > 0; i--)
        {
            
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 shootDirection = playerShotPosition() - (Vector2)transform.position;

            Quaternion shootRotation = Quaternion.Euler(0,0, Mathf.Atan2(shootDirection.y, shootDirection.x));
            float randomAngle = UnityEngine.Random.Range(-bulletSpread, bulletSpread);
            shootRotation *= Quaternion.Euler(0, 0, randomAngle);

            shootDirection = shootRotation * shootDirection;



  
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * bulletSpeed;
            yield return new WaitForSeconds(bulletBurstSpeed);
        }
    }

    
    //right click inputter
    void shootBulletRightClick()
    {
        StartCoroutine(delayBetweenBulletBurst());
    }

    //if bullet is real
    Boolean isBullet()
    {
        return bulletPrefab != null;
    }

    
    

    //player knockback
    Vector2 knockbackAngle()
    {
        Vector2 shootDirection = playerShotPosition()-playerRb.position ;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x);
        Vector2 shootingDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

   
        return -shootingDirection;
        
    }

    //player reloaded
    Boolean reloadPressed()
    {
        return Input.GetKeyDown(KeyCode.R);
    }

    IEnumerator Reload()
    {
        isReloading = true;
       
        //Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        

        //Debug.Log("Reload complete!");

        bulletCount = bulletCountMax;

        isReloading = false;
    }

    //moving functions

    //is player moving
    Boolean inputMovingHorizontal()
    {
        return Input.GetAxis("Horizontal") != 0;
    }

    //player shot
    void playerKnockbacked()
    {
        playerRb.AddForce(knockbackAngle() * knockbackSpeed, ForceMode2D.Impulse);
    }

    

    //player is moving, handle movement and also limits
    void playerWASDMovement()
    {
        playerRb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0), ForceMode2D.Impulse);
        if (Math.Abs(playerRb.velocity.x) > maxSpeed && Input.GetAxisRaw("Horizontal") != 0)
        {
            playerRb.AddForce(new Vector2(-Input.GetAxisRaw("Horizontal") * speed, 0), ForceMode2D.Impulse);
        }
    }

    //player is in air
    Boolean isFalling()
    {
        return playerRb.velocity.y < 0;
    }
    
    //player is jump
    Boolean playerJump()
    {
        return Input.GetButtonDown("Jump");
    }

    //player jump behavior
    void playerJumpMovement()
    {
        playerRb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    Boolean isGrounded()
    {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = playerRb.position;
        hits = Physics2D.RaycastAll(positionToCheck, Vector2.down, 0.6f, groundLayerMask);
        Debug.DrawRay(positionToCheck, Vector2.down * 0.6f);
        //Debug.Log(hits.Length);


        //if a collider was hit, we are grounded
        bool grounded = hits.Length > 0;

        return grounded;

    }
    
    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        
        playerWASDMovement();

        if (isPlayerShootingLeftMB() && bulletCount > 0 && !isReloading)
        {
            bulletCount--;
            shootBulletLeftClick();
        }
            else if (isPlayerShootingRightMB() && bulletCount > 0 && !isReloading)
        {
            shootBulletRightClick();
            bulletCount = 0;
            knockbackJump = true;
            
            playerKnockbacked();
        }

        if (reloadPressed() && !isReloading && bulletCount != bulletCountMax)
        {
            StartCoroutine(Reload());
        }
        else if (!isReloading && bulletCount == 0)
        {
            StartCoroutine(Reload());
        }

       if(isGrounded() && playerJump())
       {
            playerJumpMovement();
            knockbackJump = false;
       }
       else if (knockbackJump && playerJump() && !isGrounded())
       {
            playerJumpMovement();
            knockbackJump = !knockbackJump;
       }

        
        
    }


    //helps with consistency
    private void FixedUpdate()
    {
        if (isFalling())
        {
            playerRb.gravityScale = flightGravity;
            playerRb.drag = fallDrag;
        }
        else
        {
            playerRb.gravityScale = 1;
            playerRb.drag = 5;
        }
    }
}

