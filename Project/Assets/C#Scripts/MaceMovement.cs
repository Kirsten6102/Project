using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceMovement : MonoBehaviour {

    public Vector3 startPosition;
    public Vector3[] moveToPoints;
    public Vector3 currentPoint;

    public float moveSpeed;

    public int pointSelection;

    // Use this for initialization
    void Start()
    {

        //Sets the object to your starting point
        this.transform.position = startPosition;

    }

    // Update is called once per frame
    void Update()
    {

        Move();

    }


    void Move()
    {

        //Starts to move the object towards the first "moveToPoint" you set in inspector
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentPoint, Time.deltaTime * moveSpeed);

        //check to see if the object is at the next "moveToPoint"
        if (this.transform.position == currentPoint)
        {

            //if so it sets the next moveTo location
            pointSelection++;

            //if your object hits the last "moveToPoint it sends the object back to starting position to start the sequence over
            if (pointSelection == moveToPoints.Length)
            {
                pointSelection = 0;

            }

            //sets the destination of the "moveToPoint" destination
            currentPoint = moveToPoints[pointSelection];
        }
    }













    //public Transform leftPoint;
    //public Transform rightPoint;

    //public float movementSpeed;
    //public bool movingRight;
    //private Rigidbody2D enemyRB;

    //public Transform enemyGFX;

    //// Use this for initialization
    //void Start()
    //{
    //    enemyRB = GetComponent<Rigidbody2D>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (movingRight && transform.position.x > rightPoint.position.x)
    //    {
    //        movingRight = false;
    //        enemyGFX.localScale = new Vector3(1f, 1f, 0f);
    //    }
    //    if (!movingRight && transform.position.x < leftPoint.position.x)
    //    {
    //        movingRight = true;
    //        enemyGFX.localScale = new Vector3(-1f, 1f, 0f);

    //    }
    //    if (movingRight)
    //    {
    //        enemyRB.velocity = new Vector3(movementSpeed, enemyRB.velocity.y, 0f);
    //    }
    //    else
    //    {
    //        enemyRB.velocity = new Vector3(-movementSpeed, enemyRB.velocity.y, 0f);
    //    }
    //}
}

