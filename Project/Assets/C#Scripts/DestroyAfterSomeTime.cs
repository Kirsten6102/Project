using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSomeTime : MonoBehaviour {


    public float lifeTime;
    
	
	// Update is called once per frame
	void Update () {

        lifeTime = lifeTime - Time.deltaTime;

        if(lifeTime <= 0f )
        {
            Destroy(gameObject);
        }

	}
}
