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
    [SerializeField] private float speed = 50;

    //knockback movement variables
    [SerializeField] private float knockbackSpeed;


    //shooting variables
    private Boolean isShoot = false;

    //check if player is shooting
    Boolean isPlayerShooting()
    {
        
        return Input.GetMouseButtonDown(0);
    }


    //player's mouse position in screen space
    Vector2 playerShotPosition()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }
    
    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        playerRb.velocity = new Vector2(Input.GetAxis("Horizontal"), 0) * speed ;   
        
    }
}
