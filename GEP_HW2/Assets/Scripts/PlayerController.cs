using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public Sprite swordImg;

    public int appleNum = 0;
    public int nowSlot=1;
    public RectTransform selectItem;

    public GameObject handApple;
    public GameObject handSword;
    public GameObject hand;
    public GameObject makeApple;

    public NPC npc;

    public Slider hpBar;
    public float maxHp;
    public float curHp;

    public bool isAttack = false;
    public float attackTime;

    AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip swing;

    public CheckPoint checkPoint;
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        curHp = maxHp;
    }

    void PlaySound(string action)
    {
        switch(action)
        {
            case "Jump":
                audioSource.clip = jumpSound;
                break;
            case "Attack":
                audioSource.clip = swing;
                break;
        }
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        hpBar.value = curHp / maxHp;
        if(transform.position.y<-15 || curHp<=0)
        {
            SceneManager.LoadScene("End");
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlaySound("Jump");
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            playerAnim.SetTrigger("Jump");
            isGrounded = false;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(isTree)
            {
                PlaySound("Attack");
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
            SelectSlot(1);
            nowSlot = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(2);
            selectItem.anchoredPosition = new Vector3(-62, 67, 0);
            nowSlot = 2;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(nowSlot ==1 && appleNum>0)
            {
                Instantiate(makeApple, hand.transform.position, Quaternion.identity);
                CheckItem("Apple_");
            }
            else if(nowSlot ==2)
            {
                PlaySound("Attack");
                playerAnim.SetTrigger("Attack");
                StartCoroutine(Attack());
            }
        }

    }

    IEnumerator Attack()
    {
        isAttack = true;
        yield return new WaitForSeconds(attackTime);
        isAttack = false;
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


        if(other.tag == "EnemyAttack")
        {
            curHp -= 10;
        }

        if(other.tag=="Potion")
        {
            curHp += 10;
            Destroy(other.gameObject);
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
        if (nowSlot == 1)
            SelectSlot(1);


    }

    public void CheckItem(string item)
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
                else if(item == "Sword")
                {
                    slot[i].sprite = swordImg;
                    slot[i].gameObject.SetActive(true);
                }

            }
            else if(slot[i].sprite.name =="apple")
            {
                if(item=="Apple")
                    appleNum++;
                else if(item =="Apple_")
                {
                    if(appleNum>0)
                        appleNum--;
                    if(appleNum<=0)
                    {
                        appleNum = 0;
                        slot[i].gameObject.SetActive(false);
                        handApple.SetActive(false);
                    }
                }
                count[i].text = appleNum.ToString();
            }
        }
    }

    void SelectSlot(int slot)
    {
        if(slot==1)
        {
            handSword.SetActive(false);
            if (appleNum != 0)
                handApple.SetActive(true);
            else
                handApple.SetActive(false);
        }

        if (slot==2)
        {
            handApple.SetActive(false);
            if(npc.isQuest)
                handSword.SetActive(true);
        }
    }
}
