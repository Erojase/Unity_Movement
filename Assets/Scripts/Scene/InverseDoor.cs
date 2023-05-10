using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InverseDoor : MonoBehaviour
{

    public enum Iinversion
    {
        Up,
        Down,
        Left,
        Right
    }
    public Iinversion inversion = Iinversion.Up;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetType().ToString().Contains("CharacterController"))
        {
            inverse(other.gameObject);

        }
        
    }

    private void inverse(GameObject player)
    {
        PlayerMov mov = player.GetComponent<PlayerMov>();
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        switch (inversion)
        {
            case Iinversion.Up:
                Debug.Log("Going up");
                player.transform.localPosition += new Vector3(0, 1, 0);
                player.transform.rotation = new Quaternion(player.transform.rotation.x, player.transform.rotation.y, 180, player.transform.rotation.w);
                mov.gravity = -20;
                break;
            case Iinversion.Down:
                Debug.Log("Going down");
                player.transform.localPosition += new Vector3(0, -10, 0);
                player.transform.rotation = new Quaternion(player.transform.rotation.x, player.transform.rotation.y, 0, player.transform.rotation.w);
                mov.gravity = 20;
                break;
            case Iinversion.Left:
                Debug.Log("Going left");
                break;
            case Iinversion.Right:
                Debug.Log("Going right");
                break;
        }
        controller.enabled = true;


    }

}
