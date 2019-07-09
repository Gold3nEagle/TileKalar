using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class simply checks the color of the ground and if it was colored by the ball or not.
//And then sets the color accordingly
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
