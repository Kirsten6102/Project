using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    public Transform target;

    //update path 2 times per second
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rigidBody;

    //The calculted path
    public Path path;

    //The AI's speed per second
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]   //makes sure the bool does not show up in the inspector
    public bool pathEnded = false;

    //max distance from the AI to waypoint
    public float nextWaypointDistance = 3;
    
    private int currentWaypoint = 0;

    private bool searchPlayer = false;
       

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            if (!searchPlayer)
            {
                searchPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        
        }

        //Starts path from current position to player(target) position
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath ());

    }

    
    public void OnPathComplete(Path p)
    {
        Debug.Log("we got a path, did it have an error?" + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;        
        }
    }

    IEnumerator SearchForPlayer()
    {
        GameObject Result = GameObject.FindGameObjectWithTag("Player");
        if (Result == null)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(SearchForPlayer());
        }
        else 
        {
            target = Result.transform;
            searchPlayer = false;
            StartCoroutine(UpdatePath());
            yield return false;

        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            //TODO: insert player search here
            //return false;
        }

        //Starts path from current position to player(target) position
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        
        yield return new WaitForSeconds (1f/updateRate);
        StartCoroutine(UpdatePath());
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            //TODO: insert player search here
            return;
        }

        //TODO: Always look at player

        if (path == null)
            return;

        //checks if current waypoint is >= waypoint in array
        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathEnded)
                return;

            Debug.Log("End of path reached");
            pathEnded = true;
            return;
        }
        else 
        { 
            pathEnded = false;
        }
        
        //Direction to next waypoint. subtracting position from waypoint gives direction
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //move AI
        rigidBody.AddForce(dir, fMode);

        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

    }

}
