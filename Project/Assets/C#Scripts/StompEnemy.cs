using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour {

    private Rigidbody2D playerRB;

    public float bounceForce;

    public GameObject enemyDeathParticals;

    void Start()
    {
        playerRB = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);

            Instantiate(enemyDeathParticals, other.transform.position, other.transform.rotation);

            playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, 0f);
        }
    }

}
