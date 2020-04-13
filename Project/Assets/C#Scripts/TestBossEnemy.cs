using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossEnemy : MonoBehaviour {

    public int health;

    public GameObject deathParticales;

    //public GameObject levelComplete;
    public GameObject levelWin;

    public void TakeDamage(int Damage)
    {
        health -= Damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathParticales, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        levelWin.SetActive(true);
        //levelComplete.SetActive(true);
    }

}
