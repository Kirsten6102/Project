using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRespawn : MonoBehaviour {

    //vector3 for position, quaternion for rotation
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;

    private Rigidbody2D playerRB;

	void Start ()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;

        if(GetComponent<Rigidbody2D>() != null)
        {
            playerRB = GetComponent<Rigidbody2D>();
        }

	}
	
    public void ResetLevel()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startScale;

        if(playerRB != null)
        {
            playerRB.velocity = Vector3.zero;
        }
    }

}
