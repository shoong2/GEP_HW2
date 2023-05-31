using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Success : MonoBehaviour
{

    public TMP_Text success; 
    void Start()
    {
        success.text = "Score: "+((int)PlayerPrefs.GetFloat("Time")).ToString();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
