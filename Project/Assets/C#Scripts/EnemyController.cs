using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
    public float movementSpeed;
    private bool canMove;
    private Rigidbody2D enemyRigidbody;

	// Use this for initialization
	void Start () 
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (canMove)
        {
            enemyRigidbody.velocity = new Vector3(-movementSpeed, enemyRigidbody.velocity.y, 0f);      
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
            Destroy(gameObject);
        }
    }

}
