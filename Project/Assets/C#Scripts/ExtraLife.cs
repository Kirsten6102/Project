using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour {

    public int lifeToGive;
    public int expPoints;
    private LevelManager levelManager;

	// Use this for initialization
	void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();
	}
	

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            levelManager.AddLife(lifeToGive);
            levelManager.experience += expPoints;
            levelManager.experienceForLvl += expPoints;
            gameObject.SetActive(false);
        }
    }
}
