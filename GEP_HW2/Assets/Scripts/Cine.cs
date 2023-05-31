using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cine : MonoBehaviour
{
    public float wait; 
    void Start()
    {
        StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene("Main");
    }

}
