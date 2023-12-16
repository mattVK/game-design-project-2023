using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private ColoredFlash ColoredFlash;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float invincibilityTime;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float knockbackStrength;
    private LayerMask originalLayers;




    void Start()
    {
        currentHealth = maxHealth;
        originalLayers = playerCollider.excludeLayers;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator IndicateInvincibility()
    {
        
        playerCollider.excludeLayers = enemyLayerMask;
        
        yield return new WaitForSeconds(invincibilityTime);

        playerCollider.excludeLayers = originalLayers;
    }

    public void OnEnemyHitKnockback(Transform enemyTransform)
    { 
        Vector2 knockbackDirection = enemyTransform.position - transform.position;
        rb.AddForce(-knockbackDirection.normalized * knockbackStrength, ForceMode2D.Impulse);
        
    }

    public void TakeDamage()
    {
        ColoredFlash.Flash(Color.red);


        StartCoroutine(IndicateInvincibility());
        currentHealth--;
    }
}
