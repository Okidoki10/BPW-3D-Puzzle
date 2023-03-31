using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public Vector3 rotateSpeed;

    public bool popupRequired;
    public GameObject popup;
    public GameObject winPopup;

    public float activationRange;
    public GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime);

        //Tooltip popup
        if (Vector3.Distance(transform.position, Player.transform.position) <= activationRange && popupRequired)
        {
            popup.gameObject.SetActive(true);
        }
        else if (Vector3.Distance(transform.position, Player.transform.position) > activationRange && Vector3.Distance(transform.position, Player.transform.position) <= (activationRange + 1) && popupRequired)
        {
            popup.gameObject.SetActive(false);
        }

        //Take orb
        if (Vector3.Distance(transform.position, Player.transform.position) <= activationRange && Input.GetKeyDown(KeyCode.E))
        {
            popup.gameObject.SetActive(false);
            winPopup.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
