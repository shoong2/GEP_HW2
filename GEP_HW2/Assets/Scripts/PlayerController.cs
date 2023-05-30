using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float waitPunch;

    public bool isGrounded = true;
    bool isTree = false;
    bool getItem = false;

    Animator playerAnim;
    Rigidbody rigid;

    public TMP_Text guideText;

    Tree tree;
    GameObject item;

    int appleNum = 0;
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {           
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            playerAnim.SetTrigger("Jump");
            isGrounded = false;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(isTree)
            {
                StartCoroutine(ShakeTree());
            }

            if(getItem)
            {
                Destroy(item);
                appleNum++;
            }
        }

    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        direction.Normalize();

        Vector3 velocity = direction * speed;

        transform.position += velocity * Time.deltaTime;

        transform.LookAt(transform.position + direction);

        playerAnim.SetBool("Walk", direction != Vector3.zero);

        //if(Input.GetKey(KeyCode.LeftShift) && direction!= Vector3.zero)
        //{
        //    playerAnim.SetBool("Run", true);
        //}

        playerAnim.SetBool("Run", Input.GetKey(KeyCode.LeftShift) && direction!=Vector3.zero);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {       
            isGrounded = false;
        }

        //if (collision.gameObject.tag == "Tree")
        //{
        //    guideText.gameObject.SetActive(false);
        //}

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Apple")
        {
            guideText.text = "F키를 눌러 아이템을 획득하기";
            guideText.gameObject.SetActive(true);
            item = other.gameObject;
            getItem = true;
        }

        else if (other.tag =="Tree")
        {
            guideText.text = "F키를 눌러 나무를 흔들기";
            guideText.gameObject.SetActive(true);
            isTree = true;
            tree = other.gameObject.GetComponent<Tree>();
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag =="Tree")
        {
            guideText.gameObject.SetActive(false);
            isTree = false;
        }

        if(other.tag =="Apple")
        {
            guideText.gameObject.SetActive(false);
            getItem = false;
        }
    }

    IEnumerator ShakeTree()
    {
        playerAnim.SetTrigger("Shake");
        yield return new WaitForSeconds(waitPunch);
        tree.TreeSet();
        
    }

}
