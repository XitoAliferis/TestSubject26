using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    public Transform player; // Player's Transform to follow

    public Vector3 offset = new Vector3(0, 2, -5); // Offset from the Player's position

    public float smoothSpeed = 0.125f; // How smoothly the camera catches up to its targts position


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        // Calculates the desired position based on player position and offset
        // The offset is applied in the direction the player is facing 
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);

        // Smoothly interpolate between the camera's current position and the desired position      
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;

        // Adjusts the camera's rotation to match the player's rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, smoothSpeed);
        
    }

}
