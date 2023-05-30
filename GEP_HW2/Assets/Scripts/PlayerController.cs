using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public float waitPunch;
    public float waitApple;

    public bool isGrounded = true;
    bool isTree = false;
    bool getItem = false;

    Animator playerAnim;
    Rigidbody rigid;

    public TMP_Text guideText;

    Tree tree;
    GameObject item;

    public Image[] slot;
    public TMP_Text[] count;
    public Sprite appleImg;

    int appleNum = 0;

    public RectTransform selectItem;
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
                StartCoroutine(Gathering());
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectItem.anchoredPosition = new Vector3(-272, 67, 0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectItem.anchoredPosition = new Vector3(-62, 67, 0);
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
            isTree = false;
            guideText.text = "F키를 눌러 아이템을 획득하기";
            guideText.gameObject.SetActive(true);
            item = other.gameObject;
            getItem = true;
        }

        else if (other.tag =="Tree")
        {
            getItem = false;
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

    IEnumerator Gathering()
    {
        playerAnim.SetTrigger("Gathering");
        yield return new WaitForSeconds(waitApple);
        Destroy(item);
        getItem = false;
        //guideText.gameObject.SetActive(false);
        //appleNum++;
        //for(int i=0; i<slot.Length; i++)
        //{
        //    if(slot[i].sprite ==null)
        //    {
        //        if (appleNum == 0)
        //        {
        //            slot[i].sprite = appleImg;
        //            slot[i].gameObject.SetActive(true);

        //            yield break;
        //        }
        //    }
        //}
        CheckItem("Apple");

        
    }

    void CheckItem(string item)
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].sprite == null)
            {
                if (appleNum == 0 && item =="Apple")
                {
                    appleNum++;
                    count[i].text = appleNum.ToString();
                    slot[i].sprite = appleImg;
                    slot[i].gameObject.SetActive(true);

                    break;
                }

            }
            else if(slot[i].sprite.name =="apple")
            {
                appleNum++;
                count[i].text = appleNum.ToString();
            }
        }
    }

}
