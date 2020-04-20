using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public string mainMenu;
    public string firstLevel;

    private LevelManager levelManager;
    
	void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();
	}
	

    public void Restart()
    {
        PlayerPrefs.SetInt("CoinsCollected", 0);
        PlayerPrefs.SetInt("PlayerLives", levelManager.startLives);
        PlayerPrefs.SetInt("Experience", levelManager.experience);

        SceneManager.LoadScene(firstLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
