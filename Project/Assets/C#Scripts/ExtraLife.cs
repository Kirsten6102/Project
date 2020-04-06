using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour {

    public int lifeToGive;
    private LevelManager theLevelManager;

	// Use this for initialization
	void Start ()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            theLevelManager.AddLife(lifeToGive);
            gameObject.SetActive(false);
        }
    }
}
