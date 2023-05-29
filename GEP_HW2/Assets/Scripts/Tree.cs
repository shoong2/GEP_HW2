using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    Animator anim;
    public GameObject applePos;
    BoxCollider box;
    public GameObject apple;


    private void Start()
    {
        anim = GetComponent<Animator>();
        box = applePos.GetComponent<BoxCollider>();
       
    }

    public void TreeSet()
    {
        anim.SetTrigger("Tree");
        Vector3 originPosition = applePos.transform.position;
        float range_X = box.bounds.size.x;
        float range_Z = box.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, originPosition.y, range_Z);
        Vector3 respawnPosition = originPosition + RandomPostion;

        Instantiate(apple, respawnPosition, Quaternion.identity);
    }
}
