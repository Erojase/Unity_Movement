using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Grappling : MonoBehaviour
{

    private RaycastHit hit;
    private GameObject End;

    private GameObject player;
    private CharacterController controller;

    private GameObject currHit;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        End = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        controller = player.GetComponent<CharacterController>();

        Destroy(End.GetComponent<Collider>());

        End.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);


        Material maerial = Resources.Load("GrapplePoint") as Material;
        Debug.Log(maerial.ToString());
        End.GetComponent<Renderer>().material = maerial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 30))
        {
            End.transform.position = hit.point;
        }

        if (Input.GetMouseButtonDown(1))
        {
            currHit = hit.transform.gameObject;
            controller.enabled = false;
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startColor= Color.red;
            lineRenderer.endColor= Color.red;

            SpringJoint joint = currHit.AddComponent<SpringJoint>();
            currHit.GetComponent<Rigidbody>().isKinematic = true;

            joint.autoConfigureConnectedAnchor = false;
            joint.connectedBody = player.GetComponent<Rigidbody>();



            float distanceFromPoint = Vector3.Distance(player.transform.position, hit.point);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 10f;
            joint.damper = 0.2f;
            joint.massScale = 1f;


            // set width of the renderer
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            

            // set the position
            lineRenderer.SetPosition(0, player.transform.position);
            lineRenderer.SetPosition(1, hit.point);

            
        }
        if (Input.GetMouseButtonUp(1))
        {
            Destroy(GetComponent<LineRenderer>());
            Destroy(currHit.GetComponent<SpringJoint>());
            controller.enabled = true;
        }
        try
        {
            GetComponent<LineRenderer>().SetPosition(0, player.transform.position);
        }
        catch (System.Exception)
        {
        }
            

    }
}
