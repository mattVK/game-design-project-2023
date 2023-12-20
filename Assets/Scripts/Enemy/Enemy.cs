    using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    [SerializeField] private ColoredFlash ColoredFlash;
    [SerializeField] private Damage playerDamaged;
    [SerializeField] private Rigidbody2D enemyRb;
    [SerializeField] private float enemyBulletKnockbackForce;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
        
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT");
            playerDamaged.OnEnemyHitKnockback(transform);
            playerDamaged.TakeDamage();
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("HIT WITH BULLET");
            ColoredFlash.Flash(Color.red);
            Vector2 shootKnockback = (transform.position - collision.gameObject.transform.position).normalized;
            Debug.DrawLine(enemyRb.position, shootKnockback, Color.green);
            enemyRb.AddForce(shootKnockback * enemyBulletKnockbackForce, ForceMode2D.Impulse);
        }
    }

    

}    

