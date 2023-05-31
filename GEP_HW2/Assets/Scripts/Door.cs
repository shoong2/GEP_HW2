using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    public bool end = false;
    public float degreeSpeed;

    public GameManager manager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(end && transform.rotation.y<=130)
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * degreeSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Sword")
        {
            end = true;
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {
        manager.Save();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Success");
    }
}
