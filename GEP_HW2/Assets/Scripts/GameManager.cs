using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public float time = 180;
    public TMP_Text timer;

    public Door door;
    void Start()
    {
        timer.text = ((int)time).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(time>0 &&door.end == false)
            time -= Time.deltaTime;
        timer.text = ((int)time).ToString();

        if(time<=0)
        {
            SceneManager.LoadScene("End");
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("Time", time);
    }
}
