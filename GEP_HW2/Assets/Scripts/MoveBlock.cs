using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    public GameObject block;
    Button blockScript;
    bool come = false;
    private void Start()
    {
        blockScript = block.GetComponent<Button>();
    }

    private void Update()
    {
        if(blockScript.isClick &&!come)
        {
            if(gameObject.transform.position.z>=72)
            {
                transform.Translate(new Vector3(0, 0, -0.01f));
            }
        }

        if(come)
        {
            if (gameObject.transform.position.z <= 81.57f)
                transform.Translate(new Vector3(0, 0, 0.01f));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag =="Player")
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag =="Player" && blockScript.isClick)
        {
            come = true;
          
        }
    }


}
