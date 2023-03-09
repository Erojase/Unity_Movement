using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMov : MonoBehaviour
{
    private GameObject m_player;
    private Rigidbody m_body;
    private Camera m_camera;

    public Vector3 SpawnPoint = new Vector3(0, 5, 0);

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    public int sensitivity = 5;

    public float m_speed = 500.0F;
    public float jumpSpeed = 1.5F;
    public float gravity = 10.0F;

    private int jumpCount = 0;
    private bool jumping = false;
    private bool forceWalk = false;

    private bool crouched = false;

    // Start is called before the first frame update
    void Start()
    {
        m_player = this.gameObject;
        m_camera = Camera.main;
        m_body = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {   
        DoubleJump();
        Moves();
        Rotation();

        Fall();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(controller.isGrounded);
        if (collision.gameObject.tag == "floor")
        {
            jumping = false;
        }
        else if (collision.gameObject.tag == "wall_x")
        {
            jumpCount = 0;
            moveDirection.x *= 0.25F;
        }
        else if (collision.gameObject.tag == "wall_z")
        {
            jumpCount = 0;
            moveDirection.z *= 0.25F;
        }
        else if (collision.gameObject.tag == "inverse_floor")
        {
            forceWalk = true;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "wall_x")
        {
            WallRunX();

        } else if (collision.gameObject.tag == "wall_z")
        {
            WallRunZ();

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Contains("wall"))
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Inverse"))
        {
            m_player.transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z + 180));
            gravity = -gravity;
        }
    }

    public void Rotation()
    {
        var c = Camera.main.transform;
        var p = m_player.transform;
        p.Rotate(0, Input.GetAxis("Mouse X") / 2 * sensitivity, 0);
        c.Rotate(-Input.GetAxis("Mouse Y") / 2 * sensitivity, 0, 0);
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void WallRunX()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.y = 0;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            moveDirection.y = 10;
            moveDirection.x = -moveDirection.x * 1.5F;
        }
    }
    public void WallRunZ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.y = 0;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            moveDirection.y = 10;
            moveDirection.z = -moveDirection.z * 1.5F;

        }
    }

    public void DoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumping && (jumpCount > 0))
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection) * (m_speed * 5);
            moveDirection.y = jumpSpeed;
            jumpCount++;
        }
    }
    public void Moves()
    {

        if (controller.isGrounded || forceWalk)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection) * (m_speed * 5);


            // Jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
                jumping = true;
                jumpCount++;

            }
        }
        //Run
        if (Input.GetKeyDown(KeyCode.LeftShift) && !crouched)
        {
            m_speed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !crouched)
        {
            m_speed /= 2;
        }

        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            m_player.transform.localScale = transform.localScale / 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            m_player.transform.localScale = transform.localScale * 2;
        }


        if (jumpCount >= 2)
        {
            jumpCount = 0;
            jumping = false;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void Fall()
    {
        if (transform.position.y < -5)
        {
            transform.position = SpawnPoint;
        }
    }
}
