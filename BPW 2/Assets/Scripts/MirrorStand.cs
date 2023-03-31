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
    public bool usable = true;
    public float interactTimer;
    public float interactCooldown;

    float xMirrorRotation = 0f;
    public bool clamped;
    public float clampMin;
    public float clampMax;

    public bool popupRequired;
    public GameObject popup;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Tooltip popup
        if (Vector3.Distance(transform.position, Player.transform.position) <= activationRange && popupRequired && usingMirror == false && interactTimer == 0)
        {
            popup.SetActive(true);
        }
        else if (Vector3.Distance(transform.position, Player.transform.position) > activationRange && Vector3.Distance(transform.position, Player.transform.position) <= (activationRange + 1) || usingMirror == true)
        {
            popup.SetActive(false);
        }

        if (interactTimer > 0)
        {
            interactTimer -= Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, Player.transform.position) <= activationRange && Input.GetKeyDown(KeyCode.Mouse0) && interactTimer <= 0 && usingMirror == false && usable == true)
        {
            usingMirror = true;
            interactTimer = interactCooldown;
            Player.SetActive(false);
            mirrorCam.SetActive(true);
        }

        if (usingMirror == true)
        {
            //Mirror controller
            float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;

            xMirrorRotation += mouseX;
            if (clamped)
            {
                xMirrorRotation = Mathf.Clamp(xMirrorRotation, clampMin, clampMax);
            }

            transform.localRotation = Quaternion.Euler(0f, xMirrorRotation, 0f);
            stand.Rotate(Vector3.up * mouseX);


            if (interactTimer <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    usingMirror = false;
                    interactTimer = interactCooldown;
                    Player.SetActive(true);
                    mirrorCam.SetActive(false);
                }
            }
        }

    }
}
