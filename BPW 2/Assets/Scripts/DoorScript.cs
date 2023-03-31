using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public float activationTimer;
    public float baseActivationTime;

    public float requiredActiveNumber = 1;
    public float activeNumber = 0;

    public bool isOpen;
    public float openSpeed;
    public float minOpenSpeed;
    public float maxOpenSpeed;
    public float openIncrement;
    public float closeSpeed;
    public float minCloseSpeed;
    public float maxCloseSpeed;
    public float closeIncrement;

    public float topPos = 4.5f;
    public float bottomPos = 0;

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

        if (activeNumber >= requiredActiveNumber)
        {
            activationTimer = baseActivationTime;
        }

        activeNumber = 0;
    }

    public void OnActivation()
    {
        activeNumber++;
    }

    public void FixedUpdate()
    {
        if (isOpen && transform.position.y < topPos)
        {
            transform.Translate(openSpeed * Time.deltaTime * Vector3.forward);
            closeSpeed = minCloseSpeed;

            if (openSpeed <= maxOpenSpeed)
            {
                openSpeed += openIncrement;
            }
        }
        else if (!isOpen && transform.position.y > bottomPos)
        {
            transform.Translate(closeSpeed * Time.deltaTime * -Vector3.forward);
            openSpeed = minOpenSpeed;

            if (closeSpeed <= maxCloseSpeed)
            {
                closeSpeed += closeIncrement;
            }
        }
    }

}
