using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

    public string sceneToLoad;

    private PlayerMovement thePlayer;
    private CameraMovement camera;
    private LevelManager levelManager;

    public float waitToMove;
    public float waitToLoad;

    private bool movePlayer;

	// Use this for initialization
	void Start ()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        camera = FindObjectOfType<CameraMovement>();
        levelManager = FindObjectOfType<LevelManager>();
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
        camera.followTarget = false;
        levelManager.canBeHurt = true;

        levelManager.levelMusic.Stop();
        levelManager.completeLevelSound.Play();

        thePlayer.playerRigidbody.velocity = Vector3.zero;

        //PlayerPrefs is a built in utility that retrains data between scenes
        PlayerPrefs.SetInt("CoinsCollected", levelManager.coinsCollected);
        PlayerPrefs.SetInt("PlayerLives", levelManager.currentLives);
        PlayerPrefs.SetInt("PlayerHealth", levelManager.health);
        PlayerPrefs.SetInt("Experience", levelManager.experience);
        PlayerPrefs.SetInt("ExperienceForLvl", levelManager.experienceForLvl);
        PlayerPrefs.SetInt("PlayerLevel", levelManager.expLevel);

        yield return new WaitForSeconds(waitToMove);
        movePlayer = true;

        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
    
}
