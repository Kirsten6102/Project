﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public int health;

    public GameObject deathParticales;

    public GameObject levelComplete;

    private LevelManager theLevelManager;

    // Use this for initialization
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
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
        gameObject.SetActive(false);
        levelComplete.SetActive(true);
    }

   
}
