using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPGate : MonoBehaviour
{
    public Vector3 Teleport = new Vector3(0,0,0);


    private void OnTriggerEnter(Collider other)
    {
        GameObject player = other.gameObject;
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        player.transform.position = Teleport;
        controller.enabled = true;

        Camera.main.
    }
}
