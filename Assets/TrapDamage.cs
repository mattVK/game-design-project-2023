using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private Damage playerDamaged;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT");
            playerDamaged.OnEnemyHitKnockback(transform);
            playerDamaged.TakeDamage();
        }
    }
}
