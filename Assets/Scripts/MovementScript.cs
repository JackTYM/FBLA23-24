using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public float movementPower = 5f;
    public float jumpPower = 2f;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public int gravityTicks = 150;
    float currentYVelo = 0f;

    CharacterController controller;
    int jumpCount = 0;
    int lastFrame = 0;
    int lastJumpFrame = 0;
    bool jumpHeld = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(controller.isGrounded);
        if (!controller.isGrounded) {
            currentYVelo -= (jumpPower / gravityTicks) * movementPower;
        }

        if (controller.isGrounded && lastFrame < lastJumpFrame + 150) {
            currentYVelo = 0;
        }

        if (Input.GetKey(KeyCode.Space) && controller.isGrounded && lastFrame > lastJumpFrame + 150 && !jumpHeld) {
            currentYVelo = jumpPower*movementPower;
            jumpCount++;
            lastJumpFrame = lastFrame++;
        }

        controller.Move((Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward)*Time.deltaTime*movementPower);
        
        // Camera Rotate

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        pitch = Math.Max(-80, Math.Min(80, pitch));

        transform.GetChild(0).eulerAngles = new Vector3(pitch, yaw, 0.0f);
        transform.eulerAngles = new Vector3(0, transform.GetChild(0).eulerAngles.y, 0);

        controller.Move(new Vector3(0, currentYVelo*Time.deltaTime, 0));

        jumpHeld = Input.GetKey(KeyCode.Space);
    }
}