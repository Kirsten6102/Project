﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoving : MonoBehaviour {

    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3 nextPos;

    public float moveSpeed;

    public Transform childTransform;

    //public Transform startPoint;
    public Transform endPoint;

	// Use this for initialization
	void Start ()
    {
        startPos = childTransform.localPosition;
        endPos = endPoint.localPosition;
        nextPos = endPos;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
	}

    public void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != startPos ? startPos : endPos;
    }
}