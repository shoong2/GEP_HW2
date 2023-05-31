using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    public float distance;
    GameObject target;
    public Animator anim;

    int randomNum;

    PlayerController player;

    public GameObject potion;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        player = target.GetComponent<PlayerController>();
        //anim = GetComponent<Animator>();
        randomNum = Random.Range(0, 2);
        Debug.Log(randomNum);
    }

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (Vector3.Distance(target.transform.position, transform.position) < distance)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            anim.SetBool("Attack", true);
        }
        else
        {
            agent.isStopped = true;
            anim.SetBool("Attack", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Sword" && player.isAttack)
        {
            Debug.Log(randomNum);
            if(randomNum==0)
            {
                Instantiate(potion, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }    
    }
}
