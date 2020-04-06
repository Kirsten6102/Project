using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public int healthToAdd;

    private LevelManager theLevelManager;

	// Use this for initialization
	void Start ()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
	}
	

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            theLevelManager.AddHealth(healthToAdd);
            gameObject.SetActive(false);
        }
    }
}
