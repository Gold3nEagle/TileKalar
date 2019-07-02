using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    Rigidbody ballRB;
    public float ballSpeed;
    public int minSwipeRecognition;
     

    private bool isMoving;
    private Color solveColor;

    Vector3 moveDirection, collisionPosition;
    Vector2 swipePositionLastFrame, swipePositionCurrentFrame, currentSwipe;



    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        solveColor = Random.ColorHSV(0, 1);
        GetComponent<MeshRenderer>().material.color = solveColor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isMoving)
        {
            ballRB.velocity = ballSpeed * moveDirection;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.15f);
        int i = 0;
            while(i < hitColliders.Length)
        {

           Ground ground = hitColliders[i].transform.GetComponent<Ground>();
            if(ground && !ground.isColoured)
            {
                ground.ColourChanger(solveColor);
            }
            i++;
        }

        if(collisionPosition != Vector3.zero)
        {
            if(Vector3.Distance(transform.position, collisionPosition) < 1)
            {
                isMoving = false;
                moveDirection = Vector3.zero;
                collisionPosition = Vector3.zero;
            }
        }

        if (isMoving) { return; }

        if (Input.GetMouseButton(0))
        {
            swipePositionCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
             
            if(swipePositionLastFrame != Vector2.zero)
            {
                currentSwipe = swipePositionCurrentFrame - swipePositionLastFrame;

                if(currentSwipe.sqrMagnitude < minSwipeRecognition)
                {
                    return;
                }

                currentSwipe.Normalize();

                //Swiped up or down
                if(currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                { 
                    if (currentSwipe.y > 0)
                    {
                        SetDestination(Vector3.forward); 
                    }
                    else
                        SetDestination(Vector3.back); 

                }

                //Swiped left or right
                if(currentSwipe.y > -0.5 && currentSwipe.y < 0.5)
                {
                    if (currentSwipe.x > 0)
                    {
                        SetDestination(Vector3.right);
                    }
                    else
                        SetDestination(Vector3.left);

                } 
            }

            swipePositionLastFrame = swipePositionCurrentFrame;

        } 

        if (Input.GetMouseButtonUp(0))
        {
            swipePositionLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
        }

    }
     
    private void SetDestination(Vector3 direction)
    {
        moveDirection = direction;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, 100f))
        {
            collisionPosition = hit.point;
        }

        isMoving = true;

    }

}
