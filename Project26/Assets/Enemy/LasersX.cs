using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasersX : MonoBehaviour
{

    public float checkDistance = 1.0f;
    public LayerMask wallLayer;
    public float flickerInterval = 2.0f; // Interval for flicker effect

    private Renderer objectRenderer;
    private Collider objectCollider;
    private bool isFlickering = false; // Keeps track of flicker effect

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();

        //if (objectRenderer == null || objectCollider == null)
        //{
        //    // Debugging purposes
        //    Debug.LogError("Renderer or Collider component is missing from the laser prefab", this);
        //}
    }

    private void Start()
    {
        // Checks for walls using raycast
        bool wallOnLeftSide = Physics.Raycast(transform.position, -transform.right, checkDistance, wallLayer);
        bool wallOnRightSide = Physics.Raycast(transform.position, transform.right, checkDistance, wallLayer);

        // Turns on or off lasers whether there are walls detected on both sides
        if (wallOnLeftSide && wallOnRightSide)
        {
            EnableLaser(true);
            StartFlickering();
        }
        else
        {
            EnableLaser(false);
        }
    }

    private void EnableLaser(bool state) // Turns the laser on or off visually
    {
        objectRenderer.enabled = state;
        objectCollider.enabled = state;
    }

    private void StartFlickering()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            StartCoroutine(FlickerRoutine());
        }
    }

    private IEnumerator FlickerRoutine()
    {
        // Changes the lasers visibility and collider at the set interval
        while (isFlickering)
        {
            yield return new WaitForSeconds(flickerInterval);
            bool newState = !objectRenderer.enabled;
            objectRenderer.enabled = newState;
            objectCollider.enabled = newState;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //// Checks if the laser collider is active and if the colliding object is the player
        //if (objectCollider.enabled && other.CompareTag("Player"))
        //{
        //    // Accesses the PlayerStats script
        //    PlayerStats playerStats = other.GetComponent<PlayerStats>();
        //    if (playerStats != null)
        //    {
        //        // Calls the TakeDamage method with a damage of -1 HP
        //        playerStats.TakeDamage(1);

        //        // Debugging purposes
        //        Debug.Log("Player has taken 2 damage from a laser. Current HP: " + playerStats.CurrentHP);
        //    }
        //}

        if (other.CompareTag("Player"))
        {
            // Directly access PlayerStats Instance to call methods
            PlayerStats.Instance.TakeDamage(2);

            // Debugging purposes
            Debug.Log("Player has taken 2 damage from Laser. Current HP: " + PlayerStats.Instance.CurrentHP);
        }
    }

    private void OnDestroy()
    {
        isFlickering = false;
    }
}
