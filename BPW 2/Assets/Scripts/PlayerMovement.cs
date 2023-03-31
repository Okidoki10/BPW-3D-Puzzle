using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 6f;
    public float walkSpeed = 6f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 4f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;


    public Transform groundCheck;
    public float groundDistance = 0.4f; 
    public LayerMask groundMask;

    public bool moveEnabled = true;
    public bool carryingMirror;
    GameObject mirror;

    public Transform playerCam;

    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        mirror = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (moveEnabled)
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump") && isGrounded && moveEnabled)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Carrying mirror states
        if (carryingMirror)
        {
            mirror.SetActive(true);
        }
        else
        {
            mirror.SetActive(false);
        }

        //Sprinting
        if (Input.GetKey(KeyCode.LeftControl) && moveEnabled)
        {
            speed = sprintSpeed;
        } else
            if (speed == sprintSpeed)
        {
            speed = walkSpeed;
        }

        //Crouching
        if (Input.GetKey(KeyCode.LeftShift) && moveEnabled)
        {
            speed = crouchSpeed;
            gameObject.GetComponentInChildren<CharacterController>().height = 2.0f;
        }
        else
          if (speed == crouchSpeed)
        {
            speed = walkSpeed;
            gameObject.GetComponentInChildren<CharacterController>().height = 3.8f;
        }

    }
}
