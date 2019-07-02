using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public bool isColoured = false;

    public void ColourChanger(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
        isColoured = true;

        GameController.singleton.CheckFinished();
    } 
}
