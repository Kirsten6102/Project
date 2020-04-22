using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossBulletMovement : MonoBehaviour {

    public float speedOfBullet;
    public int damageToPlayer;
    public Rigidbody2D bulletRB;
    private LevelManager lvlManager;
    

    // Use this for initialization
    void Start ()
    {
        bulletRB.velocity = transform.right * speedOfBullet;
        lvlManager = FindObjectOfType<LevelManager>();
        
    }
    

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (gameObject.tag == "Player" != null)
        {
            lvlManager.HurtPlayer(damageToPlayer);
            Destroy(gameObject);
        }

    }
}
