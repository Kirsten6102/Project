using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
    public float respawnDelay;
    public PlayerMovement thePlayer;

	// Use this for initialization
	void Start() 
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
	}
	
    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    public IEnumerator RespawnCoroutine()
    {
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        thePlayer.transform.position = thePlayer.respawnPoint;
        thePlayer.gameObject.SetActive(true);
    }

}
