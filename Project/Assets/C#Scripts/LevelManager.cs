using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
    public float respawnDelay;
    public PlayerMovement thePlayer;

    public GameObject deathParticles;

	// Use this for initialization
	void Start() 
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
	}
	
    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    //Creates a delay in the respawn
    public IEnumerator RespawnCoroutine()
    {
        thePlayer.gameObject.SetActive(false);

        //Create death pariticals at the same position as the player
        Instantiate(deathParticles, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(respawnDelay);
        thePlayer.transform.position = thePlayer.respawnPoint;
        thePlayer.gameObject.SetActive(true);
    }

}
