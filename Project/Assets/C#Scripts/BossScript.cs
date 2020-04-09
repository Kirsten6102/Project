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

    public GameObject boss;
    public GameObject dropSaw;

	// Use this for initialization
	void Start ()
    {
        

        dropCount = timeBetweenDrops;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(bossActive)
        {
            boss.SetActive(true);
            //boss.transform.position = rightPont.position;

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
