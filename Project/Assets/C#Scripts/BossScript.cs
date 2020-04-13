using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public bool bossActive;

    public float timeBetweenDrops;
    private float dropCount;

    public Transform leftPoint;
    public Transform rightPont;
    public Transform dropSawSpawnPoint;

    public Vector3 bossSpawnPoint;
    public GameObject boss;
    public GameObject dropSaw;

    private CameraMovement theCamera;
    private LevelManager theLevelManager;
    
    public bool takeDamage;
    public int bossStartHealth;
    private int bossCurrentHealth;
    
    public GameObject levelComplete;

    public bool waitForRespawn;

	// Use this for initialization
	void Start ()
    {
        dropCount = timeBetweenDrops;
        
        theCamera = FindObjectOfType<CameraMovement>();
        theLevelManager = FindObjectOfType<LevelManager>();
        
        bossCurrentHealth = bossStartHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(theLevelManager.respawnCoActive)
        {
            bossActive = false;
            waitForRespawn = true;
        }

        if(waitForRespawn && !theLevelManager.respawnCoActive)
        {
            boss.SetActive(false);
            dropCount = timeBetweenDrops;
            theCamera.followTarget = true;
        }

        if (bossActive)
        {
            theCamera.followTarget = false;
            theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3(transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z), theCamera.smoothMovement * Time.deltaTime);

            boss.SetActive(true);
            bossSpawnPoint = transform.position;
            

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
                bossCurrentHealth -= 1;

                if (bossCurrentHealth <= 0)
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
