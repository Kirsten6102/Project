using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

    public string sceneToLoad;

    private PlayerMovement thePlayer;
    private CameraMovement theCamera;
    private LevelManager theLevelManager;

    public float waitToMove;
    public float waitToLoad;

    private bool movePlayer;

	// Use this for initialization
	void Start ()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        theCamera = FindObjectOfType<CameraMovement>();
        theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(movePlayer)
        {
            thePlayer.playerRigidbody.velocity = new Vector3(thePlayer.movementSpeed, thePlayer.playerRigidbody.velocity.y, 0f);
        }
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //SceneManager.LoadScene(sceneToLoad);
            StartCoroutine("LevelEndCoroutine");
        }
    }

    public IEnumerator LevelEndCoroutine()
    {
        thePlayer.canMove = false;
        theCamera.followTarget = false;
        theLevelManager.isInvincible = true;

        theLevelManager.mainLevelMusic.Stop();
        theLevelManager.CompleteLevelSound.Play();

        thePlayer.playerRigidbody.velocity = Vector3.zero;

        //PlayerPrefs is a built in utility that retrains data between scenes
        PlayerPrefs.SetInt("CoinCount", theLevelManager.coinCount);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.currentLives);
        PlayerPrefs.SetInt("PlayerHealth", theLevelManager.healthCount);

        yield return new WaitForSeconds(waitToMove);
        movePlayer = true;

        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
    
}
