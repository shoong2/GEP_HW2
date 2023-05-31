using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool checkPoint = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            checkPoint = true;
        }
    }
}
