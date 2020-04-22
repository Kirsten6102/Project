using UnityEngine;

public class BulletMovement : MonoBehaviour {



    public float speedOfBullet;
    public int amountOfDamage;
    public Rigidbody2D BulletRB;

    // Use this for initialization
    void Start()
    {
        BulletRB.velocity = transform.right * speedOfBullet;
    }

    
    void OnTriggerEnter2D(Collider2D hit)
    {
        Enemy theEnemy = hit.GetComponent<Enemy>();
        if (theEnemy != null)
        {
            theEnemy.TakeDamage(amountOfDamage);
            Destroy(gameObject);
        }
        
    }
}
