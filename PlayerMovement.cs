using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
    //Speed of movement
    public float speed = 12f;

    //gravity (adding physics)
    public float gravity = -9.81f * 2;
    //Set jump height
    public float jumpHeight = 3f;
    // Camera reference so that the camera follows where the player looks
    public Transform cameraTransform;
    //Position used to check if the player is touching the ground
    public Transform groundCheck;
    //radius of the ground check sphere
    public float groundDistance = 0.4f;
    // Layer used to determine what counts as the ground 
    public LayerMask groundMask;
    //Set the velocity as a vector
    Vector3 velocity;

    //Set booleans that will be used to check if player is on the ground or moving
    bool isOnGround;
    bool isMoving;

    //detect movement
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {   
        // Get the CharacterController component attached to this GameObject
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Ground check
        isOnGround = controller.isGrounded;
        //Resetting the default velocity
        if(isOnGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Receive Inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Get the direction the camera is facing
        Vector3 cameraForward = cameraTransform.forward;
        // Get the right direction relative to the camera
        Vector3 cameraRight = cameraTransform.right;

        //Set the vertical components to 0
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        //Normalize so that the diagonal movement isn't too fast
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Apply the final movement vector
        Vector3 move = cameraRight * x + cameraForward * z;
      
        //Check if the player is available to jump
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            //Jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Falling downwards
        velocity.y += gravity * Time.deltaTime; //gravity is similar to earth's gravity

        // Apply movement speed
        Vector3 finalMove = move * speed;
        // Apply vertical velocity
        finalMove.y = velocity.y;
        // Move the player using CharacterController
        controller.Move(finalMove * Time.deltaTime);

        // Check if the player has moved since the last frame and is on the ground
        if(lastPosition != gameObject.transform.position && isOnGround == true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        // Update lastPosition for the next frame comparison
        lastPosition = gameObject.transform.position;
    }
}
