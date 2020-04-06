using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//only used for 'dumb' enemies

public class DumbEnemyController : MonoBehaviour 
{
    public float movementSpeed;
    private bool canMove;
    private Rigidbody2D enemyRB;

	// Use this for initialization
	void Start () 
    {
        enemyRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (canMove)
        {
            enemyRB.velocity = new Vector3(-movementSpeed, enemyRB.velocity.y, 0f);      
        }
	}

    void OnBecameVisible()
    {
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DeathZone")
        {
            gameObject.SetActive(false);
        }
    }

    //stops 'dumb' enemies walking as soon as they are respawned
    //they will wait until player is back in view
    void OnEnable()
    {
        canMove = false;
    }


}
