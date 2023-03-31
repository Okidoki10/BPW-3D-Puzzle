using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CarryableMirrorStand : MonoBehaviour
{

    public float turnSpeed;
    public Transform stand;
    public GameObject mirror;
    public GameObject mirrorCam;

    public GameObject Player;
    public float activationRange;

    public bool hasMirror;
    public bool usingMirror;
    public float interactTimer;
    public float interactCooldown;

    public float xRotation = 0f;

    public bool popupRequired;
    public GameObject popup;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Tooltip popup
        if (Vector3.Distance(transform.position, Player.transform.position) <= activationRange && popupRequired && hasMirror == true && interactTimer == 0)
        {
            popup.gameObject.SetActive(true);
        }
        else if (Vector3.Distance(transform.position, Player.transform.position) > activationRange && Vector3.Distance(transform.position, Player.transform.position) <= (activationRange + 1) && popupRequired || hasMirror == false && popupRequired)
        {
            popup.gameObject.SetActive(false);
        }

        //Slight cooldown between interactions with mirror
        if (interactTimer > 0)
        {
            interactTimer -= Time.deltaTime;
        }

        //Check if player is able to use mirror
        if (Vector3.Distance(transform.position, Player.transform.position) <= activationRange && Input.GetKeyDown(KeyCode.Mouse0) && interactTimer <= 0 && usingMirror == false && hasMirror == true)
        {
            usingMirror = true;
            interactTimer = interactCooldown;
            Player.SetActive(false);
            mirrorCam.SetActive(true);
        }

        //Mirror rotation
        if (usingMirror == true)
        {
            //Mirror controller
            float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;

            xRotation -= mouseX;

            transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
            stand.Rotate(Vector3.up * mouseX);


            if (interactTimer <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    usingMirror = false;
                    interactTimer = interactCooldown;
                    Player.gameObject.SetActive(true);
                    mirrorCam.gameObject.SetActive(false);
                }
            }
        }
        

        //Mirror on/off states
        if (hasMirror)
        {
            mirror.SetActive(true);
        } 
        else
        {
            mirror.SetActive(false);
        }

        //Place mirror on stand
        if (Vector3.Distance(transform.position, Player.transform.position) <= activationRange && Input.GetKeyDown(KeyCode.E) && interactTimer <= 0 && usingMirror == false && hasMirror == false && Player.GetComponent<PlayerMovement>().carryingMirror == true)
        {
            hasMirror = true;
            Player.GetComponent<PlayerMovement>().carryingMirror = false;
            interactTimer = interactCooldown;
        }

        //Take mirror from stand
        if (Vector3.Distance(transform.position, Player.transform.position) <= activationRange && Input.GetKeyDown(KeyCode.E) && interactTimer <= 0 && usingMirror == false && hasMirror == true && Player.GetComponent<PlayerMovement>().carryingMirror == false)
        {
            hasMirror = false;
            Player.GetComponent<PlayerMovement>().carryingMirror = true;
            interactTimer = interactCooldown;
        }


    }
}
