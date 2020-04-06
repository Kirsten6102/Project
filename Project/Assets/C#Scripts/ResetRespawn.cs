using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRespawn : MonoBehaviour {

    //vector3 for position, quaternion for rotation
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startLocalScale;

    private Rigidbody2D myRB;

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startLocalScale = transform.localScale;

        if(GetComponent<Rigidbody2D>() != null)
        {
            myRB = GetComponent<Rigidbody2D>();
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ResetWorld()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startLocalScale;

        if(myRB != null)
        {
            myRB.velocity = Vector3.zero;
        }
    }

}
