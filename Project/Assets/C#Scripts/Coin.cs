using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private LevelManager levelManager;
    public int coinValue;
    public int expPoints;

	// Use this for initialization
	void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();
	}
	

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            levelManager.AddCoins(coinValue);
            levelManager.experience += expPoints;
            levelManager.experienceForLvl += expPoints;
            
            gameObject.SetActive(false);
        }

    }


}
