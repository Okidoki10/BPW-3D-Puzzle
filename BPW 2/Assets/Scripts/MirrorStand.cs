using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MirrorStand : MonoBehaviour
{

    public float turnSpeed;
    public Transform stand;
    public GameObject mirrorCam;

    public GameObject Player;
    public float activationRange;

    public bool usingMirror;
    public float interactTimer;
    public float interactCooldown;

    float xMirrorRotation = 0f;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (interactTimer > 0)
        {
            interactTimer -= Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, Player.transform.position) <= activationRange && Input.GetKeyDown(KeyCode.Mouse0) && interactTimer <= 0 && usingMirror == false)
        {
            usingMirror = true;
            interactTimer = interactCooldown;
            Player.gameObject.SetActive(false);
            mirrorCam.gameObject.SetActive(true);
        }

        if (usingMirror == true)
        {
            //Mirror controller
            float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;

            xMirrorRotation -= mouseX;

            transform.localRotation = Quaternion.Euler(0f, xMirrorRotation, 0f);
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

    }
}
