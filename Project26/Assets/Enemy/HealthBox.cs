using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    // Accesses the PlayerStats script
        //    PlayerStats playerStats = other.GetComponent<PlayerStats>();
        //    if (playerStats != null)
        //    {
        //        // Calls the GainHealth method with an increase of +5 HP
        //        playerStats.GainHealth(5);

        //        // Debugging purposes
        //        Debug.Log("Player has gained 10 health from Health Box. Current HP: " + playerStats.CurrentHP);

        //        Destroy(gameObject);
        //    }
        //}

        if (other.CompareTag("Player"))
        {
            // Directly access PlayerStats Instance to call methods
            PlayerStats.Instance.GainHealth(5);

            // Debugging purposes
            Debug.Log("Player has gained 5 health from Health Box. Current HP: " + PlayerStats.Instance.CurrentHP);

            // Destroy the Health Box GameObject after increasing health
            Destroy(gameObject);
        }
    }
}
