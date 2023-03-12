using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPGate : MonoBehaviour
{
    public Vector3 Teleport = new Vector3(0,0,0);
    public Quaternion facingExit = new Quaternion();

    [Space]
    [Space]
    [Space]
    public bool canTP = true;
    private int cooldown = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (canTP)
        {
            GameObject player = other.gameObject;
            CharacterController controller = player.GetComponent<CharacterController>();
            controller.enabled = false;
            player.transform.rotation = facingExit;
            player.transform.position = Teleport;
            controller.enabled = true;
            StartCoroutine(wait());
        }
        
    }

    IEnumerator wait()
    {
        canTP = false;
        yield return new WaitForSeconds(cooldown);
        canTP = true;
    }
}
