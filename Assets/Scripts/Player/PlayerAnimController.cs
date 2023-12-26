using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D playerRb;
    private SpriteRenderer[] playerSprite;
    private SpriteRenderer bodySprite;
    private Transform bodyTransform;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentsInChildren<SpriteRenderer>();
        bodyTransform = transform.Find("bodi");
        bodySprite = bodyTransform.GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            anim.SetBool("IsMoving", true);
            
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            for (int i = 0; i < playerSprite.Length; i++)
            {
                if (playerSprite[i].transform == bodyTransform)
                    continue;
                playerSprite[i].flipX = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            for (int i = 0; i < playerSprite.Length; i++)
            {
                if (playerSprite[i].transform == bodyTransform)
                    continue;
                playerSprite[i].flipX = false;
            }
        }

        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - bodyTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bodyTransform.rotation = Quaternion.Euler(0, 0, angle);

        if (bodyTransform.rotation.eulerAngles.z > 90f && bodyTransform.rotation.eulerAngles.z < 270f)
        {
            bodySprite.flipY = true;
            
        }
        else
        {
            bodySprite.flipY = false;
        }
    }
}
