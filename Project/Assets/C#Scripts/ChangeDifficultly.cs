using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeDifficultly : MonoBehaviour {

    private HurtPlayer hurtPlayer;
    private LevelManager levelManager;

    public string firstLevel;

    public int startLives;

    public GameObject bulletPrefab;
    public GameObject lvlManager;
    public int maxHealth;


    void Start ()
    {
        hurtPlayer = FindObjectOfType<HurtPlayer>();
        bulletPrefab.GetComponent<BulletMovement>().amountOfDamage = 0;
        lvlManager.GetComponent<LevelManager>().maxHealth = 0;

       levelManager = FindObjectOfType<LevelManager>();
	}
	


    public void Easy()
    {
        bulletPrefab.GetComponent<BulletMovement>().amountOfDamage = 20;
        lvlManager.GetComponent<LevelManager>().maxHealth = 6;

        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.SetInt("CoinsCollected", 0);
        PlayerPrefs.SetInt("PlayerLives", startLives);
        PlayerPrefs.SetInt("Experience", 0);
        PlayerPrefs.SetInt("ExperienceForLvl", 0);
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("PlayerHealth", 6);
    }

    public void Medium()
    {
        bulletPrefab.GetComponent<BulletMovement>().amountOfDamage = 10;
        lvlManager.GetComponent<LevelManager>().maxHealth = 6;

        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.SetInt("CoinsCollected", 0);
        PlayerPrefs.SetInt("PlayerLives", startLives);
        PlayerPrefs.SetInt("Experience", 0);
        PlayerPrefs.SetInt("ExperienceForLvl", 0);
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("PlayerHealth", 6);

    }

    public void Hard ()
    {
        bulletPrefab.GetComponent<BulletMovement>().amountOfDamage = 5;
        lvlManager.GetComponent<LevelManager>().health = 3;

        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.SetInt("CoinsCollected", 0);
        PlayerPrefs.SetInt("PlayerLives", startLives);
        PlayerPrefs.SetInt("Experience", 0);
        PlayerPrefs.SetInt("ExperienceForLvl", 0);
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("PlayerHealth", 3);
    }

}
