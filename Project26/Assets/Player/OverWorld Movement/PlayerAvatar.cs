using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    //public float speed = 3f; // Speed of the player

    //private CharacterController controller; // Reference to the CharacterController component

    //// Start is called before the first frame update
    //void Start()
    //{
    //    // Attach CharacterController component to the Player GameObject
    //    controller = GetComponent<CharacterController>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // Gets input from the arrow keys
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");

    //    // Creates a movement vector based on the input and speed
    //    Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * speed * Time.deltaTime;

    //    // Moves the player
    //    controller.Move(movement);
    //}

    public CharacterController controller;
    public Transform cam;

    public Animator animator;

    public float speed = 6.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calculate the angle to rotate towards. Use the camera's y rotation to ensure forward is the direction the camera is facing
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Update the player's rotation
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the player
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        animator.SetBool("Running", direction.magnitude >= 0.1f);
    }
}
