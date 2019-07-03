using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    
    public float ballSpeed;
     
    private bool isMoving;
    private Color squareColor;

    Rigidbody ballRB;
    int minSwipeRecognition = 500;
    Vector3 moveDirection, collisionPosition;
    Vector2 swipePositionLastFrame, swipePositionCurrentFrame, currentSwipe;



    //Initializing the ball's Rigidbody component, generating a random color, setting the color on the ball.
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        squareColor = Random.ColorHSV(0, 1);
        GetComponent<MeshRenderer>().material.color = squareColor;
    }
     
    //Calling the update method for a fixed time in one frame.
    void FixedUpdate()
    { 
        CheckMovement(); 
    }
     
    //Casting a ray to determine if there is a collider at the end for the ball to reach to. 
    private void SetDestination(Vector3 direction)
    {
        moveDirection = direction;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, moveDirection, out hit, 100f))
        {
            collisionPosition = hit.point;
        }

        isMoving = true;
    }

    //Check and set movement and swipes for the ball.
    void CheckMovement()
    {
        //If the ball is allowed to move, multiply the speed we have set to its direction.
        if (isMoving)
        {
            ballRB.velocity = ballSpeed * moveDirection;
        }

        //Create a small collider on the ball to use as a detector for uncolored groundSquares to color the ones we pass on.
        Collider[] ballTouchCollider = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.15f);

        int i = 0;
        while (i < ballTouchCollider.Length)
        { 
            Ground ground = ballTouchCollider[i].transform.GetComponent<Ground>();
            if (ground && !ground.isColoured)
            {
                ground.ColourChanger(squareColor);
            }
            i++;
        }

        // Checking if we have reached our destination.
        if (collisionPosition != Vector3.zero)
        {

            if (Vector3.Distance(transform.position, collisionPosition) < 1)
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

            if (swipePositionLastFrame != Vector2.zero)
            {
                currentSwipe = swipePositionCurrentFrame - swipePositionLastFrame;

                if (currentSwipe.sqrMagnitude < minSwipeRecognition)
                {
                    return;
                }

                currentSwipe.Normalize();

                //Swiped up or down
                if (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    if (currentSwipe.y > 0)
                    {
                        SetDestination(Vector3.forward);
                    }
                    else
                        SetDestination(Vector3.back);

                }

                //Swiped left or right
                if (currentSwipe.y > -0.5 && currentSwipe.y < 0.5)
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

}
