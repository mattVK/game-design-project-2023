    using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    [SerializeField] private ColoredFlash ColoredFlash;
    [SerializeField] private Damage playerDamaged;
    [SerializeField] private float currHealth, maxHealth = 3f;
    [SerializeField] private float enemyBulletKnockbackForce;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private Rigidbody2D enemyRb;
    [SerializeField] private Transform targetPlayer;
    
    Vector2 moveDirection;
    
    

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;



    }

    // Update is called once per frame
    void Update()
    {
        enemyRb.velocity = new Vector2(moveSpeed, 0f);

        if(targetPlayer)
        {
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            enemyRb.rotation = angle;
            enemyRb.velocity = new Vector2(direction.x * moveSpeed, 0f);
        }
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

            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
            if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    

}    

