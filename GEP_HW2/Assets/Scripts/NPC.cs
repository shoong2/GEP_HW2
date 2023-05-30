using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPC : MonoBehaviour
{
    public GameObject player;
    public float distance;
    NavMeshAgent nav;
    Rigidbody rid;

    public GameObject cam1;
    public GameObject cam2;

    public GameObject chat;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        rid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //transform.LookAt(player.transform);
    //    if(Vector3.Distance(transform.position, player.transform.position) <3f)
    //    {
    //        nav.SetDestination(player.transform.position);
    //    }

    //}

    //private void FixedUpdate()
    //{
    //    if (Vector3.Distance(transform.position, player.transform.position) < distance)
    //        //&& Vector3.Distance(transform.position, player.transform.position) >2)
    //    {
    //        nav.SetDestination(player.transform.position);
    //        if (Vector3.Distance(transform.position, player.transform.position) < 2)
    //        {
    //            rid.velocity = Vector3.zero;
    //            nav.Stop();
    //        }

    //    }
    //}

    private void Update()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            chat.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag =="Player")
        {
            cam2.SetActive(false);
            cam1.SetActive(true);
            chat.SetActive(false);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.transform.tag =="Player")
    //    {
    //        cam1.SetActive(false);
    //        Debug.Log("hi");
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.transform.tag == "Player")
    //    {
    //        cam2.SetActive(false);
    //        cam1.SetActive(true);
    //        Debug.Log("hi");
    //    }
    //}
}
