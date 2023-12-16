using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestSimple : MonoBehaviour
{

    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] SpriteRenderer enemyGO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT");
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("HIT WITH BULLET");
           
        }
    }
    
        
}
