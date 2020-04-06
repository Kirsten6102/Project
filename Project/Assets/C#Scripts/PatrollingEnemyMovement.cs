using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyMovement : MonoBehaviour 
{
    public Transform leftPoint;
    public Transform rightPoint;

    public float movementSpeed;
    public bool movingRight;
    private Rigidbody2D enemyRB;

    public Transform enemyGFX;

    // Use this for initialization
    void Start () 
    {
        enemyRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (movingRight && transform.position.x > rightPoint.position.x)
        {
            movingRight = false;
            enemyGFX.localScale = new Vector3(1f, 1f, 0f);
        }
        if (!movingRight && transform.position.x < leftPoint.position.x)
        {
            movingRight = true;
            enemyGFX.localScale = new Vector3(-1f, 1f, 0f);
            
        }
        if (movingRight)
        {
            enemyRB.velocity = new Vector3(movementSpeed, enemyRB.velocity.y, 0f);
        } else 
        {
            enemyRB.velocity = new Vector3(-movementSpeed, enemyRB.velocity.y, 0f);
        }
	}
}
