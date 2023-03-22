using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovableMirrorStand : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 3f;
    public GameObject stand;
    public float standDistanceX;
    public float standDistanceZ;
    public Vector3 standDistance;
    public GameObject currentSide;
    public GameObject oppositeSide;

    public GameObject Player;
    public float activationRange;

    public GameObject posXpoint;
    public GameObject negXpoint;
    public GameObject posZpoint;
    public GameObject negZpoint;

    public bool movingMirror;
    public float interactTimer;
    public float interactCooldown;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Slight cooldown between interactions with mirror
        if (interactTimer > 0)
        {
            interactTimer -= Time.deltaTime;
        }

        //Grab mirror from one of four sides
        if (Vector3.Distance(stand.transform.position, Player.transform.position) <= activationRange && Player.transform.position.x >= transform.position.x && Player.transform.position.z >= negZpoint.transform.position.z && Player.transform.position.z <= posZpoint.transform.position.z && Input.GetKeyDown(KeyCode.E) && interactTimer <= 0 && stand.GetComponent<MirrorStand>().usingMirror == false && movingMirror == false)
        {
            Debug.Log("POS X");
            Player.GetComponent<PlayerMovement>().moveEnabled = false;
            Player.GetComponentInChildren<MouseLook>().turnEnabled = false;
            movingMirror = true;
            interactTimer = interactCooldown;
            Player.transform.rotation = Quaternion.LookRotation(-transform.forward);
            standDistanceX = transform.position.x - Player.transform.position.x;
            standDistanceZ = transform.position.z - Player.transform.position.z;
            currentSide = posXpoint;
            oppositeSide = negXpoint;
        }

        if (Vector3.Distance(stand.transform.position, Player.transform.position) <= activationRange && Player.transform.position.x <= transform.position.x && Player.transform.position.z >= negZpoint.transform.position.z && Player.transform.position.z <= posZpoint.transform.position.z && Input.GetKeyDown(KeyCode.E) && interactTimer <= 0 && stand.GetComponent<MirrorStand>().usingMirror == false && movingMirror == false)
        {
            Debug.Log("NEG X");
            Player.GetComponent<PlayerMovement>().moveEnabled = false;
            Player.GetComponentInChildren<MouseLook>().turnEnabled = false;
            movingMirror = true;
            interactTimer = interactCooldown;
            Player.transform.rotation = Quaternion.LookRotation(transform.forward);
            standDistanceX = transform.position.x - Player.transform.position.x;
            standDistanceZ = transform.position.z - Player.transform.position.z;
            currentSide = negXpoint;
            oppositeSide = posXpoint;
        }

        if (Vector3.Distance(stand.transform.position, Player.transform.position) <= activationRange && Player.transform.position.z >= transform.position.z && Player.transform.position.x >= negXpoint.transform.position.x && Player.transform.position.x <= posXpoint.transform.position.x && Input.GetKeyDown(KeyCode.E) && interactTimer <= 0 && stand.GetComponent<MirrorStand>().usingMirror == false && movingMirror == false)
        {
            Debug.Log("POS Z");
            Player.GetComponent<PlayerMovement>().moveEnabled = false;
            Player.GetComponentInChildren<MouseLook>().turnEnabled = false;
            movingMirror = true;
            interactTimer = interactCooldown;
            Player.transform.rotation = Quaternion.LookRotation(transform.right);
            standDistanceX = transform.position.x - Player.transform.position.x;
            standDistanceZ = transform.position.z - Player.transform.position.z;
            currentSide = posZpoint;
            oppositeSide = negZpoint;
        }

        if (Vector3.Distance(stand.transform.position, Player.transform.position) <= activationRange && Player.transform.position.z <= transform.position.z && Player.transform.position.x >= negXpoint.transform.position.x && Player.transform.position.x <= posXpoint.transform.position.x && Input.GetKeyDown(KeyCode.E) && interactTimer <= 0 && stand.GetComponent<MirrorStand>().usingMirror == false && movingMirror == false)
        {
            Debug.Log("NEG Z");
            Player.GetComponent<PlayerMovement>().moveEnabled = false;
            Player.GetComponentInChildren<MouseLook>().turnEnabled = false;
            movingMirror = true;
            interactTimer = interactCooldown;
            Player.transform.rotation = Quaternion.LookRotation(-transform.right);
            standDistanceX = transform.position.x - Player.transform.position.x;
            standDistanceZ = transform.position.z - Player.transform.position.z;
            currentSide = negZpoint;
            oppositeSide = posZpoint;
        }

        //Let go of mirror
        if (Input.GetKeyDown(KeyCode.E) && interactTimer <= 0 && stand.GetComponent<MirrorStand>().usingMirror == false)
        {
            movingMirror = false;
            Player.GetComponent<PlayerMovement>().moveEnabled = true;
            Player.GetComponentInChildren<MouseLook>().turnEnabled = true;
        }

        if (movingMirror)
        {
            float z = Input.GetAxis("Vertical");

            Vector3 move = Player.transform.forward * z + Player.transform.up * 0;


            if (z > 0 && oppositeSide.GetComponent<MovablePlatePoints>().movable)
            {
                Player.GetComponent<PlayerMovement>().controller.Move(move * speed * Time.deltaTime);
            }

            if (z < 0 && currentSide.GetComponent<MovablePlatePoints>().movable)
            {
                Player.GetComponent<PlayerMovement>().controller.Move(move * speed * Time.deltaTime);
            }

            standDistance = new Vector3(Player.transform.position.x + standDistanceX, transform.position.y, Player.transform.position.z + standDistanceZ);
            transform.position = standDistance;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.name == "posXpoint")
        {

        }
    }


}
