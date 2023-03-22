using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public float activationTimer;
    public float baseActivationTime;

    public bool isOpen;
    public float openSpeed;
    public float minOpenSpeed;
    public float maxOpenSpeed;
    public float openIncrement;
    public float closeSpeed;
    public float minCloseSpeed;
    public float maxCloseSpeed;
    public float closeIncrement;

    public void Update()
    {
        if (activationTimer > 0)
        {
            activationTimer -= Time.deltaTime;
            isOpen = true;
        } 
        else
        {
            isOpen = false;
        }
    }

    public void OnActivation()
    {   
        activationTimer = baseActivationTime;
    }

    public void FixedUpdate()
    {
        if (isOpen && transform.position.y < 4.5f)
        {
            transform.Translate(Vector3.forward * openSpeed * Time.deltaTime);
            closeSpeed = minCloseSpeed;

            if (openSpeed <= maxOpenSpeed)
            {
                openSpeed += openIncrement;
            }
        }
        else if (!isOpen && transform.position.y > 0)
        {
            transform.Translate(-Vector3.forward * closeSpeed * Time.deltaTime);
            openSpeed = minOpenSpeed;

            if (closeSpeed <= maxCloseSpeed)
            {
                closeSpeed += closeIncrement;
            }
        }
    }

}
