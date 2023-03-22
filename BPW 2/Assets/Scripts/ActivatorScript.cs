using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorScript : MonoBehaviour
{

    public GameObject door;

    public void OnHit()
    {
        door.GetComponent<DoorScript>().OnActivation();
    }
    
}
