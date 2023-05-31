using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NPC : MonoBehaviour
{
    public GameObject player;
    public float distance;
    Rigidbody rid;

    public bool isQuest = false;

    public GameObject cam1;
    public GameObject cam2;

    public TMP_Text chat;

    public PlayerController playerControl;
    void Start()
    {
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
        if(other.tag =="Player" &&!isQuest)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            chat.gameObject.SetActive(true);
            if(playerControl.nowSlot ==1 && playerControl.appleNum>0)
            {
                StartCoroutine(Clear());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag =="Player")
        {
            cam2.SetActive(false);
            cam1.SetActive(true);
            chat.gameObject.SetActive(false);
        }
    }
    
    IEnumerator Clear()
    {
        yield return new WaitForSeconds(0.8f);
        playerControl.CheckItem("Apple_");
        chat.text = "고마워 막대기를 줄게";
        playerControl.CheckItem("Sword");
        isQuest = true;
    }
}
