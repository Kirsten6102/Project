using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string mainMenu;
    public GameObject helpMenu;

    public GameObject pauseScreen;

    private LevelManager theLevelManager;
    private PlayerMovement thePlayer;

	// Use this for initialization
	void Start ()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        theLevelManager = FindObjectOfType<LevelManager>();
        pauseScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {
                Resume();
            } else
            {
                PauseGame();
            }
        }
	}


    public void PauseGame()
    {
        Time.timeScale = 0;

        thePlayer.canMove = false;
        theLevelManager.mainLevelMusic.Pause();

        pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        thePlayer.canMove = true;
        theLevelManager.mainLevelMusic.Play();

        pauseScreen.SetActive(false);

        Time.timeScale = 1f;
    }

    public void Help()
    {
        helpMenu.gameObject.SetActive(true);
    }

    public void Return()
    {
        helpMenu.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
