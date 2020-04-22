using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour {

    public bool bossActive;

    public float timeBetweenDrops;
    private float dropCount;

    public Transform leftPoint;
    public Transform rightPont;
    public Transform dropSawSpawnPoint;

    public Vector3 bossSpawnPoint;
    public GameObject bossSpawn;
    public GameObject boss;
    public GameObject dropSaw;

    private CameraMovement theCamera;
    private LevelManager lvlManager;
    
    public bool takeDamage;
    public int bossStartHealth;
    private int bossHealth;
    
    public GameObject levelComplete;
    public GameObject bossAIScript;

    private BoxCollider2D collider;
    public bool waitForRespawn;

	// Use this for initialization
	void Start ()
    {
        dropCount = timeBetweenDrops;
        
        theCamera = FindObjectOfType<CameraMovement>();
        lvlManager = FindObjectOfType<LevelManager>();
        collider = GetComponent<BoxCollider2D>();

        boss.SetActive(false);
        collider.enabled = true;
        bossHealth = bossStartHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(lvlManager.respawnCoroActive)
        {
            bossActive = false;
            waitForRespawn = true;
        }

        if(waitForRespawn && !lvlManager.respawnCoroActive)
        {
            boss.SetActive(false);
            dropCount = timeBetweenDrops;
            theCamera.followPlayer = true;
        }

        if (bossActive)
        {
            theCamera.followPlayer = false;
            theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3(transform.position.x, 
                theCamera.transform.position.y, theCamera.transform.position.z), theCamera.smoothTheMovement * Time.deltaTime);

            collider.enabled = false;
            boss.SetActive(true);
            bossAIScript.GetComponent<EnemyAI>().enabled = true;
            bossSpawnPoint = bossSpawn.transform.position;

            //relates to dropsaws
            if (dropCount > 0)
            {
                dropCount -= Time.deltaTime;
            } else
            {
                //moves saw spawn postion and creates new saws
                dropSawSpawnPoint.position = new Vector3(Random.Range(leftPoint.position.x, rightPont.position.x), dropSawSpawnPoint.position.y, dropSawSpawnPoint.position.z);
                Instantiate(dropSaw, dropSawSpawnPoint.position, dropSawSpawnPoint.rotation);
                dropCount = timeBetweenDrops;
            }


            if (takeDamage)
            {
                bossHealth -= 1;

                if (bossHealth <= 0)
                {
                    levelComplete.SetActive(true);
                    gameObject.SetActive(false);
                }

                takeDamage = false;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D  other)
    {
        if(other.tag == "Player")
        {
            bossActive = true;
        }
    }
    
}
