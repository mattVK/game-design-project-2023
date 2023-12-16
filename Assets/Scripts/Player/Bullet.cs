using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifespan = 1f;
    [SerializeField] private TrailRenderer trailRenderer;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        
        if (lifespan <= 0f)
        {
            Destroy(gameObject);
        }
    }

   

private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Level"))
        {
            trailRenderer.emitting = false;
            Destroy(gameObject);
            
            
        }
    }
    

}
