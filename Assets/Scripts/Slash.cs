using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private GameObject Katana;
    private Animator animator;
    private Animation slash;

    private Vector3 originPos;
    private Quaternion originRot;

    void Start()
    {
        Katana = GameObject.Find("katana");
        animator = Katana.GetComponent<Animator>();
        slash = Katana.GetComponent<Animation>();

        originPos = Katana.transform.position;
        originRot = Katana.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }

    private void Animate()
    {
        if (Input.GetMouseButton(1))
        {
            animator.enabled = true;
            StartCoroutine(wait());
            
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.6F);
        animator.enabled = false;
        Katana.transform.position = originPos;
        Katana.transform.rotation = originRot;
    }
}
