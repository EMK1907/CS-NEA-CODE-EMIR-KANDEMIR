using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{

    //Take rotations in both axis as floats
    float xRotation = 0f;
    float yRotation = 0f;


    public float mouseSensitivity = 400f; //sensitivity that will be changeable for the player


    //Creating the vertical clamp
    public float topClamp = -90f;
    public float bottomClamp = 90f;
    void Start()
    {
    //locking the cursor and making it invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void Update()
    {
        //Receiving mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        //Rotation to look up and down
        xRotation -= mouseY;


        //vertical clamp
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);


        //Rotation to look left and right
        yRotation += mouseX;


        //Apply the rotations
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);


    }
}
