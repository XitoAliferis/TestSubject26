using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testtube : MonoBehaviour
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
        //        // Calls the GainSP method with an increase of +5 SP
        //        playerStats.GainSP(5);

        //        // Debugging purposes
        //        Debug.Log("Player has gained 10 SP from Test tube. Current SP: " + playerStats.CurrentHP);

        //        Destroy(gameObject);
        //    }
        //}

        if (other.CompareTag("Player"))
        {
            // Directly access PlayerStats Instance to call methods
            PlayerStats.Instance.GainSP(5);

            // Debugging purposes
            Debug.Log("Player has gained 5 SP from Test Tube. Current SP: " + PlayerStats.Instance.CurrentSP);

            // Destroy the Health Box GameObject after increasing health
            Destroy(gameObject);
        }
    }

}
