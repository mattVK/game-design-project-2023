using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dongle : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    [SerializeField] private float heightOfDongle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame, designed to be above player at all times at constant y
    void Update()
    {
        transform.position = new Vector2(playerGO.transform.position.x, heightOfDongle);
    }
}
