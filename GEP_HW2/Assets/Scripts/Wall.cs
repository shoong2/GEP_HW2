using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Button[] button;
    public GameObject[] wall;

    private void Update()
    {
        if(button[0].isClick && button[1].isClick)
        {
            wall[0].transform.Translate(new Vector3(0.01f, 0,0 ));
            wall[1].transform.Translate(new Vector3(0.01f, 0, 0));
        }
    }
}
