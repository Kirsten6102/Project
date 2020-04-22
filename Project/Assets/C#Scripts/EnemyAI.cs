using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour {


    public Transform target;
    public Transform enemyGFX;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //InvokeRepeating allows the UpdatePath function to repeat every .5 seconds
        InvokeRepeating("UpdatePath", 0f, .5f);
        
	}
	
	// FixedUpdate is called a certain number of times a second
	void FixedUpdate ()
    {
        if (path == null) 
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }else
        {
            reachedEndOfPath = false;
        }

        //normalized makes sure lengh of vector is 1
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        //flips enemy's graphics
        if (force.x >= 0.1f)
        {
            //enemyGFX.localScale = new Vector3(1f, 1f, 0f);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (force.x <= -0.1f)
        {
            //enemyGFX.localScale = new Vector3(-1f, 1f, 0f);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

    }


    void OnPathComplete(Path p)
    {
        //Debug.Log("we got a path, did it have an error?" + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    //Updates path and looks for player
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

}
