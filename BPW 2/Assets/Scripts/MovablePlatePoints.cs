using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatePoints : MonoBehaviour
{

    public bool movable = true;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "MovableMirrorZone")
        {
            movable = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "MovableMirrorZone")
        {
            movable = false;
        }
    }
}
