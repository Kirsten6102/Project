using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public int health;

    public GameObject deathParticales;


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
    }

   
}
