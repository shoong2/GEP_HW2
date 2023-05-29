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

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
            //&& Vector3.Distance(transform.position, player.transform.position) >2)
        {
            nav.SetDestination(player.transform.position);
            if (Vector3.Distance(transform.position, player.transform.position) < 2)
            {
                rid.velocity = Vector3.zero;
                nav.Stop();
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag =="Player")
        {
            Debug.Log("hi");
        }
    }
}
