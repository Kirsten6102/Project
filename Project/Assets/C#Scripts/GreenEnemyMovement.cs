using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyMovement : MonoBehaviour 
{
    public Transform leftPoint;
    public Transform rightPoint;

    public float movementSpeed;
    public bool movingRight;
    private Rigidbody2D enemyRigidbody;

	// Use this for initialization
	void Start () 
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (movingRight && transform.position.x > rightPoint.position.x)
        {
            movingRight = false;
        }
        if (!movingRight && transform.position.x < leftPoint.position.x)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            enemyRigidbody.velocity = new Vector3(movementSpeed, enemyRigidbody.velocity.y, 0f);
        } else 
        {
            enemyRigidbody.velocity = new Vector3(-movementSpeed, enemyRigidbody.velocity.y, 0f);
        }
	}
}
