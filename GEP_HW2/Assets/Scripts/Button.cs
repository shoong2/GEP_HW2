using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Renderer render;
    //M/aterial mat;

    public bool isClick = false;

    private void Start()
    {
        render = GetComponent<Renderer>();
        //mat = render.GetComponent<Material>();
    }
    private void OnCollisionStay(Collision collision)
    {
        render.material.color = new Color(255, 0, 0);
        isClick = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        render.material.color = new Color(255, 255, 255);
        isClick = false;
    }
}
