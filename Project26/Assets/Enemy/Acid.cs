using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
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
        //// Check if the colliding object is tagged as "Player"
        //if (other.CompareTag("Player"))
        //{
        //    // Access the PlayerStats script attached to the player object
        //    PlayerStats playerStats = other.GetComponent<PlayerStats>();
        //    if (playerStats != null)
        //    {
        //        // Call the TakeDamage method with a damage value of 2
        //        playerStats.TakeDamage(2);

        //        // Optional: Log the damage taken for debugging
        //        Debug.Log("Player has taken 2 damage from acid. Current HP: " + playerStats.CurrentHP);
        //    }
        //}

        if (other.CompareTag("Player"))
        {
            // Directly access PlayerStats Instance to call methods
            PlayerStats.Instance.TakeDamage(2);

            // Debugging purposes
            Debug.Log("Player has taken 2 damage from Acid. Current HP: " + PlayerStats.Instance.CurrentHP);
        }
    }
}
