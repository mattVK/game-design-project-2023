using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    
    [SerializeField] private Sprite[] healthBarStates;
    private SpriteRenderer healthBarSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        healthBarSpriteRenderer=GetComponent<SpriteRenderer>(); 
    }

    public void UpdateHealthBar(int currHealth)
    {
        healthBarSpriteRenderer.sprite = healthBarStates[6  - currHealth];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
