using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletMovement : MonoBehaviour {

    public float Speed;
    public int damage;
    public Rigidbody2D rb;


    // Use this for initialization
    void Start ()
    {
        rb.velocity = transform.right * Speed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        LevelManager player = hitInfo.GetComponent<LevelManager>();
        if (gameObject.tag == "Player")
        {
            player.HurtPlayer(damage);
            Destroy(gameObject);
        }

    }
}
