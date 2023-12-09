using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class Movement : MonoBehaviour
{

    //draw gizmos
    private void OnDrawGizmos()
    {
        if (isPlayerShooting())
        {
            Gizmos.DrawLine(playerRb.position, playerShotPosition());
        }
    }

    //basic 2D movement variables
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float maxSpeed = 75f;
 
    //knockback movement variables
    [SerializeField] private float knockbackSpeed;


    //shooting variables
    private Boolean isShoot = false;

    //check if player is shooting
    Boolean isPlayerShooting()
    {
        
        return Input.GetMouseButton(0);
    }


    //player's mouse position in screen space
    Vector2 playerShotPosition()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    //player knockback
    Vector2 knockbackAngle()
    {
        return -1 * playerShotPosition().normalized;
    }

    //is player moving
    Boolean inputMovingHorizontal()
    {
        return Input.GetAxis("Horizontal") != 0;
    }
    
    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        if (inputMovingHorizontal())
        {
            playerRb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0), ForceMode2D.Impulse);
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxSpeed);
        }
        
    }
}

