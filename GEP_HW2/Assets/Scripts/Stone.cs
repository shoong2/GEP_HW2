using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Transform pos;
    public CheckPoint check;

    public float power;
    Rigidbody rid;

    private void Start()
    {
        Debug.Log(check.checkPoint);
        rid = GetComponent<Rigidbody>();
        if(!check.checkPoint)
            rid.isKinematic = true;
    }
    private void Update()
    {
        if(check.checkPoint)
        {
            rid.isKinematic = false;
        }

        if(transform.position.y<-15f)
        {
            transform.position = pos.position;
            rid.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {
            Debug.Log("Crash");
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back*power, ForceMode.Impulse);
        }
    }
}
