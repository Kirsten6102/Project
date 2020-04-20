using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public int health;
    public int expPoints;

    public GameObject deathParticales;

    public GameObject levelComplete;

    private LevelManager levelManager;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }


    public void TakeDamage (int Damage)
    {
        health -= Damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathParticales, transform.position, Quaternion.identity);
        levelManager.experience += expPoints;
        levelManager.experienceForLvl += expPoints;
        gameObject.SetActive(false);
        levelComplete.SetActive(true);
    }

   
}
