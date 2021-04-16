using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderPowerupPickup : MonoBehaviour
{
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
        Debug.Log("Test");
        GameManager.Instance.obtainedPowerups.Add(Powerup.Glider);
        GameManager.Instance.powerupStatus.Add(false);
        Destroy(gameObject);
    }
}